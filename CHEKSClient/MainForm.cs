using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.IO;
using System.Xml;
using CHEKS.CHEKSEngine;

namespace CHEKSClient
{
	public partial class MainForm : Form
	{
		#region --- Attributs ---
		private TcpListener tcpListener;
		private TcpClient tcpSender;
		private Thread listenThread;
		
		private delegate void SetTextCallback(string text);

		private MessageDelivery _mdDualSystems;
		#endregion

		#region --- Accesseurs ---
		private MessageDelivery MD {
			get {
				if (this._mdDualSystems == null) {
					this._mdDualSystems = new MessageDelivery(CHEKSPattern.DualSystems, this.txtSystemOut.Text, this.txtSystemIn.Text, this.SendMesage);
				}
				
				return this._mdDualSystems;
			}
		}
		#endregion
		
		#region --- Constructeur ---
		public MainForm()
		{
			InitializeComponent();
		}
		#endregion
		
		#region --- Évènements ---
		void TxtSystemDoubleClick(object sender, EventArgs e)
		{
			OpenFileDialog open = new OpenFileDialog();      
			open.InitialDirectory = String.IsNullOrEmpty(this.txtRepertoireSystemes.Text) ? @"C:\" : this.txtRepertoireSystemes.Text;
			open.Multiselect = false;	    
		    open.Title = "Ouvrir un système CHEKS"; 
		    open.CheckFileExists = true; 
		    open.CheckPathExists = true; 
		    open.DefaultExt = "xml"; 
		    open.Filter = "Fichier XML (*.xml)|*.xml"; 
		    open.FilterIndex = 2;   
		
		    if (open.ShowDialog() == DialogResult.OK) 
		    { 
		    	try {
		    		CASystem cas = new CASystem();
		    		cas.Load(open.FileName);
		    		
		    		((TextBox)sender).Text = cas.Name;
		    		
		    		this.EnableButtons();
		    		
		    	} catch (Exception err) {
		    		MessageBox.Show("Fichier de système CHEKS invalide... Veuillez essayer un autre fichier ou en générer un nouveau.\n\r\n\r" + err.ToString(),
		    		                "Erreur lors de l'ouverture du fichier", 
		    		                MessageBoxButtons.OK, 
		    		                MessageBoxIcon.Error);
		    	}
		    	
		    } 
		}
		
		void TxtRepertoireSystemesDoubleClick(object sender, EventArgs e)
		{
			OpenFileDialog open = new OpenFileDialog();
		    open.InitialDirectory = @"C:\";
			open.Multiselect = false;	    
		    open.Title = "Définir le répertoire où se trouve les systèmes"; 
		    open.CheckFileExists = false;
		    open.CheckPathExists = true;
		    open.FilterIndex = 2;   
		
		    if (open.ShowDialog() == DialogResult.OK) 
		    { 
		    	((TextBox)sender).Text = Path.GetDirectoryName(open.FileName);
		    	Encrypter.Instance.SystemsDirectory = Path.GetDirectoryName(open.FileName);
		    	
		    	if (open.FileName.EndsWith(".xml")) {
		    		try {
			    		XmlDocument doc = new XmlDocument();
			    		doc.Load(open.FileName);
			    		
			    		if (doc.GetElementsByTagName("in").Count > 0 && doc.GetElementsByTagName("out").Count > 0) {
			    			// Vérifier si le système entrant spécifié est valide.
			    			CASystem cas = new CASystem();
			    			cas.Load(Path.Combine(Encrypter.Instance.SystemsDirectory, doc.GetElementsByTagName("in").Item(0).InnerText));
			    			this.txtSystemIn.Text = cas.Name;
			    			// Vérifier si le système sortant spécifié est valide.
			    			cas = new CASystem();
			    			cas.Load(Path.Combine(Encrypter.Instance.SystemsDirectory, doc.GetElementsByTagName("out").Item(0).InnerText));
			    			this.txtSystemOut.Text = cas.Name;
			    		}
		    		} catch {
		    			// Rien à faire
		    		}
		    	}
		    	
		    	this.EnableButtons();
		    	
		    } 
		}
		
		void BtnListenerStartClick(object sender, EventArgs e)
		{
			this.StartListener();
			
			this.btnListenerStart.Enabled = false;
			this.btnListenerStop.Enabled = true;
		}
		
		void BtnListenerStopClick(object sender, EventArgs e)
		{
			this.StopListener();
			
			this.btnListenerStart.Enabled = true;
			this.btnListenerStop.Enabled = false;
		}
		
