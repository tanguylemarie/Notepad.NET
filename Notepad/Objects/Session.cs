using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notepad.Objects
{
    public class Session
    {
        private const string FILENAME = "session.xml";

        private static string _applicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static string _applicationPath = Path.Combine(_applicationDataPath, "Notepad.NET");

        /// <summary>
        /// Chemin d'accés et nom du fichier représentant la session.
        /// </summary>
        public string FileName { get; } = Path.Combine(_applicationPath, FILENAME);

        public int ActiveIndex { get; set; } = 0;

        public List<TextFile> TextFiles { get; set; }

        public Session()
        {
            TextFiles = new List<TextFile>();
        }
    }
}
