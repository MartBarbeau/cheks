using System;
using System.Xml;

namespace CHEKS 
{
	namespace CHEKSEngine
	{
		/// <summary>
		/// <para>Class containing a relation and related parameters.</para>
		/// </summary>
		public class Relation
		{
			#region --- Attributs ---
			private string toAgent = "";
			private float rate = 0f;
			private int delay = 0;
			#endregion
			
			#region --- Accesseurs ---
			/// <summary>
			/// <para></para>
			/// </summary>
			public string ToAgent {
				get {
					return this.toAgent;
				}
				set {
					this.toAgent = value;
				}
			}
			/// <summary>
			/// <para></para>
			/// </summary>
			public float Rate {
				get {
					return this.rate;
				}
				set {
					this.rate = value;
				}
			}
			/// <summary>
			/// <para></para>
			/// </summary>
			public int Delay {
				get {
					return this.delay;
				}
				set {
					this.delay = value;
				}
			}
			#endregion
			
			#region --- Constructeurs ---
			/// <summary>
			/// <para>Constructor building a relation with the specified parameters.</para>
			/// </summary>
			/// <param name="agentImpact">Agent that will be impacted by the relation.</param>
			/// <param name="impactMaximum">Maximum impact that will be used in the relation.</param>
			/// <param name="delaiMaximum">Maximum application delay for the impact.</param>
			public Relation(string agentImpact, float impactMaximum, int delaiMaximum)
			{
				this.toAgent = agentImpact;
				this.rate = Utilitaires.getRandomInt((int)impactMaximum * -1, (int)impactMaximum);
				this.delay = Utilitaires.QuarterShot() ? 0 : Utilitaires.getRandomInt(1, delaiMaximum);
			}

			/// <summary>
			/// <para>Constructor building a relation fromh the specified xml node.</para>
			/// </summary>
			/// <param name="relationNode">Node containing the relation definition.</param>
			public Relation(XmlNode relationNode)
			{
				this.toAgent = relationNode.Attributes["toAgent"].Value;
				this.rate = float.Parse(relationNode.Attributes["rate"].Value);
				this.delay = int.Parse(relationNode.Attributes["delay"].Value);
			}
			#endregion
			
			#region --- Méthodes ---
			/// <summary>
			/// <para>Serialization of the relation.</para>
			/// </summary>
			/// <returns>Serialization string for the relation.</returns>
			public string Serialize()
			{
				return "<relation toAgent=\"" + this.toAgent + "\" rate=\"" + this.rate.ToString() + "\" delay=\"" + this.delay.ToString() + "\" />" + "\n\r";;
			}
			#endregion
		}
	}
}