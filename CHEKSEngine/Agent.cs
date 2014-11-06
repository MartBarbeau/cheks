using System;
using System.Xml;
using System.Collections.Generic;

namespace CHEKS
{
	namespace CHEKSEngine
	{
		/// <summary>
		/// <para>Class representing an agent in the system. That agent will interact with other agents, through sets of logics containing relations.</para>
		/// </summary>
		public class Agent
		{
			#region --- Attributs ---
		    private string name = "";
		    private int min = 0;
		    private int max = 0;
		    private int level = 0;
		    private int lastLevel = 0;
		    private Dictionary<int, Dictionary<string, int>> influence = new Dictionary<int, Dictionary<string, int>>();
		    private List<Logic> logics= new List<Logic>();
		    private string logicSerializationCache = "";
			#endregion
			
			#region --- Accesseurs ---
			/// <summary>
			/// <para>Name of the agent.</para>
			/// </summary>
			public string Name {
				get {
					return this.name;
				}
			}
			/// <summary>
			/// <para>Minimum level/Lower limit (numeric representation) of the agent.</para>
			/// </summary>
			public int Min {
				get {
					return this.min;
				}
			}
			/// <summary>
			/// <para>Maximum level/Lower limit (numeric representation) of the agent.</para>
			/// </summary>
			public int Max {
				get {
					return this.max;
				}
			}
			/// <summary>
			/// <para>Current level (numeric representation) of the agent.</para>
			/// </summary>
			public int Level {
				get {
					return this.level;
				}
			}
			/// <summary>
			/// <para>Last level (numeric representation) of the agent.</para>
			/// </summary>
			public int LastLevel {
				get {
					return this.lastLevel;
				}
			}
			/// <summary>
			/// <para>List of influences/impacts that are pending for the agent.</para>
			/// </summary>
			public Dictionary<int, Dictionary<string, int>> Influence {
				get {
					return this.influence;
				}
			}
			/// <summary>
			/// <para>List of the logics in the agent.</para>
			/// </summary>
			public List<Logic> Logics {
				get {
					return this.logics;
				}
			}
			#endregion
			
			#region --- Constructeurs ---
			/// <summary>
			/// <para>Constructor building an agent with the specified parameters.</para>
			/// </summary>
			/// <param name="nom">Name of the agent.</param>
			/// <param name="niveau">Initial level of the agent.</param>
			/// <param name="niveauMinimum">Minimum level/Lower limit (numeric representation) of the agent.</param>
			/// <param name="niveauMaximum">Maximum level/Lower limit (numeric representation) of the agent.</param>
			/// <param name="nombreLogiques">Number of different logic the agent will have.</param>
			/// <param name="minimumRelations">Minimum number of relations/interactions in each logic.</param>
			/// <param name="maximumRelations">Maximum number of relations/interactions in each logic.</param>
			/// <param name="impactMaximum">Maximum impact an interaction should have on another agent.</param>
			/// <param name="delaiMaximum">Maximum delai of execution an interaction should have.</param>
			/// <param name="listeAgents">List of the names of the agents in the system. Has to be generated first to allow bounding of relations/interactions.</param>
			public Agent(string nom, int niveau, int niveauMinimum, int niveauMaximum, int nombreLogiques, int minimumRelations, int maximumRelations, int impactMaximum, int delaiMaximum, List<string> listeAgents) {
				this.name = nom;
				this.level = niveau;
				this.lastLevel = level;
				this.min = niveauMinimum;
				this.max = niveauMaximum;

				// Calculer les incréments pour les minimums et maximums des logiques
				int incrementLogique = (int)((niveauMaximum - niveauMinimum) / nombreLogiques);

				// Créer les logiques en partant du minimum de l'agent, vers le maximum, par incrément du nombre de logique spécifié...
				int minimumLogique = niveauMinimum;
				while (minimumLogique < this.max + 1) {
					// Déterminer qu'on a pas dépassé le maximum de l'agent et si c'est le cas, remettre à la valeur min
					int maximumLogique = minimumLogique + incrementLogique-1;
					if (maximumLogique > this.max) {
						maximumLogique = this.max;
					}

					this.logics.Add(new Logic(minimumLogique, 
					                          maximumLogique, 
					                          Utilitaires.getRandomInt(minimumRelations, maximumRelations),
					                          impactMaximum,
					                          delaiMaximum,
					                          listeAgents));
					
					// Incrémenter le minimum
					minimumLogique += incrementLogique;
				}
			}

