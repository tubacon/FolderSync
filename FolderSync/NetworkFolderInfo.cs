using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderSync
{
    public class NetworkFolderInfo
    {
        public required string SourceDirectory { get; set; }
        public required string SourceUsername { get; set; }
        public required string SourcePassword { get; set; }
        public required string TargetDirectory { get; set; }
        public required string TargetUsername { get; set; }
        public required string TargetPassword { get; set; }

        public static class Network
        {
            public static void ConnectToShare(string directory, string username, string password)
            {
                Console.WriteLine($"Connecting to {directory} with username {username}");
            }
        }
    }

   
}
