using System;
using System.Windows.Forms;

namespace CHEKS
{
	namespace CHEKSEngineInitializer
	{
		internal sealed class Program
		{
			/// <summary>
			/// Program entry point.
			/// </summary>
			[STAThread]
			private static void Main(string[] args)
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				//Application.Run(new CHEKS.CHEKSEngineInitializer.MainForm());
				Application.Run(new MainForm());
			}
			
		}
	}
}
