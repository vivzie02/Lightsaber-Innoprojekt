using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Renci.SshNet;
using System.IO;

namespace ExampleAssets.Scripts
{
    public class ServerConnection
    {
        private static string jsonFilePath = "..\\..\\StreamingAssets\\test"; //Application.persistentDataPath;


        public void downloadFiles()
        {
            string serverAddress = "ftp-n2.cs.technikum-wien.at";
            string username = "innoapp";
            string password = "jGv9t^F5Nun*X6i4$97@";
            int port = 22; // SFTP default port

            SftpClient client = new SftpClient(serverAddress, port, username, password);

            try
            {
                client.Connect();
                Debug.Log("Hmm");
                if (client.IsConnected)
                {
                    Debug.Log("Test");
                    string remoteFolderPath = "/sftp/Lightsaber";
                    string localFolderPath = jsonFilePath;

                    var files = client.ListDirectory(remoteFolderPath);

                    foreach (var file in files)
                    {
                        string remoteFilePath = remoteFolderPath + "/" + file.Name;
                        string localPath = Path.Combine(localFolderPath, file.Name);

                        using (var fileStream = File.OpenWrite(localPath))
                        {
                            Debug.Log(file.Name);
                            client.DownloadFile(remoteFilePath, fileStream);
                        }
                    }
                }
            }
            finally
            {
                if (client.IsConnected)
                    client.Disconnect();
                client.Dispose();
            }
        }
    }
}

