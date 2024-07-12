using System;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace FolderSync
{
    public class Operations
    {
        public void CopyNetworkFolders(NetworkFolderInfo networkFolderInfo)
        {
            //connect to source and get folders
            Network.ConnectToShare(networkFolderInfo.SourceDirectory, networkFolderInfo.SourceUsername, networkFolderInfo.SourcePassword);

            //get folders
            string[] folders = Directory.GetDirectories(networkFolderInfo.SourceDirectory);

            //connect to target
            Network.ConnectToShare(networkFolderInfo.TargetDirectory, networkFolderInfo.TargetUsername, networkFolderInfo.TargetPassword);

            Console.WriteLine("Connection is successfull.");
            Console.WriteLine(folders.Length + " folders found.");

            foreach (string f in folders)
            {
                string[] folderSplit = f.Split('\\');
                string folderName = folderSplit[folderSplit.Length - 1];
                string targetDirectory = networkFolderInfo.TargetDirectory + "\\" + folderName;
              //  GetDirectoryInfoAndCopy(f, targetdirectory);
            }

            Network.DisconnectFromShare(networkFolderInfo.SourceDirectory, true);
        }

    }
}
