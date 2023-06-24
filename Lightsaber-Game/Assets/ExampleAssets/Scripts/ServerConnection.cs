using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Renci.SshNet;
using System.IO;
using Renci.SshNet.Sftp;
using System;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine.Events;

namespace ExampleAssets.Scripts
{
    public class ServerConnection : MonoBehaviour
    {
        string host = "ftp-n2.cs.technikum-wien.at";
        string username = "innoapp";
        string password = "jGv9t^F5Nun*X6i4$97@";
        string serverFilePath = "/sftp/Lightsaber";
        string localFilePath;

        public UnityEvent OnFilesDownloaded = new UnityEvent();

        private void Start()
        {
            if (!Directory.Exists(Application.persistentDataPath + "/LevelFiles"))
                Directory.CreateDirectory(Application.persistentDataPath + "/LevelFiles");

            localFilePath = Application.persistentDataPath + "/LevelFiles";
        }

        public async Task UpdateFilesFromServer()
        {
            List<string> filesOnServer = await Task.Run(() => SFTPUtils.GetFilesFromFTPDirectory(host, username, password, serverFilePath));
            List<string> localFiles = Directory.GetFiles(Application.persistentDataPath + "/LevelFiles", "*" + ".json").ToList();

            for (int i = localFiles.Count - 1; i >= 0; i--)
            {
                string localFileName = Path.GetFileName(localFiles[i]);
                if (!filesOnServer.Contains(localFileName))
                {
                    File.Delete(localFiles[i]);
                    localFiles.RemoveAt(i);
                }
                else
                    localFiles[i] = localFileName;
            }

            foreach (string fileOnServer in filesOnServer)
            {
                if (!localFiles.Contains(fileOnServer))
                {
                    bool downloaded = await SFTPUtils.DownloadFile(host, username, password, serverFilePath + "/" + fileOnServer, localFilePath + "/" + fileOnServer);
                    if (!downloaded)
                        Debug.LogError("downloading file: " + fileOnServer + " failed");
                }
            }

            OnFilesDownloaded?.Invoke();
        }

        public async Task test()
        {
            //if (!Directory.Exists(Application.persistentDataPath + "/LevelFiles"))
            //{
            //    Directory.CreateDirectory(Application.persistentDataPath + "/LevelFiles");
            //}
            this.localFilePath = Application.persistentDataPath + "/LevelFiles";
            await UpdateFilesFromServer();
        }
    }
}


