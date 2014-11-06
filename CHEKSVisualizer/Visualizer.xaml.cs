using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.IO;
using CHEKS.CHEKSEngine;
using System.Collections;
using WPFChart3D;

namespace CHEKS
{
	namespace CHEKSVisualizer
	{
		public partial class Visualizer : Window
		{
			#region --- Attributs ---
			CASystem cas = new CASystem();
			
	        // Classe de transformation pour rotation du modèle
	        public WPFChart3D.TransformMatrix m_transformMatrix = new WPFChart3D.TransformMatrix();
	
	        // Modèle 3d
	        private WPFChart3D.Chart3D m_3dChart;       // Données
	        public int m_nChartModelIndex = -1;         // Index pour le Viewport3d
	
	        // Rectangle pour la sélection
	        ViewportRect m_selectRect = new ViewportRect();
	        public int m_nRectModelIndex = -1;
			#endregion
			
			#region --- Constructeurs ---
			public Visualizer()
			{
				this.Close();
			}
			
			public Visualizer(string fileArg)
			{
				InitializeComponent();
	
	            m_selectRect.SetRect(new Point(-0.5, -0.5), new Point(-0.5, -0.5));
	            WPFChart3D.Model3D model3d = new WPFChart3D.Model3D();
	            ArrayList meshs = m_selectRect.GetMeshes();
	            m_nRectModelIndex = model3d.UpdateModel(meshs, null, m_nRectModelIndex, this.mainViewport);
				
				if (File.Exists(fileArg)) {
					LoadCas(fileArg);
				} else {
					this.Close();
				}
	            
	            /*
				//Define a rotation
				RotateTransform3D myRotateTransform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), 1));
				Vector3DAnimation myVectorAnimation = new Vector3DAnimation(new Vector3D(-1, -1, -1), new Duration(TimeSpan.FromMilliseconds(5000)));
				myVectorAnimation.RepeatBehavior = RepeatBehavior.Forever;
				*/
			}
			#endregion
	
			#region --- Méthodes ---
			private void LoadCas(string fileArg)
			{
				this.statusPane.Text = DateTime.Now.ToString("hh:mm:ss");
				
				try {
					this.cas = new CASystem();
					this.cas.Load(fileArg);
					
					BuildSurfacePlot(cas);
				} catch (Exception e) {
		    		MessageBox.Show("Fichier de système CHEKS invalide... Veuillez essayer un autre fichier ou en générer un nouveau.\n\r\n\r" + e.ToString(),
		    		                "Erreur lors de l'ouverture du fichier",
		    		                MessageBoxButton.OK, 
		    		                MessageBoxImage.Error);
					
					this.Close();
				}
	
			}
			
	        public void BuildSurfacePlot(CASystem cas)
	        {
				int nombreRelations = 0;
				
				// Construire le graphique...
				foreach (Agent a in this.cas.Agents.Values) {
					nombreRelations += a.getCurrentLogic().Relations.Count;
				}
				
				this.lblSysteme.Content = String.Format("Système : {0}", cas.Name);
				this.lblAgents.Content = String.Format("Agents : {0}", cas.Agents.Count.ToString());
				this.lblRelations.Content = String.Format("Relations : {0}", nombreRelations.ToString());
				
				Agent[] agents = new Agent[cas.Agents.Count];
				cas.Agents.Values.CopyTo(agents, 0);
	
	        	
				int nXNo = (int)Math.Sqrt(cas.Agents.Count);
	            int nYNo = nXNo;
	            
	            // 1. Définir la surface de la grille
	            m_3dChart = new UniformSurfaceChart3D();
	            ((UniformSurfaceChart3D)m_3dChart).SetGrid(nXNo, nYNo, -100, 100, -100, 100);
	
	            // 2. Définir la valeur z de la surface pour chaque agent
	            int nVertNo = m_3dChart.GetDataNo();
	            for (int i = 0; i < nVertNo; i++)
	            {
	               m_3dChart[i].z = agents[i].Level;
	            }
	            m_3dChart.GetDataRange();
	
	            // 3. Définir la couleur de la surface selon la valeur de z
	            double zMin = m_3dChart.ZMin();
	            double zMax = m_3dChart.ZMax();
	            for (int i = 0; i < nVertNo; i++)
	            {
	                Vertex3D vert = m_3dChart[i];
	                double h = (vert.z - zMin) / (zMax - zMin);
	
	                Color color = WPFChart3D.TextureMapping.PseudoColor(h);
	                m_3dChart[i].color = color;
	            }
	
	            // 4. Obtenir l'array Mesh3D de la surface
	            ArrayList meshs = ((UniformSurfaceChart3D)m_3dChart).GetMeshes();
	            
	            // 6. Définir le modèle d'affichage pour la surface
	            WPFChart3D.Model3D model3d = new WPFChart3D.Model3D();
	            Material backMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.Gray));
	            m_nChartModelIndex = model3d.UpdateModel(meshs, backMaterial, m_nChartModelIndex, this.mainViewport);
	
