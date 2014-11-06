/*
 * Created by SharpDevelop.
 * User: lmsace
 * Date: 2014-09-03
 * Time: 11:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace CHEKSClient
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.grpServer = new System.Windows.Forms.GroupBox();
			this.label6 = new System.Windows.Forms.Label();
			this.txtSystemIn = new System.Windows.Forms.TextBox();
			this.txtInOutput = new System.Windows.Forms.TextBox();
			this.btnListenerStart = new System.Windows.Forms.Button();
			this.btnListenerStop = new System.Windows.Forms.Button();
			this.txtListenerPort = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.grpClient = new System.Windows.Forms.GroupBox();
			this.txtIPAddress = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.btnSend = new System.Windows.Forms.Button();
			this.txtSystemOut = new System.Windows.Forms.TextBox();
			this.txtMessage = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtOutOutput = new System.Windows.Forms.TextBox();
			this.txtClientPort = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.txtRepertoireSystemes = new System.Windows.Forms.TextBox();
			this.grpServer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtListenerPort)).BeginInit();
			this.grpClient.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtClientPort)).BeginInit();
			this.SuspendLayout();
			// 
			// grpServer
			// 
			this.grpServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.grpServer.Controls.Add(this.label6);
			this.grpServer.Controls.Add(this.txtSystemIn);
			this.grpServer.Controls.Add(this.txtInOutput);
			this.grpServer.Controls.Add(this.btnListenerStart);
			this.grpServer.Controls.Add(this.btnListenerStop);
			this.grpServer.Controls.Add(this.txtListenerPort);
			this.grpServer.Controls.Add(this.label1);
			this.grpServer.Location = new System.Drawing.Point(12, 38);
			this.grpServer.Name = "grpServer";
			this.grpServer.Size = new System.Drawing.Size(797, 286);
			this.grpServer.TabIndex = 0;
			this.grpServer.TabStop = false;
			this.grpServer.Text = "Listener";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(7, 47);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(67, 23);
			this.label6.TabIndex = 6;
			this.label6.Text = "System in :";
			// 
			// txtSystemIn
			// 
			this.txtSystemIn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtSystemIn.Location = new System.Drawing.Point(80, 44);
			this.txtSystemIn.Name = "txtSystemIn";
			this.txtSystemIn.ReadOnly = true;
			this.txtSystemIn.Size = new System.Drawing.Size(711, 20);
			this.txtSystemIn.TabIndex = 5;
			this.txtSystemIn.TabStop = false;
			this.txtSystemIn.DoubleClick += new System.EventHandler(this.TxtSystemDoubleClick);
			// 
			// txtInOutput
			// 
			this.txtInOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtInOutput.Location = new System.Drawing.Point(7, 70);
			this.txtInOutput.Multiline = true;
			this.txtInOutput.Name = "txtInOutput";
			this.txtInOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtInOutput.Size = new System.Drawing.Size(784, 210);
			this.txtInOutput.TabIndex = 3;
			this.txtInOutput.WordWrap = false;
			// 
			// btnListenerStart
			// 
			this.btnListenerStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnListenerStart.Enabled = false;
			this.btnListenerStart.Location = new System.Drawing.Point(635, 15);
			this.btnListenerStart.Name = "btnListenerStart";
			this.btnListenerStart.Size = new System.Drawing.Size(75, 23);
			this.btnListenerStart.TabIndex = 1;
			this.btnListenerStart.Text = "Start";
			this.btnListenerStart.UseVisualStyleBackColor = true;
			this.btnListenerStart.Click += new System.EventHandler(this.BtnListenerStartClick);
			// 
			// btnListenerStop
			// 
			this.btnListenerStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnListenerStop.Enabled = false;
			this.btnListenerStop.Location = new System.Drawing.Point(716, 15);
			this.btnListenerStop.Name = "btnListenerStop";
			this.btnListenerStop.Size = new System.Drawing.Size(75, 23);
			this.btnListenerStop.TabIndex = 2;
			this.btnListenerStop.Text = "Stop";
			this.btnListenerStop.UseVisualStyleBackColor = true;
			this.btnListenerStop.Click += new System.EventHandler(this.BtnListenerStopClick);
			// 
			// txtListenerPort
			// 
			this.txtListenerPort.Location = new System.Drawing.Point(80, 18);
			this.txtListenerPort.Maximum = new decimal(new int[] {
									65000,
									0,
									0,
									0});
			this.txtListenerPort.Name = "txtListenerPort";
			this.txtListenerPort.Size = new System.Drawing.Size(120, 20);
			this.txtListenerPort.TabIndex = 0;
			this.txtListenerPort.Value = new decimal(new int[] {
									8080,
									0,
									0,
									0});
			this.txtListenerPort.ValueChanged += new System.EventHandler(this.TxtListenerPortValueChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(68, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Port : ";
			// 
			// grpClient
			// 
			this.grpClient.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.grpClient.Controls.Add(this.txtIPAddress);
			this.grpClient.Controls.Add(this.label5);
			this.grpClient.Controls.Add(this.btnSend);
			this.grpClient.Controls.Add(this.txtSystemOut);
			this.grpClient.Controls.Add(this.txtMessage);
			this.grpClient.Controls.Add(this.label4);
			this.grpClient.Controls.Add(this.label3);
			this.grpClient.Controls.Add(this.txtOutOutput);
			this.grpClient.Controls.Add(this.txtClientPort);
			this.grpClient.Controls.Add(this.label2);
			this.grpClient.Location = new System.Drawing.Point(12, 330);
			this.grpClient.Name = "grpClient";
			this.grpClient.Size = new System.Drawing.Size(797, 262);
			this.grpClient.TabIndex = 1;
			this.grpClient.TabStop = false;
			this.grpClient.Text = "Client";
			// 
			// txtIPAddress
			// 
			this.txtIPAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtIPAddress.Location = new System.Drawing.Point(635, 16);
			this.txtIPAddress.Name = "txtIPAddress";
			this.txtIPAddress.Size = new System.Drawing.Size(156, 20);
			this.txtIPAddress.TabIndex = 5;
			this.txtIPAddress.Text = "127.0.0.1";
			this.txtIPAddress.TextChanged += new System.EventHandler(this.TxtIPAddressTextChanged);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(6, 46);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(68, 23);
			this.label5.TabIndex = 8;
			this.label5.Text = "System out :";
			// 
			// btnSend
			// 
			this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSend.Enabled = false;
			this.btnSend.Location = new System.Drawing.Point(716, 231);
			this.btnSend.Name = "btnSend";
			this.btnSend.Size = new System.Drawing.Size(75, 23);
			this.btnSend.TabIndex = 8;
			this.btnSend.Text = "Send";
			this.btnSend.UseVisualStyleBackColor = true;
			this.btnSend.Click += new System.EventHandler(this.BtnSendClick);
			// 
			// txtSystemOut
			// 
			this.txtSystemOut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtSystemOut.Location = new System.Drawing.Point(80, 43);
			this.txtSystemOut.Name = "txtSystemOut";
			this.txtSystemOut.ReadOnly = true;
			this.txtSystemOut.Size = new System.Drawing.Size(711, 20);
			this.txtSystemOut.TabIndex = 7;
			this.txtSystemOut.TabStop = false;
			this.txtSystemOut.DoubleClick += new System.EventHandler(this.TxtSystemDoubleClick);
			// 
			// txtMessage
			// 
			this.txtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtMessage.Location = new System.Drawing.Point(80, 233);
			this.txtMessage.Name = "txtMessage";
			this.txtMessage.Size = new System.Drawing.Size(630, 20);
			this.txtMessage.TabIndex = 7;
			this.txtMessage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtMessageKeyPress);
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label4.Location = new System.Drawing.Point(7, 236);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(67, 23);
			this.label4.TabIndex = 7;
			this.label4.Text = "Message : ";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.Location = new System.Drawing.Point(535, 19);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 23);
			this.label3.TabIndex = 5;
			this.label3.Text = "Send to (IP) : ";
			// 
			// txtOutOutput
			// 
			this.txtOutOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtOutOutput.Location = new System.Drawing.Point(7, 69);
			this.txtOutOutput.Multiline = true;
			this.txtOutOutput.Name = "txtOutOutput";
			this.txtOutOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtOutOutput.Size = new System.Drawing.Size(784, 156);
			this.txtOutOutput.TabIndex = 6;
			this.txtOutOutput.WordWrap = false;
			// 
			// txtClientPort
			// 
			this.txtClientPort.Location = new System.Drawing.Point(80, 17);
			this.txtClientPort.Maximum = new decimal(new int[] {
									65000,
									0,
									0,
									0});
			this.txtClientPort.Name = "txtClientPort";
			this.txtClientPort.Size = new System.Drawing.Size(120, 20);
			this.txtClientPort.TabIndex = 4;
			this.txtClientPort.Value = new decimal(new int[] {
									8080,
									0,
									0,
									0});
			this.txtClientPort.ValueChanged += new System.EventHandler(this.TxtClientPortValueChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(6, 19);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(68, 23);
			this.label2.TabIndex = 0;
			this.label2.Text = "Port : ";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(12, 15);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(74, 23);
			this.label11.TabIndex = 6;
			this.label11.Text = "Systems dir :";
			// 
			// txtRepertoireSystemes
			// 
			this.txtRepertoireSystemes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtRepertoireSystemes.Location = new System.Drawing.Point(92, 12);
			this.txtRepertoireSystemes.Name = "txtRepertoireSystemes";
			this.txtRepertoireSystemes.ReadOnly = true;
			this.txtRepertoireSystemes.Size = new System.Drawing.Size(717, 20);
			this.txtRepertoireSystemes.TabIndex = 99;
			this.txtRepertoireSystemes.TabStop = false;
			this.txtRepertoireSystemes.DoubleClick += new System.EventHandler(this.TxtRepertoireSystemesDoubleClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(821, 604);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.txtRepertoireSystemes);
			this.Controls.Add(this.grpClient);
			this.Controls.Add(this.grpServer);
			this.Name = "MainForm";
			this.Text = "CHEKS TEST APPLICATION";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.grpServer.ResumeLayout(false);
			this.grpServer.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtListenerPort)).EndInit();
			this.grpClient.ResumeLayout(false);
			this.grpClient.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtClientPort)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.TextBox txtRepertoireSystemes;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox txtSystemOut;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtSystemIn;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown txtClientPort;
		private System.Windows.Forms.TextBox txtOutOutput;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtIPAddress;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtMessage;
		private System.Windows.Forms.Button btnSend;
		private System.Windows.Forms.GroupBox grpClient;
		private System.Windows.Forms.NumericUpDown txtListenerPort;
		private System.Windows.Forms.Button btnListenerStop;
		private System.Windows.Forms.Button btnListenerStart;
		private System.Windows.Forms.TextBox txtInOutput;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox grpServer;
	}
}
