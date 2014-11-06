using System;
using System.Collections.Generic;

namespace CHEKS
{
	namespace CHEKSEngine
	{
		/// <summary>
		/// <para>Utility methods to help during the system generation and initialization.</para>
		/// </summary>
	    public static class Utilitaires
	    {
	    	#region --- Attributs ---
	    	private static Random rnd = new Random();
	    	#endregion
	    	
			#region --- Méthodes pour nombres & données aléatoires ---
			/// <summary>
			/// <para>Return a random integer in the specified range.</para>
			/// </summary>
			/// <param name="minRange">Minimum integer to return.</param>
			/// <param name="maxRange">Maximum integer to return</param>
			/// <returns>A random integer in the range.</returns>
			public static int getRandomInt(int minRange, int maxRange)
			{
				return rnd.Next(minRange, maxRange+1);
			}

			/// <summary>
			/// <para>Get a random agent in the provided agent names list.</para>
			/// </summary>
			/// <param name="listeAgents">List containing the name of the agents</param>
			/// <returns>A random agent name.</returns>
			public static string getRandomAgent(List<string> listeAgents) {
				return listeAgents[getRandomInt(0, listeAgents.Count-1)];
			}

			/// <summary>
			/// <para>Random true or false.</para>
			/// </summary>
			/// <returns>True or False</returns>
			public static bool QuarterShot()
			{
				if (getRandomInt(1, 2) == 1) {
					return true;
				} else {
					return false;
				}
			}

			/// <summary>
			/// <para>Random 1 out of 12 true or false.</para>
			/// </summary>
			/// <returns>True or false.</returns>
			public static bool DoubleDiceShot()
			{
				if (getRandomInt(1, 12) == 1) {
					return true;
				} else {
					return false;
				}
			}
			#endregion
	    }
	}
}