			/// <summary>
			/// <para>Constructor building a system from a definition contained in an xml node</para>
			/// </summary>
			/// <param name="agentNode">Node containing the agent definition.</param>
			public Agent(XmlNode agentNode)
			{
				this.name = agentNode.Attributes["name"].Value;
		        this.min = int.Parse(agentNode.Attributes["min"].Value);
		        this.max = int.Parse(agentNode.Attributes["max"].Value);
		        this.level = int.Parse(agentNode.Attributes["level"].Value);
		        this.lastLevel = int.Parse(agentNode.Attributes["lastLevel"].Value);
		                
		        foreach (XmlNode childNode in agentNode.ChildNodes) {
		            if (childNode.Name.Equals("logic", StringComparison.OrdinalIgnoreCase)) {
		                this.logics.Add(new Logic(childNode));
		                this.logicSerializationCache += childNode.OuterXml;		                
		            } else if (childNode.Name.Equals("influence", StringComparison.OrdinalIgnoreCase)) {
		        		// Si le tour n'est pas présent dans la matrice d'influence, l'ajouter
		        		if (!this.influence.ContainsKey(int.Parse(childNode.Attributes["round"].Value))) {
		        			this.influence.Add(int.Parse(childNode.Attributes["round"].Value), new Dictionary<string, int>());
		        		}
		        		
		        		// Parcourir les influences et les ajouter à la matrice
		        		foreach (XmlNode impactNode in childNode.ChildNodes) {
		        			this.influence[int.Parse(childNode.Attributes["round"].Value)].Add(impactNode.Attributes["fromAgent"].Value,
		        			                                                                        int.Parse(impactNode.Attributes["value"].Value));
		        		}
		        	}
		        }
			}
			#endregion
			
			#region --- Méthodes ----
			/// <summary>
			/// <para>Get the logic for the current agent's state.</para>
			/// </summary>
			/// <returns>The logic for the current agent's state.</returns>
			public Logic getCurrentLogic() {
		        foreach (Logic logic in this.logics) {
		            if ((logic.Min <= this.level) && (logic.Max >= this.level)) {
		                return logic;
		            }
		        }
		        
		        return null;
		    }
	
			/// <summary>
			/// <para>Get the logic for the specified agent's state.</para>
			/// </summary>
			/// <returns>The logic for the specified agent's state.</returns>
		    public Logic getLogic(int i) {
		        foreach (Logic logic in this.logics) {
		            if ((logic.Min <= i) && (logic.Max >= i)) {
		                return logic;
		            }
		        }
		        
		        return null;
		    }
		    
			/// <summary>
			/// <para>Receive an impact from another agent.</para>
			/// </summary>
			/// <param name="turn">Delai of execution for the impact.</param>
			/// <param name="fromAgent">Name of the agent sending the impact.</param>
			/// <param name="points">Value of the impact (can be positive or negative).</param>
		    public void InfluenceAgent (int turn, string fromAgent, int points) {
				// Vérifier si un tour est présent dans la matrice d'influence
				if (!this.influence.ContainsKey(turn)) {
					this.influence.Add(turn, new Dictionary<string, int>());
				}
		        // Vérifier si une influence provenant du même agent existe déjà , si oui, ajouter au total...
		        if (!this.influence[turn].ContainsKey(fromAgent)) {
		        	this.influence[turn].Add(fromAgent, points);
		        }
		        else {
		        	int tempPoints = this.influence[turn][fromAgent];
		        	this.influence[turn][fromAgent] = tempPoints + points;
		        } 
		    }
		    
			/// <summary>
			/// <para>Send current logic's impacts to other agents.</para>
			/// </summary>
			/// <param name="cas">System containing the other agents.</param>
		    public void PrepareToPlay (CASystem cas) {
		        // Envoyer les influences aux autres agents
		        foreach (Relation relation in this.getCurrentLogic().Relations) {      	
	    			cas.Agents[relation.ToAgent].InfluenceAgent(cas.CurrentPlay + relation.Delay,
	                        this.name, 
	                        (int)(relation.Rate));
		        }
		    }
		    
