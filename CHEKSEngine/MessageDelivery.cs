using System;
using System.Collections.Generic;
using System.Xml;

namespace CHEKS
{
	namespace CHEKSEngine
	{
		#region --- Délégués ---
		/// <summary>
		/// <para>Delegate for the internal methods required by the selected CHEKS pattern.</para>
		/// </summary>
		public delegate Message InternalSendMessageDelegate(string message);
		/// <summary>
		/// <para>Delegate for the message sending (TCP, DB, etc) method in the application using the library.</para>
		/// </summary>
		public delegate bool OnSendMessageDelegate(CHEKS.CHEKSEngine.Message message);
		#endregion
		
		#region --- Énumérations ---
		/// <summary>
		/// <para>CHEKS usage patterns</para>
		/// </summary>
		public enum CHEKSPattern {
			/// <summary>
			/// <para>Single System Patern.</para>
			/// </summary>
			SingleSystem,
			/// <summary>
			/// <para>Dual Systems Patern.</para>
			/// </summary>
			DualSystems,
			/// <summary>
			/// <para>Electronic Key Patern #1.</para>
			/// </summary>
			ElectronicKey1
		}
		#endregion
			
		/// <summary>
		/// <para>Class responsible to manage the sending and receiving of messages using TCP/IP, including encryption and systems evolutions.</para>
		/// </summary>
		public class MessageDelivery
		{		
			#region --- Attributs ---
			private InternalSendMessageDelegate internalSendMessageDelegate;
			private OnSendMessageDelegate onSendMessageDelegate;
			private InternalSendMessageDelegate internalReceiveMessageDelegate;
			
			private string senderSystem;
			private string receiverSystem;
			
			private CHEKSPattern activePattern;
			
			private bool SingleSystemLock = false;
			
			// Pools
			private Queue<Message> outMessagePool = new Queue<Message>();
			#endregion
			
			#region --- Accesseurs ---
			/// <summary>
			/// <para>Delegate that will handle the message sending (via TCP, DB, etc) in the application using the library.</para>
			/// </summary>
			public OnSendMessageDelegate onSendMessage {
				get {
					return this.onSendMessageDelegate;
				}
				set {
					this.onSendMessageDelegate = value;
				}
			}
			/// <summary>
			/// <para>Delegate that will handle the message sending for the selected pattern.</para>
			/// </summary>
			public InternalSendMessageDelegate SendMessage {
				get {
					return this.internalSendMessageDelegate;
				}
			}
			/// <summary>
			/// <para>Delegate that will handle the message reception for the selected pattern.</para>
			/// </summary>
			public InternalSendMessageDelegate ReceiveMessage {
				get {
					return this.internalReceiveMessageDelegate;
				}
			}
			/// <summary>
			/// <para>System that will be used to send messages.</para>
			/// </summary>
			public string SenderSystem {
				get {
					return this.senderSystem;
				}
			}
			/// <summary>
			/// <para>System that will be used to receive messages.</para>
			/// </summary>
			public string ReceiverSystem {
				get {
					return this.receiverSystem;
				}
			}
			/// <summary>
			/// <para>Message pool for single system patterns and when messages can't be sent. Has to be handle outside the library if required.</para>
			/// </summary>
			public Queue<Message> OutMessagePool {
				get {
					return this.outMessagePool;
				}
			}
			#endregion
			
			#region --- Constructeur ---
			/// <summary>
			/// <para>Constructor building a MessageDelivery object from specified parameters.</para>
			/// </summary>
			/// <param name="pattern">CHEKS usage pattern to be used.</param>
			/// <param name="sender">Sender system id.</param>
			/// <param name="receiver">Receiver system id.</param>
			/// <param name="sendMessage">Delegate that will handle the message sending (via TCP, DB, etc).</param>
			/// <exception cref="System.Exception">Raise exceptions if sender system, receiver system or sendMessage delegate is missing.</exception>
			public MessageDelivery(CHEKSPattern pattern, string sender, string receiver = "", OnSendMessageDelegate sendMessage = null)
			{
				if (string.IsNullOrEmpty(sender)) {
					throw new Exception("no sender system specified");
				}
				
				this.senderSystem = sender;
				this.receiverSystem = receiver;
				this.onSendMessageDelegate = sendMessage;
				this.activePattern = pattern;
				
				switch (pattern) {
					case CHEKSPattern.SingleSystem:
						if (sendMessage == null) {
							throw new Exception("no receiver delegate specified");
						}
						this.receiverSystem = this.senderSystem;
						this.internalSendMessageDelegate = this.SendMessageSingleSystem;
						this.internalReceiveMessageDelegate = this.ReceiveMessageSingleSystem;
						break;
					case CHEKSPattern.DualSystems:
						if (string.IsNullOrEmpty(receiver)) {
							throw new Exception("no receiver system specified");
						}
						if (sendMessage == null) {
							throw new Exception("no receiver delegate specified");
						}
						this.internalSendMessageDelegate = this.SendMessageDualSystems;
						this.internalReceiveMessageDelegate = this.ReceiveMessageDualSystems;
						break;
					case CHEKSPattern.ElectronicKey1:
						this.internalSendMessageDelegate = this.SendMessageElectronicKey1;
						break;
				}
			}
			#endregion
			
