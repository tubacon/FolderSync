using System;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Diagnostics.Metrics;
using System.Reflection;

namespace FolderSync
{
    public class Operations
    {
        public void CopyNetworkFolders()
        {
            var networkFolderInfo = new NetworkFolderInfo
            {
                SourceDirectory = @"\\Server\Share",
                SourceUsername = "username",
                SourcePassword = "password",
                TargetDirectory = @"\\Server\Share",
                TargetUsername = "username",
                TargetPassword = "password"
            };

            GetFolders(networkFolderInfo);
        }

        private void GetFolders(NetworkFolderInfo networkFolderInfo)
        {
            try
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
                    string folderName = folderSplit[^1]; // C# 8.0
                    string targetDirectory = networkFolderInfo.TargetDirectory + "\\" + folderName;
                    Copy(new DirectoryInfo(f), new DirectoryInfo(targetDirectory));
                }

                Network.DisconnectFromShare(networkFolderInfo.SourceDirectory, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message);
            }
        }

        private void Copy(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);
            Console.WriteLine("Folders are copying.");

            // Copy each file into the new directory
            Parallel.ForEach(source.GetFiles(), fi =>
            {
                var path = target.FullName + @"\" + fi.Name;
                if (File.Exists(path))
                {
                    Console.WriteLine("Copying was not done because there is a file with the same name in the folder : " + fi.Name);
                }
                else
                {
                    fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
                    Console.WriteLine(target.FullName + @"\" + fi.Name);
                }
            });

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                try
                {
                    DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                    Copy(diSourceSubDir, nextTargetSubDir);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error : " + ex.Message);
                }
            }

            Console.WriteLine("File copy successful.");
        }

    }
}