	            // 7. Définir la matrice de projection pour que les données soient dans la région affichées
	            float xMin = m_3dChart.XMin();
	            float xMax = m_3dChart.XMax();
	            m_transformMatrix.CalculateProjectionMatrix(xMin, xMax, xMin, xMax, zMin, zMax, 0.5);
	            
	            // 8. Go!
	            TransformChart();
	        }
	        
	        // Retourner, dragger et zoomer sur le modèle
	        private void TransformChart()
	        {
	            if (m_nChartModelIndex == -1) return;
	            ModelVisual3D visual3d = (ModelVisual3D)(this.mainViewport.Children[m_nChartModelIndex]);
	            if (visual3d.Content == null) return;
	            Transform3DGroup group1 = visual3d.Content.Transform as Transform3DGroup;
	            group1.Children.Clear();
	            group1.Children.Add(new MatrixTransform3D(m_transformMatrix.m_totalMatrix));
	        }
			#endregion
			
			#region --- Évènements ---
	        public void OnViewportMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs args)
	        {
	            Point pt = args.GetPosition(mainViewport);
	            if (args.ChangedButton == MouseButton.Left)         // rotate or drag 3d model
	            {
	                m_transformMatrix.OnLBtnDown(pt);
	            }
	            else if (args.ChangedButton == MouseButton.Right)   // select rect
	            {
	                m_selectRect.OnMouseDown(pt, mainViewport, m_nRectModelIndex);
	            }
	        }
	
	        public void OnViewportMouseMove(object sender, System.Windows.Input.MouseEventArgs args)
	        {
	            Point pt = args.GetPosition(mainViewport);
	
	            if (args.LeftButton == MouseButtonState.Pressed)                // rotate or drag 3d model
	            {
	                m_transformMatrix.OnMouseMove(pt, mainViewport);
	
	                TransformChart();
	            }
	            else if (args.RightButton == MouseButtonState.Pressed)          // select rect
	            {
	                m_selectRect.OnMouseMove(pt, mainViewport, m_nRectModelIndex);
	            }
	            else
	            {
	                /*
	                String s1;
	                Point pt2 = m_transformMatrix.VertexToScreenPt(new Point3D(0.5, 0.5, 0.3), mainViewport);
	                s1 = string.Format("Screen:({0:d},{1:d}), Predicated: ({2:d}, H:{3:d})", 
	                    (int)pt.X, (int)pt.Y, (int)pt2.X, (int)pt2.Y);
	                this.statusPane.Text = s1;
	                */
	            }
	        }
	
	        public void OnViewportMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs args)
	        {
	            Point pt = args.GetPosition(mainViewport);
	            if (args.ChangedButton == MouseButton.Left)
	            {
	                m_transformMatrix.OnLBtnUp();
	            }
	            else if (args.ChangedButton == MouseButton.Right)
	            {
	                if (m_nChartModelIndex == -1) return;
	                // 1. get the mesh structure related to the selection rect
	                MeshGeometry3D meshGeometry = WPFChart3D.Model3D.GetGeometry(mainViewport, m_nChartModelIndex);
	                if (meshGeometry == null) return;
	              
	                // 2. set selection in 3d chart
	                m_3dChart.Select(m_selectRect, m_transformMatrix, mainViewport);
	
	                // 3. update selection display
	                m_3dChart.HighlightSelection(meshGeometry, Color.FromRgb(200, 200, 200));
	            }
	        }
	
	        // zoom in 3d display
	        public void OnKeyDown(object sender, System.Windows.Input.KeyEventArgs args)
	        {
	            m_transformMatrix.OnKeyDown(args);
	            TransformChart();
	        }
	        
			void btnChangerPlan_Click(object sender, RoutedEventArgs e)
			{
				m_transformMatrix.Rotate(new Point(100, 1000), mainViewport);
	            TransformChart();
	            
	            btnChangerPlan.IsEnabled = false;
			}
			
			void btnIMG_Click(object sender, RoutedEventArgs e)
			{
				RenderTargetBitmap rtb = new RenderTargetBitmap((int)this.Width, (int)this.Height, 96, 96, PixelFormats.Pbgra32);
				rtb.Render(this);
				
				PngBitmapEncoder png = new PngBitmapEncoder();
				png.Frames.Add(BitmapFrame.Create(rtb));
				using (Stream stm = File.Create((btnChangerPlan.IsEnabled == false? "PLAN_" : "") + this.statusPane.Text.Replace(":", "") + ".png"))
				{
				   png.Save(stm);
				}
			}
			#endregion
		}
	}
}