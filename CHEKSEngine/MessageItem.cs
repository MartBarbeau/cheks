using System;
using System.Xml;

namespace CHEKS
{
	namespace CHEKSEngine
	{
		/// <summary>
		/// <para>CHEKS message item ibject. Used to manipulate the contents of a message.</para>
		/// </summary>
		public class MessageItem
		{
			#region --- Attributs ---
			/// <summary>
			/// <para>Validation of the system id</para>
			/// </summary>
			public string SystemCheck;
			/// <summary>
			/// <para>Content of the message</para>
			/// </summary>
			public string MessageContent;
			/// <summary>
			/// <para>Requested action</para>
			/// </summary>
			public string Request;
			#endregion
	
			#region --- Constructeur ---
			/// <summary>
			/// <para>Constructor building a message item with the specified parameters.</para>
			/// </summary>
			/// <param name="systemCheck">System id that should be used to access the message.</param>
			/// <param name="messageContent">Text content of the message.</param>
			public MessageItem(string systemCheck, string messageContent)
			{
				this.SystemCheck = systemCheck;
				this.MessageContent = messageContent;
				this.Request = "";
			}
			
			/// <summary>
			/// <para>Constructor building a message item from the specified serialization string.</para>
			/// </summary>
			/// <param name="serialization">Serialization string to be used to build the object.</param>
			public MessageItem(string serialization)
			{
                string sysCheck = "<systemCheck>";
                string msContent = "<messageContent>";
                string request = "<request>";

                if (string.IsNullOrEmpty(serialization))
                {
                    this.SystemCheck = "";
                    this.MessageContent = "";
                }
                else
                {
                    /*
                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(serialization);
                    if (xml.ChildNodes.Item(0).ChildNodes.Count > 0) {
                        this.SystemCheck = xml.ChildNodes.Item(0).ChildNodes.Item(0).InnerText;
                        this.MessageContent = xml.ChildNodes.Item(0).ChildNodes.Item(1).InnerText;
                        if (xml.ChildNodes.Item(0).ChildNodes.Count == 3) {
                            this.Request = xml.ChildNodes.Item(0).ChildNodes.Item(2).InnerText;
                     * }
                     * */

                    /*
                     * Parser XML custom pour contrer l'injection XML, à refactorer!
                     * 
                     */
                    if (serialization.Contains(sysCheck))//TODO: voir robustesse
                    {
                        try
                        {
                            int indexStartSys = serialization.IndexOf(sysCheck) + sysCheck.Length;
                            int indexEndSys = serialization.IndexOf("</systemCheck>");
                            this.SystemCheck =
                                serialization.Substring(indexStartSys, indexEndSys - indexStartSys);
                            int indexStartMs = serialization.IndexOf(msContent) + msContent.Length;
                            int indexEndMs = serialization.LastIndexOf("</messageContent>");
                            this.MessageContent =
                                serialization.Substring(indexStartMs, indexEndMs - indexStartMs);
                            if (serialization.Contains(request))
                            {
                                int indexStartRq = serialization.LastIndexOf(request) + request.Length;
                                int indexEndRq = serialization.LastIndexOf("</request>");
                                this.Request =
                                    serialization.Substring(indexStartRq, indexEndRq - indexStartRq);
                            }
                        }
                        catch (Exception)
                        {

                            this.SystemCheck = null;
                            this.MessageContent = null;
                            this.Request = null;
                        }

                    }

                }
			}
			
			/// <summary>
			/// <para>Constructor building a message item from the specified sxml node.</para>
			/// </summary>
			/// <param name="node">Node to be used to build the object.</param>
			public MessageItem(XmlNode node)
			{
					if (node.ChildNodes.Count > 1) {
						this.SystemCheck = node.ChildNodes.Item(0).InnerText;
						this.MessageContent = node.ChildNodes.Item(1).InnerText;
						this.Request = "";
						
						if (node.ChildNodes.Count == 3) {
							this.Request = node.ChildNodes.Item(2).InnerText;
						}
					}
			}
			#endregion
			
			#region --- Méthodes ---
			/// <summary>
			/// <para>Serialization of the message item</para>
			/// </summary>
			/// <returns>Serialization string for the message item</returns>
			public string Serialize()
			{
				return "<item><systemCheck>" + this.SystemCheck + "</systemCheck><messageContent>" + this.MessageContent + "</messageContent>" + (this.Request == "" ? "" : "<request>" + this.Request) + "</request>" + "</item>";
			}
			#endregion
		}
	}
}