			#region --- Méthodes d'envoi ---
			/// <summary>
			/// <para>Send message delegate for the single system pattern.</para>
			/// </summary>
			/// <param name="messageText">Text to be sent.</param>
			/// <returns>Message object indicating the state of the transaction</returns>
			private Message SendMessageSingleSystem (string messageText)
			{
				if (string.IsNullOrEmpty(messageText)) {
					return new Message(this.senderSystem, MessageStates.Empty);
				}
				
				// Vérifier si le système est en attente d'une réponse et si oui, pooler le prochain message
				if (SingleSystemLock) {
					// Pooler le message
					this.outMessagePool.Enqueue(new Message(messageText));
					
					return new Message(this.senderSystem, MessageStates.Pooled);
				}
				
				Message m = new Message(this.senderSystem, MessageStates.None);
				m.AddContent(Encrypter.Instance.Encrypt(this.senderSystem, messageText));
				
				// Envoyer le message de façon synchrone
				if (this.onSendMessage(m)) {
					// Barrer le système pour éviter la désynchronisation
					this.SingleSystemLock = true;
					
					// Faire évoluer le système
					Encrypter.Instance.Play(this.senderSystem);

					// Retourner le succès
					return new Message(this.senderSystem, MessageStates.Success);
				} else {
					// Retourner l'échec
					return new Message(this.senderSystem, MessageStates.Failed);
				}
			}

			/// <summary>
			/// <para>Send message delegate for the dual systems pattern.</para>
			/// </summary>
			/// <param name="messageText">Text to be sent.</param>
			/// <returns>Message object indicating the state of the transaction</returns>
			private Message SendMessageDualSystems (string messageText)
			{
				Message m = new Message(this.senderSystem, MessageStates.None);
				m.AddContent(Encrypter.Instance.Encrypt(this.senderSystem, messageText));
				
				// Pooler le message
				this.outMessagePool.Enqueue(m);
				
				// Appeler le délégué d'envoi du message
				if (this.onSendMessageDelegate(m)) {
					// Faire évoluer le système
					Encrypter.Instance.Play(this.senderSystem);
					
					// Enlever le message de la pile
					this.outMessagePool.Dequeue();
					
					// Retourner la confirmation que le message a été envoyé
					return new Message(this.senderSystem, MessageStates.Success);
				}

				// Aviser que le message n'a pas été envoyé
				return new Message(this.senderSystem, MessageStates.Failed);
			}
			
			/// <summary>
			/// <para>Send the next message in queue when using the single system pattern.</para>
			/// </summary>
			/// <returns>Message object indicating the state of the transaction</returns>
			private Message SendNexMessage()
			{
				// Débarrer le système
				this.SingleSystemLock = false;
				
				// Envoyer le message
				return this.SendMessage(outMessagePool.Peek().Serialize());
			}
			#endregion
			
