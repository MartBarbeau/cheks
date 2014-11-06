using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;

namespace CHEKS
{
	namespace CHEKSEngine
	{
		/// <summary>
		/// <para>Main class of the CHEKSEngine. It contains everything needed to Load, Play and Save a system.
		/// It also contains the agents interacting in the system.
		/// </para>
		/// </summary>
		public class CASystem
		{
			#region --- Attributs ---
		    private string name = "";
		    private int currentPlay = 1;
		    private string lastSystemState = "";
		    private Dictionary<string, Agent> agents = new Dictionary<string, Agent>();
			#endregion
			
			#region --- Accesseurs ---
			/// <summary>
			/// <para>Name of the system.</para>
			/// </summary>
			public string Name {
				get {
					return this.name;
				}
			}
			/// <summary>
			/// <para></para>
			/// </summary>
			public int CurrentPlay {
				get {
					return this.currentPlay;
				}
			}
			/// <summary>
			/// <para>List containing all the agent objects contained in the system.</para>
			/// </summary>
			public Dictionary<string, Agent> Agents {
				get {
					return this.agents;
				}
			}
			#endregion
	
			#region --- Constructeurs ---
			/// <summary>
			/// <para>Default constructor. Build an empty system with a generated Guid as a name.</para>
			/// </summary>
			public CASystem()
			{
				this.name = System.Guid.NewGuid().ToString();
			}
			
			/// <summary>
			/// <para>Constructor building a system from a definition contained in a specified file.</para>
			/// </summary>
			/// <param name="fileName">Complete path to the system definition xml file.</param>
			public CASystem(string fileName) {
				this.Load(fileName);
			}
			#endregion
			
			#region --- Méthodes ---
			/// <summary>
			/// <para>Generate a system from the specified parameters</para>
			/// </summary>
			/// <param name="utiliserGUID">If true, agents names will be Guid. Otherwise, it will be a counter. Can be useful to save some disk space.</param>
			/// <param name="nombreAgents">The number of agents interacting in the system.</param>
			/// <param name="minimumRelations">Minimum number of relations an agent should have.</param>
			/// <param name="maximumRelations">Maximum number of relations an agent should have.</param>
			/// <param name="niveauMinimum">Minimum level/Lower limit (numeric representation) an agent should have.</param>
			/// <param name="niveauMaximum">Maximum level/Upper limit (numeric representation) an agent should have.</param>
			/// <param name="impactMaximum">Maximum impact an interaction should have on another agent.</param>
			/// <param name="delaiMaximum">Maximum delai of execution an interaction should have.</param>
			public void GenerateCASystem(bool utiliserGUID, int nombreAgents, int minimumRelations, int maximumRelations, int niveauMinimum, int niveauMaximum, int impactMaximum, int delaiMaximum)
			{
				// Construire la liste des agents (noms seulement...)
				List<string> listeAgents = new List<string>();
				for (int x = 0; x < nombreAgents; x++) {
					listeAgents.Add(utiliserGUID ? System.Guid.NewGuid().ToString() : x.ToString());
				}
				// Initialiser les agents et les ajouter au CASystem
				foreach (string nomAgent in listeAgents) {
					this.agents.Add(nomAgent, new Agent(nomAgent, 
							                 Utilitaires.getRandomInt(niveauMinimum, niveauMaximum), 
							                 niveauMinimum, 
							                 niveauMaximum,
										     Utilitaires.getRandomInt(2, (niveauMaximum-niveauMinimum)), // nombre de logiques pour l'agent
										     minimumRelations,
										     maximumRelations,
								             impactMaximum,
										     delaiMaximum,
							                 listeAgents));
				}
				
				this.lastSystemState = this.Vectorization(false);
			}
			
			/// <summary>
			/// <para>Load a system from a definition contained in a specified file.</para>
			/// </summary>
			/// <param name="fileName">Complete path to the system definition xml file.</param>
			public void Load(string fileName)
			{
	            // Lire le fichier
	            XmlDocument doc = new XmlDocument();
	            
	            doc.Load(fileName);
	            XmlNode casNode = doc.GetElementsByTagName("cas").Item(0);
	            this.lastSystemState = casNode.Attributes["lastSystemState"].Value;
	            
            	this.name = casNode.Attributes.GetNamedItem("name").Value;

	            // Lire les agents, leurs relations, etc...
	            foreach (XmlNode agentNode in doc.GetElementsByTagName("agent")) {
	                Agent newAgent = new Agent(agentNode);
	                this.agents.Add(newAgent.Name, newAgent);
	            }
	            
	            // Valider si il y a la présence d'un fichier "state". Si oui, le charger.
	            if (File.Exists(fileName.Replace(".xml", ".state.xml"))) {
		            XmlDocument docState = new XmlDocument();
		            
	            	docState.Load(fileName.Replace(".xml", ".state.xml"));
	            	
	            	// Lire le contenu, et updater l'agent concerné
		            foreach (XmlNode agentNode in docState.GetElementsByTagName("agent")) {
	            		this.agents[agentNode.Attributes["name"].Value].SetState(agentNode);
		            }
                }
                
		    }
			
