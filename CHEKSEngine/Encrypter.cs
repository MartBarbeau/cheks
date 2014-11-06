using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace CHEKS
{
	namespace CHEKSEngine
	{
		/// <summary>
		/// <para>Class containing all the encryption and decryption method. Also, this class is a singleton, to prevent that 2 instances use the same systems definition and cause unsync.</para>
		/// </summary>
	    public class Encrypter
	    {
			#region --- Attributs ---
			private static Encrypter encrypter;

			private Dictionary<string, CASystem> caSystems = new Dictionary<string, CASystem>();

			private const int keysize = 256; // Taille de la clé... 256 bits devraient être assez!
			
			private string systemsDirectory = "";
			#endregion

			#region --- Accesseurs ---
			/// <summary>
			/// <para>Get the Encrypter singleton instance.</para>
			/// </summary>
			public static Encrypter Instance {
				get {
					if (encrypter == null) {
						encrypter = new Encrypter();
					}

					return encrypter;
				}
			}
			/// <summary>
			/// <para>Directory containing the systems used for the encryption/decryption.</para>
			/// </summary>
			public string SystemsDirectory {
				get {
					return systemsDirectory;
				}
				set {
					systemsDirectory = value;
				}
			}
			#endregion

			#region --- Constructeurs ---
	        private Encrypter()
	        {
				// Rien à faire
	        }
			#endregion

			#region --- Méthodes ---
			/// <summary>
			/// <para>Encrypt a string using the specified system's linearization.</para>
			/// </summary>
			/// <param name="systemId">Id of the system to be used for encryption.</param>
			/// <param name="plainText">Text to encrypt</param>
			/// <returns>Encrypted string.</returns>
			/// <exception cref="System.Exception">Exceptions will be raised if : the system id is missing or if the system is not found.</exception>
			public string Encrypt(string systemId, string plainText)
			{
				// Validation des paramètres
				if (string.IsNullOrEmpty(systemId)) {
					throw new Exception("Missing system id for encryption");
				}
				
				// Obtenir la clé courante pour le système
				string key = this.getTruncatedVectorization(systemId);
				string linearization = this.getVectorization(systemId);
				
				if (string.IsNullOrEmpty(key)) {
					throw new Exception("Missing key for encryption");;
				} else { // OK, Encrypter
					// Formatter le texte à encrypter en message XML
					plainText = "<systemCheck>" + systemId + "</systemCheck><messageContent>" + plainText + "</messageContent>";
					
					// Convertir le texte à encrypter en tabeau de bytes
					byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
					
					// Encrypter...
					using (PasswordDeriveBytes password = new PasswordDeriveBytes(key, null))
					{
						byte[] keyBytes = password.GetBytes(keysize / 8);
						using (RijndaelManaged symmetricKey = new RijndaelManaged())
						{
							symmetricKey.Mode = CipherMode.CBC;
							symmetricKey.Padding = PaddingMode.Zeros;
							using (ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.UTF8.GetBytes(linearization.Substring(linearization.Length-16))))
							{
								using (MemoryStream memoryStream = new MemoryStream())
								{
									using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
									{
										cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
										cryptoStream.FlushFinalBlock();
										byte[] cipherTextBytes = memoryStream.ToArray();
										return Convert.ToBase64String(cipherTextBytes);
									}
								}
							}
						}
					}
				}
			}

			/// <summary>
			/// <para>Decrypt a string using the specified system's linearization.</para>
			/// </summary>
			/// <param name="systemId">Id of the system to be used for decryption.</param>
			/// <param name="cipherText">Text to decrypt</param>
			/// <returns>Encrypted string.</returns>
			/// <exception cref="System.Exception">Exceptions will be raised if : the cypherText is empty, the system id is missing or if the system is not found.</exception>
			public string Decrypt(string systemId, string cipherText)
			{
				// Validation des paramètres
				if (string.IsNullOrEmpty(systemId)) {
					throw new Exception("Missing system id for decryption");;
				}
				if (string.IsNullOrEmpty(cipherText)) {
					throw new Exception("Nothing to decrypt");;
				}
				
				// Obtenir la clé courante pour le système
				string key = this.getTruncatedVectorization(systemId);
				string linearization = this.getVectorization(systemId);
				
				if (string.IsNullOrEmpty(key)) {
					throw new Exception("Missing key for decryption");;
				} else { // OK, Décrypter
					// Convertir le texte à décrypter en tableau de bytes
					byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
					
					// Décrypter...
					using (PasswordDeriveBytes password = new PasswordDeriveBytes(key, null))
					{
						byte[] keyBytes = password.GetBytes(keysize / 8);
						using(RijndaelManaged symmetricKey = new RijndaelManaged())
						{
							symmetricKey.Mode = CipherMode.CBC;
							symmetricKey.Padding = PaddingMode.Zeros;
							using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.UTF8.GetBytes(linearization.Substring(linearization.Length-16))))
							{
								using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
								{
									using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
									{
										byte[] plainTextBytes = new byte[cipherTextBytes.Length];
										int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
										return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
									}
								}
							}
						}
					}
				}
			}
			
			/// <summary>
			/// <para>Open a system and load it in the cache.</para>
			/// </summary>
			/// <param name="systemId">Id of the system to open.</param>
			/// <returns>True if successful.</returns>
			/// <exception cref="System.Exception">Exception will be raised if the file is corrupted or unloadable.</exception>
			private bool OpenSystem(string systemId)
			{
				if (!this.caSystems.ContainsKey(systemId)) {
					if (File.Exists(systemsDirectory + "\\" + systemId + ".xml")) {
						try {
							CASystem cas = new CASystem(systemsDirectory + "\\" + systemId + ".xml");
							if (cas.Name.Equals(systemId)) {
								this.caSystems.Add(cas.Name, cas);
							}
						} catch {
							return false;
						}
						
						return true;
					} else {
						return false;
					}
				} else {
					return true;
				}
				
			}
			
			/// <summary>
			/// <para>Close a system and clear it from the cache.</para>
			/// </summary>
			/// <param name="systemId">Id of the system to close.</param>
			/// <returns>True if successful.</returns>
			public void CloseSystem(string systemId)
			{
				if (this.caSystems.ContainsKey(systemId)) {
					this.caSystems.Remove(systemId);
				}
			}
			
			/// <summary>
			/// <para>Get linearisation for the specified system.</para>
			/// </summary>
			/// <param name="systemId">Id of the system to get the linearization.</param>
			/// <returns>Linearization string.</returns>
			private string getVectorization(string systemId)
			{
				if (this.OpenSystem(systemId)) {
					return this.caSystems[systemId].Vectorization(true);
				} else {
					return "";
				}
			}

			/// <summary>
			/// <para>Get a truncated linearisation for the specified system (%8).</para>
			/// </summary>
			/// <param name="systemId">Id of the system to get the linearization.</param>
			/// <returns>Linearization string.</returns>
			private string getTruncatedVectorization(string systemId)
			{
				if (!this.OpenSystem(systemId)) {
					 return "";
				}

				// Obtenir la clé
				string key = this.caSystems[systemId].Vectorization(true);

				// Ajuster la longeur de la clé pour qu'elle soit divisible par 8 (byte)
				int keyLength = key.Length - (key.Length % 8);

				// Retourner la clé dans un multiple de 8
				return key.Substring(0, keyLength);
			}
			
			/// <summary>
			/// <para>Make the specified system evolve to its next state and save it.</para>
			/// </summary>
			/// <param name="systemId">Id of the system to make evolve.</param>
			public void Play(string systemId)
			{
				if (!this.OpenSystem(systemId)) {
					 return;
				}
				
				this.caSystems[systemId].Play();

				this.caSystems[systemId].SaveState();
			}
			#endregion
	    }
	}
}

