using System;
using System.Xml;
using System.Collections.Generic;

namespace CHEKS
{
	namespace CHEKSEngine
	{
		/// <summary>
		/// <para>Logic containing relations/impacts for an agent's given state.</para>
		/// </summary>
		public class Logic
		{
			#region --- Attributs ---
			private int min = 0;
			private int max = 0;
			private int self = 0;
			private List<Relation> relations = new List<Relation>();
			#endregion
			
			#region --- Accessseurs ---
			/// <summary>
			/// <para>Agent's minimum level/Lower limit where the logic/set of relations applies.</para>
			/// </summary>
			public int Min {
				get {
					return this.min;
				}
				set {
					this.min = value;
				}
			}
			/// <summary>
			/// <para>Agent's maximum level/Upper limit where the logic/set of relations applies.</para>
			/// </summary>
			public int Max {
				get {
					return this.max;
				}
				set {
					this.max = value;
				}
			}
			/// <summary>
			/// <para>Auto impact on the agent.</para>
			/// </summary>
			public int Self {
				get {
					return this.self;
				}
				set {
					this.self = value;
				}
			}
			/// <summary>
			/// <para>List of the relation objects contained in the logic.</para>
			/// </summary>
			public List<Relation> Relations {
				get {
					return this.relations;
				}
			}
			#endregion
			
			#region --- Constructeurs ---
			/// <summary>
			/// <para>Constructor building a system from specified parameters.</para>
			/// </summary>
			/// <param name="minimum">Agent's minimum level/Lower limit where the logic/set of relations applies.</param>
			/// <param name="maximum">Agent's maximum level/Upper limit where the logic/set of relations applies.</param>
			/// <param name="nombreRelation">Number of relations/impacts the logic shoud have.</param>
			/// <param name="impactMaximum">Maximum impact an interaction should have on another agent.</param>
			/// <param name="delaiMaximum">Maximum delai of execution an interaction should have.</param>
			/// <param name="listeAgents">List of the names of the agents in the system. Has to be generated first to allow bounding of relations/interactions.</param>
			public Logic(int minimum, int maximum, int nombreRelation, int impactMaximum, int delaiMaximum, List<string> listeAgents)
			{
				this.min = minimum;
				this.max = maximum;
				this.self = Utilitaires.getRandomInt(impactMaximum * -1, impactMaximum);

				// Ajouter les relations
				for (int x = 0; x < nombreRelation; x++) {
					this.relations.Add(new Relation(Utilitaires.getRandomAgent(listeAgents), impactMaximum, delaiMaximum));
				}
			}

			/// <summary>
			/// <para>Constructor building a system from a definition contained in an xml node.</para>
			/// </summary>
			/// <param name="logicNode">Node containing the logic definition.</param>
			public Logic(XmlNode logicNode)
			{
		        this.min = int.Parse(logicNode.Attributes["min"].Value);
		        this.max = int.Parse(logicNode.Attributes["max"].Value);
		        this.self = int.Parse(logicNode.Attributes["self"].Value);
		        
		        foreach (XmlNode subNode in logicNode.ChildNodes) {
		        	if (subNode.Name.Equals("relation", StringComparison.OrdinalIgnoreCase)) {
		                this.relations.Add(new Relation(subNode));
		        	}
		        }
			}		
			#endregion
			
			#region --- Méthodes ---
			/// <summary>
			/// <para>Serialization of the logic.</para>
			/// </summary>
			/// <returns>Serialization string for the logic.</returns>
			public string Serialize()
			{
		        string s = "<logic min=\"" + this.min + "\" max=\"" + this.max + "\" self=\"" + this.self + "\">" + "\n\r";

		        foreach (Relation relation in this.relations) {
		        	s += relation.Serialize();
		        }
		        
		        s += "</logic>" + "\n\r";
		        
		        return s;
			}
			#endregion
		}
	}
}