			#region --- Méthodes de réception ---
			/// <summary>
			/// <para>Message reception delegate for the single system pattern.</para>
			/// </summary>
			/// <param name="message">Text message received to be decrypted.</param>
			/// <returns>Message object containing the decrypted message and the state of the transaction.</returns>
			/// <exception cref="System.Exception">Exception raised when unable to receive the message properly.</exception>
			private Message ReceiveMessageSingleSystem(string message) {
				// Valider que le message est pas vide
				if (string.IsNullOrEmpty(message.TrimEnd('\0'))) {
				    return new Message(this.receiverSystem, MessageStates.Empty);
			    }
				
				// Convertir en Message
				Message response = new Message(message.TrimEnd('\0'));
				
				// Par défaut, le message est considéré comme valide.
				bool valid = true;
				
				// Décrypter le message
				for (int x = 0; x < response.ContentList.Count; x++) {
					XmlDocument xml = new XmlDocument();
					xml.LoadXml(response.ContentList[x]);
					xml.ChildNodes.Item(0).InnerText = Encrypter.Instance.Decrypt(this.receiverSystem, xml.ChildNodes.Item(0).InnerText).TrimEnd('\0');
					
					// On essai de désérialiser dans un objet MessageItem... si ça fonctionne et que le systemId est le même, c'est ok
					MessageItem mi = new MessageItem("<item>" + xml.ChildNodes.Item(0).InnerText + "</item>");
					
					// Ajouter à la liste des contenus
					response.ContentList[x] = mi.Serialize();
					
					if (mi.SystemCheck != receiverSystem) {
						valid = false;
					}
				}

				// Vérifier si le message est toujours valide
				if (valid) {
					Encrypter.Instance.Play(this.receiverSystem);
					
					// Vérifier si d'autres messages sont à envoyer
					if (this.outMessagePool.Count > 0) {
						response = this.SendNexMessage();
						
						// Valider la réponse, lever une exception si le message n'est pas envoyé
						if (response.State == MessageStates.Failed) {
							throw new Exception("unable to receive message");
						}
					}
					
					// Changer l'état
					response.State = MessageStates.Success;
					
					// Retourner le message
					return response;
				}
				
				// Retourner l'échec de la réception du message
				return new Message(this.receiverSystem, MessageStates.Failed);
			}
			
			/// <summary>
			/// <para>Message reception delegate for the dual systems pattern.</para>
			/// </summary>
			/// <param name="message">Text message received to be decrypted.</param>
			/// <returns>Message object containing the decrypted message and the state of the transaction.</returns>
			/// <exception cref="System.Exception">Exception raised when unable to receive the message properly.</exception>
			private Message ReceiveMessageDualSystems(string message) {
				// Valider que le message est pas vide
				if (string.IsNullOrEmpty(message.TrimEnd('\0'))) {
				    return new Message(this.receiverSystem, MessageStates.Empty);
			    }
				
				// Convertir en Message
				Message response = new Message(message.TrimEnd('\0'));
				
				// Par défaut, le message est considéré comme valide.
				bool valid = true;
				
				// Décrypter le message
				for (int x = 0; x < response.ContentList.Count; x++) {
					XmlDocument xml = new XmlDocument();
					xml.LoadXml(response.ContentList[x]);
					xml.ChildNodes.Item(0).InnerText = Encrypter.Instance.Decrypt(this.receiverSystem, xml.ChildNodes.Item(0).InnerText).TrimEnd('\0');
					
					// On essai de désérialiser dans un objet MessageItem... si ça fonctionne et que le systemId est le même, c'est ok
					try {
						if (string.IsNullOrEmpty(xml.ChildNodes.Item(0).InnerText)) {
							valid = false;
							response.ContentList[x] = "";
						} else {
							MessageItem mi = new MessageItem("<item>" + xml.ChildNodes.Item(0).InnerText + "</item>");
							
							if (mi.SystemCheck != receiverSystem) {
								throw new Exception("Invalid message");
							} else {
								// Ajouter à la liste des contenus
								response.ContentList[x] = mi.Serialize();
							}
					    }
					} catch {
						valid = false;
						response.ContentList[x] = "";
					}

				}
				
				if (valid) {
					try {
						Encrypter.Instance.Play(this.receiverSystem);
						
						// Changer l'état
						response.State = MessageStates.Success;
						
						// Retourner le message
						return response;
					} catch { // TODO : Faire une vrai gestion des erreurs...
						throw new Exception("unable to receive message");
					}
				}
				
				// Retourner l'échec de la réception du message
				return new Message(this.receiverSystem, MessageStates.Failed);
			}
			#endregion
			
			#region --- Méthodes de clés électroniques ----
			/// <summary>
			/// <para>NOT IMPLEMENTED YET</para>
			/// </summary>
			/// <param name="message"></param>
			/// <returns>Message Object</returns>
			/// <exception cref="System.Exception">Not implemented yet.</exception>
			private Message SendMessageElectronicKey1 (string message)
			{
				throw new Exception("Not implemented yet");
				// TODO : Développer le pattern de clé (mettre dans une autre classe?! Genre KeyReader?!)
				return new Message("", MessageStates.None);
			}
			#endregion
		}
	}
}
