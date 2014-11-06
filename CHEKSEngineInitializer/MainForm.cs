using CHEKS.CHEKSEngine; 

using System;
using System.Xml;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace CHEKS
{
	namespace CHEKSEngineInitializer
	{
		public partial class MainForm : Form
		{
			#region --- Attributs ---
			private CASystem cas;
			string fileName = "";
			string originalFileName = "";
			#endregion
			
			#region --- Constructeurs ---
			public MainForm()
			{
				InitializeComponent();
			}
			#endregion
			
			#region --- Évènements ---
			void BtnGenererClick(object sender, EventArgs e)
			{
				if (this.ValiderParametres()) {
					this.GenererCAS();
					
					this.fileName = this.cas.Name + ".xml";
					this.originalFileName = this.fileName;
					
					this.btnGenerer.Enabled = false;
					this.btnEnregistrer.Enabled = true;
					this.btnVisualiserCAS.Enabled = true;
					this.btnReinitialiser.Enabled = true;
					this.btnOuvrir.Enabled = false;
					
					this.btnActiver.Enabled = true;
					this.btnDesactiver.Enabled = false;
					this.btnPlay.Enabled = true;
					this.btnPlayAndShow.Enabled = true;
					this.tmrActivation.Enabled = false;
					this.txtInterval.Enabled = true;
				}
				
			}
			
			void BtnEnregistrerClick(object sender, EventArgs e)
			{
				SaveFileDialog saver = new SaveFileDialog();      
			    saver.InitialDirectory = @"C:\"; 
			    saver.Title = "Enregistrer le système CHEKS"; 
			    saver.CheckFileExists = false; 
			    saver.CheckPathExists = true; 
			    saver.DefaultExt = "xml"; 
			    saver.Filter = "Fichier XML (*.xml)|*.xml"; 
			    saver.FilterIndex = 2; 
			    saver.RestoreDirectory = true;
			    saver.FileName = this.originalFileName;
			
			    if (saver.ShowDialog() == DialogResult.OK) 
			    {
			    	if (this.cas.Save(saver.FileName)) {
			    		this.fileName = saver.FileName; 
			    	} else {
			    		MessageBox.Show("Impossible d'enregistrer le système dans le fichier spécifié.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
			    	}
			    } 
			}
			
			void BtnReinitialiserClick(object sender, EventArgs e)
			{
				this.cas = new CASystem();
				
				this.txtNombreAgents.Text = "0";
				this.txtMinimumRelations.Text = "0";
				this.txtMaximumRelations.Text = "0";
				this.txtNiveauMinimum.Text = "0";
				this.txtNiveauMaximum.Text = "0";
				this.txtImpactMaximum.Text = "0";
				this.txtDelaiMaximum.Text = "0";
				
				this.wbAffichage.Navigate("");
				
				this.btnGenerer.Enabled = true;
				this.btnEnregistrer.Enabled = false;
				this.btnVisualiserCAS.Enabled = false;
				this.btnReinitialiser.Enabled = false;
				this.btnOuvrir.Enabled = true;
				
				this.btnActiver.Enabled = false;
				this.btnDesactiver.Enabled = false;
				this.btnPlay.Enabled = false;
				this.btnPlayAndShow.Enabled = false;
				this.tmrActivation.Enabled = false;
				this.txtInterval.Enabled = true;
				this.txtActivation.Text = "";
				
				this.fileName = "";
				this.originalFileName = "";
			}
			
			void BtnOuvrirClick(object sender, EventArgs e)
			{	
				OpenFileDialog open = new OpenFileDialog();      
			    open.InitialDirectory = @"C:\";
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
			    		this.cas = new CASystem();
			    		this.cas.Load(open.FileName);
			    		
			    		this.fileName = open.FileName;
			    		this.originalFileName = this.fileName;
			    		
			    		this.AfficherCASXML();
			    		
						this.btnGenerer.Enabled = false;
						this.btnEnregistrer.Enabled = true;
						this.btnVisualiserCAS.Enabled = true;
						this.btnReinitialiser.Enabled = true;
						this.btnOuvrir.Enabled = false;
						
						this.btnActiver.Enabled = true;
						this.btnDesactiver.Enabled = false;
						this.btnPlay.Enabled = true;
						this.btnPlayAndShow.Enabled = true;
						this.tmrActivation.Enabled = false;
						this.txtInterval.Enabled = true;
			    	} catch (Exception err) {
			    		MessageBox.Show("Fichier de système CHEKS invalide... Veuillez essayer un autre fichier ou en générer un nouveau.\n\r\n\r" + err.ToString(),
			    		                "Erreur lors de l'ouverture du fichier", 
			    		                MessageBoxButtons.OK, 
			    		                MessageBoxIcon.Error);
			    	}
			    	
			    } 
			}
			
			void BtnActiverClick(object sender, EventArgs e)
			{
				this.tmrActivation.Interval = int.Parse(txtInterval.Text);
				
				this.btnActiver.Enabled = false;
				this.btnDesactiver.Enabled = true;
				this.tmrActivation.Enabled = true;
				this.btnPlay.Enabled = false;
				this.btnPlayAndShow.Enabled = false;
				this.txtInterval.Enabled = false;			
			}
			
			void TmrActivationTick(object sender, EventArgs e)
			{
				string activationText = this.cas.Vectorization(true);
				
				System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
				this.cas.Play();
				sw.Stop();
				
				this.txtActivation.AppendText("(" + sw.ElapsedMilliseconds + " m/s) :: " + activationText);
				this.txtActivation.AppendText(Environment.NewLine);
			}
			
			void BtnDesactiverClick(object sender, EventArgs e)
			{
				this.btnActiver.Enabled = true;
				this.btnDesactiver.Enabled = false;
				this.btnPlay.Enabled = true;
				this.btnPlayAndShow.Enabled = true;
				this.tmrActivation.Enabled = false;
				this.txtInterval.Enabled = true;
				this.AfficherCASXML();
			}
			
			void BtnPlayClick(object sender, EventArgs e)
			{
				this.TmrActivationTick(null, null);
				this.AfficherCASXML();
			}
			
			void ChkAffichageCheckedChanged(object sender, EventArgs e)
			{
				AfficherCASXML();
			}
			
			void BtnVisualiserCASClick(object sender, EventArgs e)
			{
				ProcessStartInfo startInfo = new ProcessStartInfo();
				startInfo.Arguments = "\"" + this.fileName + "\"";
				startInfo.FileName = Path.GetDirectoryName(Application.ExecutablePath) + "\\CHEKSVisualizer.exe";
				if (File.Exists(startInfo.FileName)) {
					Process p = Process.Start(startInfo);
				} else {
					MessageBox.Show("Le visualisateur de système est- introuvable.", "CHEKSVisualizer introuvable", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			
			void BntPlayAndShowClick(object sender, EventArgs e)
			{
				this.BtnPlayClick(null, null);
				this.BtnVisualiserCASClick(null, null);
			}
			#endregion
			
			#region --- Méthodes ---
			public bool ValiderParametres() {
				int x = 0;
				if (!int.TryParse(this.txtNombreAgents.Text, out x)) return false;
				if (!int.TryParse(this.txtMinimumRelations.Text, out x)) return false;
				if (!int.TryParse(this.txtMaximumRelations.Text, out x)) return false;
				if (!int.TryParse(this.txtNiveauMinimum.Text, out x)) return false;
				if (!int.TryParse(this.txtNiveauMaximum.Text, out x)) return false;
				if (!int.TryParse(this.txtImpactMaximum.Text, out x)) return false;
				if (!int.TryParse(this.txtDelaiMaximum.Text, out x)) return false;
				
				if (int.Parse(this.txtNiveauMaximum.Text) < 1) return false;
				if (int.Parse(this.txtNiveauMaximum.Text) < int.Parse(this.txtNiveauMinimum.Text)) return false;
				
				return true;
			}
			
			public void GenererCAS()
			{
				// Objet CASystem qui servira à pour l'initialisation
				cas = new CASystem();
		
				// Variables d'initialisation des agents
				int nombreAgents = int.Parse(this.txtNombreAgents.Text);
				int minimumRelations = int.Parse(this.txtMinimumRelations.Text);
				int maximumRelations = int.Parse(this.txtMaximumRelations.Text);
				int niveauMinimum = int.Parse(this.txtNiveauMinimum.Text);
				int niveauMaximum = int.Parse(this.txtNiveauMaximum.Text);
				int impactMaximum = int.Parse(this.txtImpactMaximum.Text);
				int delaiMaximum = int.Parse(this.txtDelaiMaximum.Text);
		
				this.cas.GenerateCASystem(this.chkchkUtiliserGUID.Checked,
				                          nombreAgents,
				                          minimumRelations, 
				                          maximumRelations, 
				                          niveauMinimum, 
				                          niveauMaximum, 
				                          impactMaximum, 
				                          delaiMaximum);
				
				// Afficher le XML dans wbAffichage
				this.AfficherCASXML();
			}
			
			private void AfficherCASXML()
			{	
				if (this.cas == null) {
					this.wbAffichage.Navigate("");
					return;
				}
				
				string sfileName = Path.Combine(Path.GetTempPath(), "CASystem.xml");
		
				if (this.cas.Save(sfileName)) {
		    		this.fileName = sfileName; 
		    	} else {
					MessageBox.Show("Impossible d'enregistrer le fichier", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
			    }
				
				if (chkAffichage.Checked) {
					wbAffichage.Navigate(sfileName);
				} else {
					this.wbAffichage.Navigate("");
				}
			}
			#endregion
		}
	}
}