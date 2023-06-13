using Renci.SshNet;
using System.Collections.Generic;
using Renci.SshNet.Sftp;
using System.IO;
using System;

public static class SFTPUtils
{
    public static List<string> GetFilesFromFTPDirectory(string host, string username, string password, string path)
    {
        List<string> actualFiles = new List<string>();

        try
        {
            using (var sftpClient = new SftpClient(host, username, password))
            {
                sftpClient.Connect();
                var files = sftpClient.ListDirectory(path);

                foreach (SftpFile file in files)
                {
                    if ((file.Name != ".") && (file.Name != ".."))
                        actualFiles.Add(file.Name);
                }

                sftpClient.Disconnect();
            }
        }
        catch(Exception ex)
        {
            UnityEngine.Debug.Log(ex);
            actualFiles = null;
        }

        return actualFiles;
    }

    public static bool DownloadFile(string host, string username, string password, string serverFilePath, string localFilePath)
    {
        bool success = true;
        try
        {
            using (var sftpClient = new SftpClient(host, username, password))
            {
                sftpClient.Connect();

                using (Stream fileStream = File.Create(localFilePath))
                {
                    sftpClient.DownloadFile(serverFilePath, fileStream);
                }

                sftpClient.Disconnect();
            }
        }
        catch
        {
            success = false;
        }

        return success;
    }

    public static bool UploadFile(string host, string username, string password, string serverFilePath, string localFilePath)
    {
        bool success = true;
        try
        {
            using (var sftpClient = new SftpClient(host, username, password))
            {
                sftpClient.Connect();

                using (Stream fileStream = File.OpenRead(localFilePath))
                {
                    sftpClient.UploadFile(fileStream, serverFilePath);
                }

                sftpClient.Disconnect();
            }
        }
        catch
        {
            success = false;
        }

        return success;
    }

    public static bool DeleteFile(string host, string username, string password, string serverFilePath)
    {
        try
        {
            using (var sftpClient = new SftpClient(host, username, password))
            {
                sftpClient.Connect();
                sftpClient.DeleteFile(serverFilePath);
                sftpClient.Disconnect();
            }

            return true;
        }
        catch
        {
            return false;
        }
    }
}