			/// <summary>
			/// <para>Applies the pending impacts that has been previously received.</para>
			/// </summary>
			/// <param name="cas">System containing the other agents.</param>
		    public void Play(CASystem cas) {
		        // Entrer le niveau actuel dans l'historique
		        this.lastLevel = this.level;
		        // Ajuster l'auto influence
		        this.level += this.getCurrentLogic().Self;

		        if (this.influence.ContainsKey(cas.CurrentPlay)) {
					foreach (KeyValuePair<string, int> entry in this.influence[cas.CurrentPlay]) {
		                this.level += entry.Value;
			        }
	            }
		        
		        // Enlever les éléments périmés de la martrice des influences, et enlever 1 aux tours suivants
		        if (this.influence.ContainsKey(cas.CurrentPlay)) {
		        	this.influence.Remove(cas.CurrentPlay);
		        }
		        Dictionary<int, Dictionary<string, int>> tempInfluence = new Dictionary<int, Dictionary<string, int>>();
		        foreach (KeyValuePair<int, Dictionary<string, int>> tour in this.influence) {
		        	tempInfluence.Add(tour.Key-1, tour.Value);
		        }
		        this.influence = tempInfluence;
		        
		        // Ajuster le niveau de l'agent en fonction des min/max définis, en inversant!
		        if (this.level < this.min) {
		            this.level = this.max;
		        }
		        
		        if (this.level > this.max) {
		            this.level = this.min;
		        }
		    }
		    
			/// <summary>
			/// <para>Set the state and pending impacts of the agent.</para>
			/// </summary>
			public void SetState(XmlNode agentNode)
			{
		        this.level = int.Parse(agentNode.Attributes["level"].Value);
		        this.lastLevel = int.Parse(agentNode.Attributes["lastLevel"].Value);
		                
		        foreach (XmlNode childNode in agentNode.ChildNodes) {
		            if (childNode.Name.Equals("influence", StringComparison.OrdinalIgnoreCase)) {
		        		// Si le tour n'est pas présent dans la matrice d'influence, l'ajouter
		        		if (!this.influence.ContainsKey(int.Parse(childNode.Attributes["round"].Value))) {
		        			this.influence.Add(int.Parse(childNode.Attributes["round"].Value), new Dictionary<string, int>());
		        		}
		        		
		        		// Parcourir les influences et les ajouter à la matrice
		        		foreach (XmlNode impactNode in childNode.ChildNodes) {
		        			this.influence[int.Parse(childNode.Attributes["round"].Value)].Add(impactNode.Attributes["fromAgent"].Value,
		        			                                                                        int.Parse(impactNode.Attributes["value"].Value));
		        		}
		        	}
		        }
			}
			
			/// <summary>
			/// <para>Serialization of the agent.</para>
			/// </summary>
			/// <returns>Serialization string for the agent.</returns>
		    public string Serialize() {
		        string s = "<agent name=\"" + this.name + "\" min=\"" + this.min + "\" max=\"" + this.max + "\" level=\"" + this.level + "\" lastLevel=\"" + this.lastLevel + "\">" + "\n\r";

		        // Logiques, si en cache, utiliser cette version là!
		        if (string.IsNullOrEmpty(this.logicSerializationCache)) {
			        foreach (Logic logic in this.Logics) {
			        	s += logic.Serialize();
			        }
		        } else {
		        	s+= this.logicSerializationCache;
		        }

		        s += "</agent>" + "\n\r";
		        
		        return s;
		    }
			
			/// <summary>
			/// <para>Serialization of the state of the agent.</para>
			/// </summary>
			/// <returns>Serialization string for the state of the agent.</returns>
		    public string SerializeState() {
		        string s = "<agent name=\"" + this.name + "\" level=\"" + this.level + "\" lastLevel=\"" + this.lastLevel + "\">" + "\n\r";

		        // Liste des influences pas encore appliquées
		        foreach (KeyValuePair<int, Dictionary<string, int>> entry in this.influence) {
					s += "<influence round=\"" + entry.Key.ToString() + "\">" + "\n\r";
		            foreach (KeyValuePair<string, int> entryInfluence in ((Dictionary<string, int>)entry.Value)) {
						s += "<impact fromAgent=\"" + entryInfluence.Key + "\" value=\"" + entryInfluence.Value.ToString() + "\" />" + "\n\r";
		            }
		            s += "</influence>" + "\n\r";
		        }

		        s += "</agent>" + "\n\r";
		        
		        return s;
		    }
			#endregion
		}
	}
}