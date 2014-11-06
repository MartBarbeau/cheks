using System;
using System.Xml;
using System.Collections.Generic;

namespace CHEKS
{
	namespace CHEKSEngine
	{
		#region --- Enums ---
		/// <summary>
		/// <para>List of possible states for a message.</para>
		/// </summary>
		public enum MessageStates {
			/// <summary>
			/// <para>Indicates that the message was successfully sent.</para>
			/// </summary>
			Success,
			/// <summary>
			/// <para>Indicates that the message failed to send.</para>
			/// </summary>
			Failed,
			/// <summary>
			/// <para>Indicates that the message was pooled.</para>
			/// </summary>
			Pooled,
			/// <summary>
			/// <para>Indicates that the message has no state.</para>
			/// </summary>
			None,
			/// <summary>
			/// <para>Indicates that the message was empty.</para>
			/// </summary>
			Empty
		}
		#endregion
		
		/// <summary>
		/// <para>CHEKS Message object, containing 0 to x messages.</para>
		/// </summary>
		public class Message
		{
			#region --- Attributs ---
			/// <summary>
			/// <para>Id of the system that will receive the message.</para>
			/// </summary>
			public string SystemId;
			/// <summary>
			/// <para>State of the message.</para>
			/// </summary>
			public MessageStates State;
			private List<string> content = new List<string>();
			#endregion
			
			#region ---- Accesseurs ---
			/// <summary>
			/// <para>Concatenation of all the contents in the message.</para>
			/// </summary>
			public string Content {
				get {
					string tempContent = "";
					foreach (string s in this.content) {
						tempContent += s;
					}
					return tempContent;
				}
			}
			/// <summary>
			/// <para>List of all the content strings in the message.</para>
			/// </summary>
			public List<string> ContentList {
				get {
					return this.content;
				}
			}
			#endregion
			
			#region --- Constructeurs ---
			/// <summary>
			/// <para>Constructor building a new message from specified parameters.</para>
			/// </summary>
			/// <param name="systemId">Id of the system that will receive the message.</param>
			/// <param name="state">Initial state of the message.</param>
			public Message(string systemId, MessageStates state)
			{
				this.SystemId = systemId;
				this.State = state;
			}
			
			/// <summary>
			/// <para>Constructor building a new message from a serialization string.</para>
			/// </summary>
			/// <param name="serialization">String containing the serialization.</param>
			public Message(string serialization)
			{
				XmlDocument xml = new XmlDocument();
				xml.LoadXml(serialization);
				
				this.SystemId = xml.GetElementsByTagName("message").Item(0).ChildNodes.Item(0).InnerText;
				this.State = (MessageStates)Enum.Parse(typeof(MessageStates), xml.GetElementsByTagName("message").Item(0).ChildNodes.Item(1).InnerText);
				foreach (XmlNode node in xml.GetElementsByTagName("message").Item(0).ChildNodes.Item(2).ChildNodes) {
					this.content.Add(node.OuterXml);
				}
				
			}
			#endregion
			
			#region --- Méthodes ---
			/// <summary>
			/// <para>Add content to the message</para>
			/// </summary>
			/// <param name="messageContent">Content to be added to the message.</param>
			public void AddContent(string messageContent)
			{
				this.content.Add("<item>" + messageContent + "</item>");
			}
			
			/// <summary>
			/// <para>Remove content to the message</para>
			/// </summary>
			/// <param name="messageContent">Content to be removed to the message.</param>
			public void RemoveContent(string messageContent)
			{
				if (this.content.Contains("<item>" + messageContent + "</item>")) {
					this.content.Remove("<item>" + messageContent + "</item>");	
				}
			}
			
			/// <summary>
			/// <para>Serialization of the message.</para>
			/// </summary>
			/// <returns>Serialization string for the message.</returns>
			public string Serialize()
			{
				return "<message><systemId>" + this.SystemId + "</systemId><state>" + this.State.ToString() + "</state><content>" + this.Content + "</content></message>";
			}
			#endregion
		}
	}
}