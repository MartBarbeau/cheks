namespace CHEKS
{
	namespace CHEKSEngineInitializer
	{
		partial class MainForm
		{
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
				this.components = new System.ComponentModel.Container();
				this.lblNombreAgents = new System.Windows.Forms.Label();
				this.lblMinRelations = new System.Windows.Forms.Label();
				this.lblMaxRelations = new System.Windows.Forms.Label();
				this.lblNiveauMinimum = new System.Windows.Forms.Label();
				this.lblNimeauMaximum = new System.Windows.Forms.Label();
				this.txtNombreAgents = new System.Windows.Forms.NumericUpDown();
				this.txtMinimumRelations = new System.Windows.Forms.NumericUpDown();
				this.txtMaximumRelations = new System.Windows.Forms.NumericUpDown();
				this.txtNiveauMinimum = new System.Windows.Forms.NumericUpDown();
				this.txtNiveauMaximum = new System.Windows.Forms.NumericUpDown();
				this.grpParametresSystemiques = new System.Windows.Forms.GroupBox();
				this.chkchkUtiliserGUID = new System.Windows.Forms.CheckBox();
				this.chkAffichage = new System.Windows.Forms.CheckBox();
				this.txtImpactMaximum = new System.Windows.Forms.NumericUpDown();
				this.lblImpactMaximum = new System.Windows.Forms.Label();
				this.lblDelaiMaximum = new System.Windows.Forms.Label();
				this.txtDelaiMaximum = new System.Windows.Forms.NumericUpDown();
				this.grpActions = new System.Windows.Forms.GroupBox();
				this.btnVisualiserCAS = new System.Windows.Forms.Button();
				this.btnOuvrir = new System.Windows.Forms.Button();
				this.btnEnregistrer = new System.Windows.Forms.Button();
				this.btnReinitialiser = new System.Windows.Forms.Button();
				this.btnGenerer = new System.Windows.Forms.Button();
				this.wbAffichage = new System.Windows.Forms.WebBrowser();
				this.grpPlay = new System.Windows.Forms.GroupBox();
				this.btnPlayAndShow = new System.Windows.Forms.Button();
				this.btnPlay = new System.Windows.Forms.Button();
				this.txtActivation = new System.Windows.Forms.TextBox();
				this.btnDesactiver = new System.Windows.Forms.Button();
				this.btnActiver = new System.Windows.Forms.Button();
				this.txtInterval = new System.Windows.Forms.NumericUpDown();
				this.lblInterval = new System.Windows.Forms.Label();
				this.tmrActivation = new System.Windows.Forms.Timer(this.components);
				((System.ComponentModel.ISupportInitialize)(this.txtNombreAgents)).BeginInit();
				((System.ComponentModel.ISupportInitialize)(this.txtMinimumRelations)).BeginInit();
				((System.ComponentModel.ISupportInitialize)(this.txtMaximumRelations)).BeginInit();
				((System.ComponentModel.ISupportInitialize)(this.txtNiveauMinimum)).BeginInit();
				((System.ComponentModel.ISupportInitialize)(this.txtNiveauMaximum)).BeginInit();
				this.grpParametresSystemiques.SuspendLayout();
				((System.ComponentModel.ISupportInitialize)(this.txtImpactMaximum)).BeginInit();
				((System.ComponentModel.ISupportInitialize)(this.txtDelaiMaximum)).BeginInit();
				this.grpActions.SuspendLayout();
				this.grpPlay.SuspendLayout();
				((System.ComponentModel.ISupportInitialize)(this.txtInterval)).BeginInit();
				this.SuspendLayout();
				// 
				// lblNombreAgents
				// 
				this.lblNombreAgents.Location = new System.Drawing.Point(12, 22);
				this.lblNombreAgents.Name = "lblNombreAgents";
				this.lblNombreAgents.Size = new System.Drawing.Size(100, 23);
				this.lblNombreAgents.TabIndex = 0;
				this.lblNombreAgents.Text = "Nombre d\'agents :";
				// 
				// lblMinRelations
				// 
				this.lblMinRelations.Location = new System.Drawing.Point(12, 49);
				this.lblMinRelations.Name = "lblMinRelations";
				this.lblMinRelations.Size = new System.Drawing.Size(120, 23);
				this.lblMinRelations.TabIndex = 1;
				this.lblMinRelations.Text = "Minimum de relations :";
				// 
				// lblMaxRelations
				// 
				this.lblMaxRelations.Location = new System.Drawing.Point(12, 76);
				this.lblMaxRelations.Name = "lblMaxRelations";
				this.lblMaxRelations.Size = new System.Drawing.Size(120, 23);
				this.lblMaxRelations.TabIndex = 2;
				this.lblMaxRelations.Text = "Maximum de relations :";
				// 
				// lblNiveauMinimum
				// 
				this.lblNiveauMinimum.Location = new System.Drawing.Point(12, 103);
				this.lblNiveauMinimum.Name = "lblNiveauMinimum";
				this.lblNiveauMinimum.Size = new System.Drawing.Size(100, 23);
				this.lblNiveauMinimum.TabIndex = 3;
				this.lblNiveauMinimum.Text = "Niveau minimum :";
				// 
				// lblNimeauMaximum
				// 
				this.lblNimeauMaximum.Location = new System.Drawing.Point(12, 130);
				this.lblNimeauMaximum.Name = "lblNimeauMaximum";
				this.lblNimeauMaximum.Size = new System.Drawing.Size(100, 23);
				this.lblNimeauMaximum.TabIndex = 4;
				this.lblNimeauMaximum.Text = "Niveau maximum :";
				// 
				// txtNombreAgents
				// 
				this.txtNombreAgents.Location = new System.Drawing.Point(138, 20);
				this.txtNombreAgents.Maximum = new decimal(new int[] {
												1000,
												0,
												0,
												0});
				this.txtNombreAgents.Name = "txtNombreAgents";
				this.txtNombreAgents.Size = new System.Drawing.Size(120, 20);
				this.txtNombreAgents.TabIndex = 6;
				this.txtNombreAgents.Value = new decimal(new int[] {
												5,
												0,
												0,
												0});
				// 
				// txtMinimumRelations
				// 
				this.txtMinimumRelations.Location = new System.Drawing.Point(138, 47);
				this.txtMinimumRelations.Maximum = new decimal(new int[] {
												1000,
												0,
												0,
												0});
				this.txtMinimumRelations.Name = "txtMinimumRelations";
				this.txtMinimumRelations.Size = new System.Drawing.Size(120, 20);
				this.txtMinimumRelations.TabIndex = 7;
				this.txtMinimumRelations.Value = new decimal(new int[] {
												1,
												0,
												0,
												0});
				// 
				// txtMaximumRelations
				// 
				this.txtMaximumRelations.Location = new System.Drawing.Point(138, 74);
				this.txtMaximumRelations.Maximum = new decimal(new int[] {
												1000,
												0,
												0,
												0});
				this.txtMaximumRelations.Name = "txtMaximumRelations";
				this.txtMaximumRelations.Size = new System.Drawing.Size(120, 20);
				this.txtMaximumRelations.TabIndex = 8;
				this.txtMaximumRelations.Value = new decimal(new int[] {
												3,
												0,
												0,
												0});
				// 
				// txtNiveauMinimum
				// 
				this.txtNiveauMinimum.Location = new System.Drawing.Point(138, 101);
				this.txtNiveauMinimum.Maximum = new decimal(new int[] {
												1000,
												0,
												0,
												0});
				this.txtNiveauMinimum.Name = "txtNiveauMinimum";
				this.txtNiveauMinimum.Size = new System.Drawing.Size(120, 20);
				this.txtNiveauMinimum.TabIndex = 9;
				// 
				// txtNiveauMaximum
				// 
				this.txtNiveauMaximum.Location = new System.Drawing.Point(138, 128);
				this.txtNiveauMaximum.Maximum = new decimal(new int[] {
												1000,
												0,
												0,
												0});
				this.txtNiveauMaximum.Name = "txtNiveauMaximum";
				this.txtNiveauMaximum.Size = new System.Drawing.Size(120, 20);
				this.txtNiveauMaximum.TabIndex = 10;
				this.txtNiveauMaximum.Value = new decimal(new int[] {
												20,
												0,
												0,
												0});
				// 
				// grpParametresSystemiques
				// 
				this.grpParametresSystemiques.Controls.Add(this.chkchkUtiliserGUID);
				this.grpParametresSystemiques.Controls.Add(this.chkAffichage);
				this.grpParametresSystemiques.Controls.Add(this.txtImpactMaximum);
				this.grpParametresSystemiques.Controls.Add(this.lblImpactMaximum);
				this.grpParametresSystemiques.Controls.Add(this.lblDelaiMaximum);
				this.grpParametresSystemiques.Controls.Add(this.txtDelaiMaximum);
				this.grpParametresSystemiques.Controls.Add(this.txtNombreAgents);
				this.grpParametresSystemiques.Controls.Add(this.txtNiveauMaximum);
				this.grpParametresSystemiques.Controls.Add(this.lblNombreAgents);
				this.grpParametresSystemiques.Controls.Add(this.txtNiveauMinimum);
				this.grpParametresSystemiques.Controls.Add(this.lblMinRelations);
				this.grpParametresSystemiques.Controls.Add(this.txtMaximumRelations);
				this.grpParametresSystemiques.Controls.Add(this.lblMaxRelations);
				this.grpParametresSystemiques.Controls.Add(this.txtMinimumRelations);
				this.grpParametresSystemiques.Controls.Add(this.lblNiveauMinimum);
				this.grpParametresSystemiques.Controls.Add(this.lblNimeauMaximum);
				this.grpParametresSystemiques.Location = new System.Drawing.Point(12, 12);
				this.grpParametresSystemiques.Name = "grpParametresSystemiques";
				this.grpParametresSystemiques.Size = new System.Drawing.Size(557, 164);
				this.grpParametresSystemiques.TabIndex = 11;
				this.grpParametresSystemiques.TabStop = false;
				this.grpParametresSystemiques.Text = "Paramètres systémiques";
				// 
				// chkchkUtiliserGUID
				// 
				this.chkchkUtiliserGUID.Location = new System.Drawing.Point(302, 98);
				this.chkchkUtiliserGUID.Name = "chkchkUtiliserGUID";
				this.chkchkUtiliserGUID.Size = new System.Drawing.Size(224, 24);
				this.chkchkUtiliserGUID.TabIndex = 18;
				this.chkchkUtiliserGUID.Text = "Utiliser des GUID pour le nom des agents";
				this.chkchkUtiliserGUID.UseVisualStyleBackColor = true;
				// 
				// chkAffichage
				// 
				this.chkAffichage.Location = new System.Drawing.Point(302, 71);
				this.chkAffichage.Name = "chkAffichage";
				this.chkAffichage.Size = new System.Drawing.Size(134, 24);
				this.chkAffichage.TabIndex = 17;
				this.chkAffichage.Text = "Afficher XML généré";
				this.chkAffichage.UseVisualStyleBackColor = true;
				this.chkAffichage.CheckedChanged += new System.EventHandler(this.ChkAffichageCheckedChanged);
				// 
				// txtImpactMaximum
				// 
				this.txtImpactMaximum.Location = new System.Drawing.Point(428, 20);
				this.txtImpactMaximum.Maximum = new decimal(new int[] {
												1000,
												0,
												0,
												0});
				this.txtImpactMaximum.Name = "txtImpactMaximum";
				this.txtImpactMaximum.Size = new System.Drawing.Size(120, 20);
				this.txtImpactMaximum.TabIndex = 15;
				this.txtImpactMaximum.Value = new decimal(new int[] {
												3,
												0,
												0,
												0});
				// 
				// lblImpactMaximum
				// 
				this.lblImpactMaximum.Location = new System.Drawing.Point(302, 22);
				this.lblImpactMaximum.Name = "lblImpactMaximum";
				this.lblImpactMaximum.Size = new System.Drawing.Size(100, 23);
				this.lblImpactMaximum.TabIndex = 11;
				this.lblImpactMaximum.Text = "Impact maximum :";
				// 
				// lblDelaiMaximum
				// 
				this.lblDelaiMaximum.Location = new System.Drawing.Point(302, 49);
				this.lblDelaiMaximum.Name = "lblDelaiMaximum";
				this.lblDelaiMaximum.Size = new System.Drawing.Size(120, 23);
				this.lblDelaiMaximum.TabIndex = 12;
				this.lblDelaiMaximum.Text = "Délai maximum :";
				// 
				// txtDelaiMaximum
				// 
				this.txtDelaiMaximum.Location = new System.Drawing.Point(428, 47);
				this.txtDelaiMaximum.Maximum = new decimal(new int[] {
												1000,
												0,
												0,
												0});
				this.txtDelaiMaximum.Name = "txtDelaiMaximum";
				this.txtDelaiMaximum.Size = new System.Drawing.Size(120, 20);
				this.txtDelaiMaximum.TabIndex = 16;
				this.txtDelaiMaximum.Value = new decimal(new int[] {
												3,
												0,
												0,
												0});
				// 
				// grpActions
				// 
				this.grpActions.Controls.Add(this.btnVisualiserCAS);
				this.grpActions.Controls.Add(this.btnOuvrir);
				this.grpActions.Controls.Add(this.btnEnregistrer);
				this.grpActions.Controls.Add(this.btnReinitialiser);
				this.grpActions.Controls.Add(this.btnGenerer);
				this.grpActions.Location = new System.Drawing.Point(13, 182);
				this.grpActions.Name = "grpActions";
				this.grpActions.Size = new System.Drawing.Size(556, 84);
				this.grpActions.TabIndex = 12;
				this.grpActions.TabStop = false;
				this.grpActions.Text = "Actions";
				// 
				// btnVisualiserCAS
				// 
				this.btnVisualiserCAS.Enabled = false;
				this.btnVisualiserCAS.Location = new System.Drawing.Point(498, 19);
				this.btnVisualiserCAS.Name = "btnVisualiserCAS";
				this.btnVisualiserCAS.Size = new System.Drawing.Size(53, 53);
				this.btnVisualiserCAS.TabIndex = 4;
				this.btnVisualiserCAS.Text = "État du système";
				this.btnVisualiserCAS.UseVisualStyleBackColor = true;
				this.btnVisualiserCAS.Click += new System.EventHandler(this.BtnVisualiserCASClick);
				// 
				// btnOuvrir
				// 
				this.btnOuvrir.Location = new System.Drawing.Point(252, 19);
				this.btnOuvrir.Name = "btnOuvrir";
				this.btnOuvrir.Size = new System.Drawing.Size(240, 23);
				this.btnOuvrir.TabIndex = 3;
				this.btnOuvrir.Text = "Ouvrir un système";
				this.btnOuvrir.UseVisualStyleBackColor = true;
				this.btnOuvrir.Click += new System.EventHandler(this.BtnOuvrirClick);
				// 
				// btnEnregistrer
				// 
				this.btnEnregistrer.Enabled = false;
				this.btnEnregistrer.Location = new System.Drawing.Point(253, 49);
				this.btnEnregistrer.Name = "btnEnregistrer";
				this.btnEnregistrer.Size = new System.Drawing.Size(240, 23);
				this.btnEnregistrer.TabIndex = 2;
				this.btnEnregistrer.Text = "Enregistrer le système";
				this.btnEnregistrer.UseVisualStyleBackColor = true;
				this.btnEnregistrer.Click += new System.EventHandler(this.BtnEnregistrerClick);
				// 
				// btnReinitialiser
				// 
				this.btnReinitialiser.Enabled = false;
				this.btnReinitialiser.Location = new System.Drawing.Point(7, 49);
				this.btnReinitialiser.Name = "btnReinitialiser";
				this.btnReinitialiser.Size = new System.Drawing.Size(240, 23);
				this.btnReinitialiser.TabIndex = 1;
				this.btnReinitialiser.Text = "Réinitialiser";
				this.btnReinitialiser.UseVisualStyleBackColor = true;
				this.btnReinitialiser.Click += new System.EventHandler(this.BtnReinitialiserClick);
				// 
				// btnGenerer
				// 
				this.btnGenerer.Location = new System.Drawing.Point(6, 19);
				this.btnGenerer.Name = "btnGenerer";
				this.btnGenerer.Size = new System.Drawing.Size(240, 23);
				this.btnGenerer.TabIndex = 0;
				this.btnGenerer.Text = "Générer le système";
				this.btnGenerer.UseVisualStyleBackColor = true;
				this.btnGenerer.Click += new System.EventHandler(this.BtnGenererClick);
				// 
				// wbAffichage
				// 
				this.wbAffichage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
												| System.Windows.Forms.AnchorStyles.Left) 
												| System.Windows.Forms.AnchorStyles.Right)));
				this.wbAffichage.Location = new System.Drawing.Point(13, 272);
				this.wbAffichage.MinimumSize = new System.Drawing.Size(20, 20);
				this.wbAffichage.Name = "wbAffichage";
				this.wbAffichage.Size = new System.Drawing.Size(1001, 179);
				this.wbAffichage.TabIndex = 13;
				// 
				// grpPlay
				// 
				this.grpPlay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
												| System.Windows.Forms.AnchorStyles.Right)));
				this.grpPlay.Controls.Add(this.btnPlayAndShow);
				this.grpPlay.Controls.Add(this.btnPlay);
				this.grpPlay.Controls.Add(this.txtActivation);
				this.grpPlay.Controls.Add(this.btnDesactiver);
				this.grpPlay.Controls.Add(this.btnActiver);
				this.grpPlay.Controls.Add(this.txtInterval);
				this.grpPlay.Controls.Add(this.lblInterval);
				this.grpPlay.Location = new System.Drawing.Point(576, 13);
				this.grpPlay.Name = "grpPlay";
				this.grpPlay.Size = new System.Drawing.Size(438, 253);
				this.grpPlay.TabIndex = 14;
				this.grpPlay.TabStop = false;
				this.grpPlay.Text = "Activer le système";
				// 
				// btnPlayAndShow
				// 
				this.btnPlayAndShow.Enabled = false;
				this.btnPlayAndShow.Location = new System.Drawing.Point(331, 16);
				this.btnPlayAndShow.Name = "btnPlayAndShow";
				this.btnPlayAndShow.Size = new System.Drawing.Size(50, 23);
				this.btnPlayAndShow.TabIndex = 22;
				this.btnPlayAndShow.Text = "Play+";
				this.btnPlayAndShow.UseVisualStyleBackColor = true;
				this.btnPlayAndShow.Click += new System.EventHandler(this.BntPlayAndShowClick);
				// 
				// btnPlay
				// 
				this.btnPlay.Enabled = false;
				this.btnPlay.Location = new System.Drawing.Point(275, 16);
				this.btnPlay.Name = "btnPlay";
				this.btnPlay.Size = new System.Drawing.Size(50, 23);
				this.btnPlay.TabIndex = 21;
				this.btnPlay.Text = "Play";
				this.btnPlay.UseVisualStyleBackColor = true;
				this.btnPlay.Click += new System.EventHandler(this.BtnPlayClick);
				// 
				// txtActivation
				// 
				this.txtActivation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
												| System.Windows.Forms.AnchorStyles.Right)));
				this.txtActivation.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
				this.txtActivation.Location = new System.Drawing.Point(6, 48);
				this.txtActivation.Multiline = true;
				this.txtActivation.Name = "txtActivation";
				this.txtActivation.ScrollBars = System.Windows.Forms.ScrollBars.Both;
				this.txtActivation.Size = new System.Drawing.Size(426, 193);
				this.txtActivation.TabIndex = 20;
				this.txtActivation.WordWrap = false;
				// 
				// btnDesactiver
				// 
				this.btnDesactiver.Enabled = false;
				this.btnDesactiver.Location = new System.Drawing.Point(219, 16);
				this.btnDesactiver.Name = "btnDesactiver";
				this.btnDesactiver.Size = new System.Drawing.Size(50, 23);
				this.btnDesactiver.TabIndex = 19;
				this.btnDesactiver.Text = "Désact";
				this.btnDesactiver.UseVisualStyleBackColor = true;
				this.btnDesactiver.Click += new System.EventHandler(this.BtnDesactiverClick);
				// 
				// btnActiver
				// 
				this.btnActiver.Enabled = false;
				this.btnActiver.Location = new System.Drawing.Point(163, 16);
				this.btnActiver.Name = "btnActiver";
				this.btnActiver.Size = new System.Drawing.Size(50, 23);
				this.btnActiver.TabIndex = 18;
				this.btnActiver.Text = "Activer";
				this.btnActiver.UseVisualStyleBackColor = true;
				this.btnActiver.Click += new System.EventHandler(this.BtnActiverClick);
				// 
				// txtInterval
				// 
				this.txtInterval.Location = new System.Drawing.Point(86, 19);
				this.txtInterval.Maximum = new decimal(new int[] {
												10000,
												0,
												0,
												0});
				this.txtInterval.Name = "txtInterval";
				this.txtInterval.Size = new System.Drawing.Size(71, 20);
				this.txtInterval.TabIndex = 17;
				this.txtInterval.Value = new decimal(new int[] {
												500,
												0,
												0,
												0});
				// 
				// lblInterval
				// 
				this.lblInterval.Location = new System.Drawing.Point(6, 21);
				this.lblInterval.Name = "lblInterval";
				this.lblInterval.Size = new System.Drawing.Size(120, 23);
				this.lblInterval.TabIndex = 16;
				this.lblInterval.Text = "Interval (m/s) :";
				// 
				// tmrActivation
				// 
				this.tmrActivation.Interval = 500;
				this.tmrActivation.Tick += new System.EventHandler(this.TmrActivationTick);
				// 
				// MainForm
				// 
				this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
				this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
				this.ClientSize = new System.Drawing.Size(1026, 463);
				this.Controls.Add(this.grpPlay);
				this.Controls.Add(this.wbAffichage);
				this.Controls.Add(this.grpActions);
				this.Controls.Add(this.grpParametresSystemiques);
				this.Name = "MainForm";
				this.Text = "Initialisateur CHEKS";
				this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
				((System.ComponentModel.ISupportInitialize)(this.txtNombreAgents)).EndInit();
				((System.ComponentModel.ISupportInitialize)(this.txtMinimumRelations)).EndInit();
				((System.ComponentModel.ISupportInitialize)(this.txtMaximumRelations)).EndInit();
				((System.ComponentModel.ISupportInitialize)(this.txtNiveauMinimum)).EndInit();
				((System.ComponentModel.ISupportInitialize)(this.txtNiveauMaximum)).EndInit();
				this.grpParametresSystemiques.ResumeLayout(false);
				((System.ComponentModel.ISupportInitialize)(this.txtImpactMaximum)).EndInit();
				((System.ComponentModel.ISupportInitialize)(this.txtDelaiMaximum)).EndInit();
				this.grpActions.ResumeLayout(false);
				this.grpPlay.ResumeLayout(false);
				this.grpPlay.PerformLayout();
				((System.ComponentModel.ISupportInitialize)(this.txtInterval)).EndInit();
				this.ResumeLayout(false);
			}
			private System.Windows.Forms.Button btnPlayAndShow;
			private System.Windows.Forms.Button btnPlay;
			private System.Windows.Forms.Button btnVisualiserCAS;
			private System.Windows.Forms.CheckBox chkchkUtiliserGUID;
			private System.Windows.Forms.CheckBox chkAffichage;
			private System.Windows.Forms.TextBox txtActivation;
			private System.Windows.Forms.Timer tmrActivation;
			private System.Windows.Forms.Button btnActiver;
			private System.Windows.Forms.Button btnDesactiver;
			private System.Windows.Forms.Label lblInterval;
			private System.Windows.Forms.NumericUpDown txtInterval;
			private System.Windows.Forms.GroupBox grpPlay;
			private System.Windows.Forms.Button btnOuvrir;
			private System.Windows.Forms.NumericUpDown txtDelaiMaximum;
			private System.Windows.Forms.Label lblDelaiMaximum;
			private System.Windows.Forms.Label lblImpactMaximum;
			private System.Windows.Forms.NumericUpDown txtImpactMaximum;
			private System.Windows.Forms.Button btnReinitialiser;
			private System.Windows.Forms.Button btnEnregistrer;
			private System.Windows.Forms.WebBrowser wbAffichage;
			private System.Windows.Forms.Button btnGenerer;
			private System.Windows.Forms.GroupBox grpActions;
			private System.Windows.Forms.GroupBox grpParametresSystemiques;
			private System.Windows.Forms.NumericUpDown txtNiveauMaximum;
			private System.Windows.Forms.NumericUpDown txtNiveauMinimum;
			private System.Windows.Forms.NumericUpDown txtMaximumRelations;
			private System.Windows.Forms.NumericUpDown txtMinimumRelations;
			private System.Windows.Forms.NumericUpDown txtNombreAgents;
			private System.Windows.Forms.Label lblNimeauMaximum;
			private System.Windows.Forms.Label lblNiveauMinimum;
			private System.Windows.Forms.Label lblMaxRelations;
			private System.Windows.Forms.Label lblMinRelations;
			private System.Windows.Forms.Label lblNombreAgents;
		}
	}
}