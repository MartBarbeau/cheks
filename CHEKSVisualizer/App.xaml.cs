using System;
using System.Windows;
using System.Data;
using System.Xml;
using System.Configuration;

namespace CHEKS
{
	namespace CHEKSVisualizer
	{
		/// <summary>
		/// Interaction logic for App.xaml
		/// </summary>
		public partial class App : Application
		{
	        protected override void OnStartup(StartupEventArgs e)
	        {
	        	try {
					if(e.Args.Length == 1) {
		        		Visualizer vis = new Visualizer(e.Args[0]);
		        		vis.Show();
	        		} else {
	        			Application.Current.Shutdown();
	        		}
	        	} catch (Exception ex) {
	        		MessageBox.Show(ex.ToString(), "Erreur dans CHEKSVisualizer", MessageBoxButton.OK, MessageBoxImage.Error);
	        	}
	
	        }
		}
	}
}