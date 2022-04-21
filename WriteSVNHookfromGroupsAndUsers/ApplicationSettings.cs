using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SVNHookGenerator
{
    internal class ApplicationSettings
    {
        public string Settingsfile { get; set; }
        public string RepositoriesPath { get; set; }
        public string PostHookfileName { get; set; }
        public string AccessCSVFileName { get; set; }
        public string AplicationDirectory { get; set; }
    }
}