		void BtnSendClick(object sender, EventArgs e)
		{
			this.AppendTextOut(this.CustomTimeStamp() + " Sending message...");
			this.AppendTextOut(Environment.NewLine);
						
			this.AppendTextOut("--- Encrypting...");
			this.AppendTextOut(Environment.NewLine);
			
			if (this.MD.SendMessage(this.txtMessage.Text).State == MessageStates.Success) {
				this.txtMessage.Text = "";
			} else {
				this.AppendTextOut("--- Unable to send message");
				this.AppendTextOut(Environment.NewLine);
			}
		}
		
		void TxtListenerPortValueChanged(object sender, EventArgs e)
		{
			this.EnableButtons();
		}
		
		void TxtIPAddressTextChanged(object sender, EventArgs e)
		{
			this.EnableButtons();
		}
		
		void TxtClientPortValueChanged(object sender, EventArgs e)
		{
			this.EnableButtons();
		}
		
		void TxtMessageKeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)13) {
				this.BtnSendClick(null, null);
			}
		}
		#endregion
		
		#region --- Méthodes de réception des messages ---
		private void StartListener() {
			// Welcome message
			this.AppendTextIn(this.CustomTimeStamp() + " CHEKS Test Listener starting...");
			this.AppendTextIn(Environment.NewLine);

			// Server socket creation
			this.AppendTextIn("--- Creating sockets...");
			this.tcpListener = new TcpListener(IPAddress.Any, int.Parse(this.txtListenerPort.Text));
			this.AppendTextIn(Environment.NewLine);
			this.AppendTextIn("--- Done!");
			this.AppendTextIn(Environment.NewLine);

			// Démarrage du serveur
			this.AppendTextIn(string.Format("--- Starting listener on port {0}", this.txtListenerPort.Text));
			this.listenThread = new Thread(new ThreadStart(ListenForClients));
			this.listenThread.IsBackground = true;
			this.listenThread.Start();
			this.AppendTextIn(Environment.NewLine);
			this.AppendTextIn("--- Done!");
			this.AppendTextIn(Environment.NewLine);
			this.AppendTextIn(this.CustomTimeStamp() + " Listening...");
			this.AppendTextIn(Environment.NewLine);
		}

		private void StopListener()
		{
			this.tcpListener.Stop();
			this.listenThread.Abort();
			
			this.AppendTextIn(this.CustomTimeStamp() + " CHEKS Test Listener stopped!");
			this.AppendTextIn(Environment.NewLine);
		}

		private void ListenForClients()
		{
			this.tcpListener.Start();

			while (true)
			{
				// Attendre qu'un client se connecte
				TcpClient client = tcpListener.AcceptTcpClient();

				// Renvoyer le traitement à un thread pour éviter que ça bloque
				Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
				clientThread.IsBackground = true;
				clientThread.Start(client);
			}
		}

		private void HandleClientComm(object client)
		{
			TcpClient tcpClient = (TcpClient)client;
			NetworkStream clientStream = tcpClient.GetStream();

			UTF8Encoding encoder = new UTF8Encoding();
			
			string messageString = "";

			this.AppendTextIn(string.Format(this.CustomTimeStamp() + " Incoming message from {0}", ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address.ToString()));
			this.AppendTextIn(Environment.NewLine);
			
			int byteTotal = 0;

			// Réception du message
			while (true)
			{
				this.AppendTextIn("--- Receiving block");
				this.AppendTextIn(Environment.NewLine);
				byte[] message = new byte[4096];
				int bytesRead;

				bytesRead = 0;

				try
				{
					// Attendre que le client envoi un message
					bytesRead = clientStream.Read(message, 0, 4096);
					
					byteTotal += bytesRead;
				}
				catch (Exception e)
				{
					this.AppendTextIn("--- Error while receiving the message : " + e.ToString());
					this.AppendTextIn(Environment.NewLine);
					break;
				}

				// Si 0 bytes reçues, le client s'est déconnecté
				if (bytesRead == 0)
				{
					this.AppendTextIn("--- Client disconnected");
					this.AppendTextIn(Environment.NewLine);
					break;
				}

				this.AppendTextIn("--- Done (" + byteTotal.ToString() + " bytes)!");
				this.AppendTextIn(Environment.NewLine);
				// Placer les bytes reçues dans le message
				messageString += encoder.GetString(message, 0, bytesRead);
			}

			tcpClient.Close();

			// Traiter le message
			ProcessIncomingMessage(messageString);
		}

		private void ProcessIncomingMessage(string message)
		{
			this.AppendTextIn("--- Raw message : ");
			this.AppendTextIn(message);
			this.AppendTextIn(Environment.NewLine);
			
			try {
				this.AppendTextIn("--- Decrypting...");
				this.AppendTextIn(Environment.NewLine);
				
				// Décrypter le message et l'afficher
				CHEKS.CHEKSEngine.Message mess = this.MD.ReceiveMessage(message);
				
				this.AppendTextIn("--- Done...");
				this.AppendTextIn(Environment.NewLine);
				
				if (mess.State == MessageStates.Success) {
					// Afficher le message
					this.AppendTextIn(this.CustomTimeStamp() + " Decrypted message : ");
					this.AppendTextIn(new CHEKS.CHEKSEngine.MessageItem(mess.Content).MessageContent);
					this.AppendTextIn(Environment.NewLine);
				} else {
					this.AppendTextIn("--- Error : message state = " + mess.State);
					this.AppendTextIn(Environment.NewLine);
				}
			} catch (Exception e) {
				this.AppendTextIn("--- Error : " + e.ToString());
				this.AppendTextIn(Environment.NewLine);
			}
		}
		#endregion

		#region --- Méthodes d'envoi des messages ---
		private bool SendMesage(CHEKS.CHEKSEngine.Message message)
		{
			
			this.AppendTextOut("--- Done!");
			this.AppendTextOut(Environment.NewLine);
			this.AppendTextOut("--- Raw message : " + this.txtMessage.Text);
			this.AppendTextOut(Environment.NewLine);
			this.AppendTextOut("--- Encrypted message : " + message.Serialize());
			this.AppendTextOut(Environment.NewLine);
			this.AppendTextOut("--- Setting up TCP connection...");
			this.AppendTextOut(Environment.NewLine);
			
			int byteSent = 0;
			
			try {
				tcpSender = new TcpClient();
	
				tcpSender.Connect(new IPEndPoint(IPAddress.Parse(this.txtIPAddress.Text), 
				                                 int.Parse(this.txtClientPort.Text)));
				
				NetworkStream clientStream = tcpSender.GetStream();
				
				this.AppendTextOut("--- Done!");
				this.AppendTextOut(Environment.NewLine);
	
				ASCIIEncoding encoder = new ASCIIEncoding();
				byte[] buffer = encoder.GetBytes(message.Serialize());
	
				this.AppendTextOut("--- Sending data...");
				this.AppendTextOut(Environment.NewLine);
				// TODO : Flusher par block au lieu de tout d'un coup?
				clientStream.Write(buffer, 0 , buffer.Length);
				byteSent += buffer.Length;
				clientStream.Flush();
				clientStream.Close();
				this.AppendTextOut("--- Done!");
				this.AppendTextOut(Environment.NewLine);
			} catch (Exception e) {
				MessageBox.Show(e.ToString(), "Error while sending message", MessageBoxButtons.OK, MessageBoxIcon.Error);
				
				return false;
			}
			
			this.AppendTextOut(this.CustomTimeStamp() + " Message sent (" + byteSent.ToString() + " bytes)");
			this.AppendTextOut(Environment.NewLine);
			
			return true;
		}
		#endregion
		
		#region --- Méthodes générales ---
		private void EnableButtons()
		{
			if (!string.IsNullOrEmpty(this.txtSystemIn.Text) 
			    && !string.IsNullOrEmpty(this.txtRepertoireSystemes.Text) 
			    && !string.IsNullOrEmpty(this.txtListenerPort.Text)) {
    			this.btnListenerStart.Enabled = true;
    		}
    		
    		if (!string.IsNullOrEmpty(this.txtSystemOut.Text) 
			    && !string.IsNullOrEmpty(this.txtRepertoireSystemes.Text) 
			    && !string.IsNullOrEmpty(this.txtClientPort.Text) 
			    && !string.IsNullOrEmpty(this.txtIPAddress.Text)) {
    			this.btnSend.Enabled = true;
    		}
		}
		
		private void AppendTextIn(string txt)
		{
			if (this.txtInOutput.InvokeRequired)
			{	
				SetTextCallback d = new SetTextCallback(AppendTextIn);
				this.Invoke(d, new object[] {txt});
			}
			else
			{
				this.txtInOutput.AppendText(txt);
			}

		}
		
		private void AppendTextOut(string txt)
		{
			if (this.txtOutOutput.InvokeRequired)
			{	
				SetTextCallback d = new SetTextCallback(AppendTextOut);
				this.Invoke(d, new object[] {txt});
			}
			else
			{
				this.txtOutOutput.AppendText(txt);
			}
		}
		
		private string CustomTimeStamp()
		{
			return DateTime.Now.ToString("HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
		}
		#endregion
	}
}
