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
        string host = "127.0.0.1"; //"ftp-n2.cs.technikum-wien.at";
        string username = "inno-user"; //"innoapp";
        string password = "password"; //"jGv9t^F5Nun*X6i4$97@";
        string serverFilePath = "/Lightsaber"; //"/sftp/Lightsaber";
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
            System.IO.DirectoryInfo di = new DirectoryInfo(Application.persistentDataPath + "/LevelFiles");
            FileInfo[] localFiles = di.GetFiles();
            foreach (FileInfo file in localFiles)
            {
                file.Delete();
            }

            try
            {
                List<string> filesOnServer = await Task.Run(() => SFTPUtils.GetFilesFromFTPDirectory(host, username, password, serverFilePath));

                foreach (string fileOnServer in filesOnServer)
                {
                    Debug.Log(localFilePath);
                    bool downloaded = await SFTPUtils.DownloadFile(host, username, password, serverFilePath + "/" + fileOnServer, localFilePath + "/" + fileOnServer);
                    if (!downloaded)
                        Debug.LogError("downloading file: " + fileOnServer + " failed");
                }
                
            }
            catch(System.Exception e)
            {
                Debug.LogException(e);
                return;
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


