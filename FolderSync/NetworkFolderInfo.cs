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
    }
}