			/// <summary>
			/// <para>Save the system, including state and upcoming impacts in an xml file. Save to the default systems directory, with the system name as a fileName.</para>
			/// </summary>
			/// <returns>Returns true if file has been successfully saved.</returns>
			public bool Save()
			{
				return this.Save(Encrypter.Instance.SystemsDirectory, true);
			}
			
			/// <summary>
			/// <para>Save the state of the system, including agents states and upcoming impacts in an xml file. Save to the default systems directory, with the system name as a fileName.</para>
			/// </summary>
			/// <returns>Returns true if file has been successfully saved.</returns>
			public bool SaveState()
			{
				return this.SaveState(Encrypter.Instance.SystemsDirectory, true);
			}
			
			/// <summary>
			/// <para>Save the system, including state and upcoming impacts in an xml file. Save to the specified file.</para>
			/// </summary>
			/// <param name="fileName">Complete file name or file path. If file path, you have to set addIdToPath to true.</param>
			/// <param name="addIdToPath">Specifies if the system must add the system id as a file name.</param>
			/// <returns>Returns true if file has been successfully saved.</returns>
			public bool Save(string fileName, bool addIdToPath = false)
			{
				if (addIdToPath) {
					fileName += "\\" + this.name + ".xml";
				}
				
				try {
					// TODO : Utiliser une base de données ou autre chose... c'est trop lent avec des fichiers
					using (StreamWriter outFile = new StreamWriter(fileName))
			        {
						outFile.Write(this.Serialize());
						outFile.Close();
			        }
					
					this.SaveState(fileName, addIdToPath);
				} catch {
					// TODO : Gestion des erreurs efficace...
					return false;
				}
				
				return true;
			}
			
			/// <summary>
			/// <para>Save the state of the system, including agents states and upcoming impacts in an xml file. Save to the specified file.</para>
			/// </summary>
			/// <param name="fileName">Complete file name or file path. If file path, you have to set addIdToPath to true.</param>
			/// <param name="addIdToPath">Specifies if the system must add the system id as a file name.</param>
			/// <returns>Returns true if file has been successfully saved.</returns>
			public bool SaveState(string fileName, bool addIdToPath = false)
			{
				if (addIdToPath) {
					fileName += "\\" + this.name + ".state.xml";
				}
				
				if (!fileName.EndsWith(".state.xml")) {
					fileName = fileName.Replace(".xml", ".state.xml");
				}
				
				try {
					// TODO : Utiliser une base de données ou autre chose... c'est trop lent avec des fichiers
					using (StreamWriter outFile = new StreamWriter(fileName))
			        {
						outFile.Write(this.SerializeState());
						outFile.Close();
			        }
				} catch {
					// TODO : Gestion des erreurs efficace...
					return false;
				}
				
				return true;
			}
		    
			/// <summary>
			/// <para>Make the system evolve to its next state (Executions of the relations and impact of each agents).</para>
			/// </summary>
		    public void Play ()
		    {
		        // Pour chacun des agents, envoyer les influences selon la carte des actions et updater le niveau selon l'auto-influence
		        foreach (Agent agent in this.agents.Values) {
		            agent.PrepareToPlay(this);
		        }
		        
		        // Pour chacun des agents, comptabiliser le tout
		        foreach (Agent agent in this.agents.Values) {
		            agent.Play(this);
		        }
		    }
			
			/// <summary>
			/// <para>Vectorization of the system.</para>
			/// </summary>
			/// <param name="toChar">Specifies if the vectorization has to be made with ascii chars or numeric values.</param>
			/// <returns>Returns the vectorization of the system (i.e. the state (numeric representation) of each agent, appended in a string. If toChar is set to true, the ascii character will be return if the state is in the range.</returns>
			public string Vectorization(bool toChar)
			{
				string resultat = "";
				
				// TODO : Tester si on aura besoin d'ordonner les agents sur des systèmes différents pour avoir la même clé...
				foreach (Agent agent in this.agents.Values) {
					if (toChar && agent.Level > 32 && agent.Level < 127) { // Limites ascii
						resultat += Convert.ToChar(agent.Level);
					} else {
						resultat += agent.Level;
					}
				}
				
				return resultat;
			}
		    
			/// <summary>
			/// <para>Serialization of the system.</para>
			/// </summary>
			/// <returns>Serialization string for the system.</returns>
		    private string Serialize() 
		    {
		        string s = "<cas name=\"" + this.name + "\" lastSystemState=\"" + this.lastSystemState + "\">" + "\n\r";
		        foreach (KeyValuePair<string, Agent> entry in this.agents) {
		            s += entry.Value.Serialize();
		        }
		        s += "</cas>" + "\n\r";
		        
		        return s;
		    }
		    
			/// <summary>
			/// <para>Serialization of the system.</para>
			/// </summary>
			/// <returns>Serialization string for the system.</returns>
		    private string SerializeState() 
		    {
		        string s = "<casState name=\"" + this.name + "\" lastSystemState=\"" + this.lastSystemState + "\">" + "\n\r";
		        foreach (KeyValuePair<string, Agent> entry in this.agents) {
		            s += entry.Value.SerializeState();
		        }
		        s += "</casState>" + "\n\r";
		        
		        return s;
		    }
			#endregion
		}
	}
}