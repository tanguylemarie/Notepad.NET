using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Notepad.Objects
{
    public class Session
    {
        private const string FILENAME = "session.xml";

        private static string _applicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static string _applicationPath = Path.Combine(_applicationDataPath, "Notepad.NET");

        private readonly XmlWriterSettings _writterSettings;

        /// <summary>
        /// Chemin d'accés et nom du fichier représentant la session.
        /// </summary>
        public string FileName { get; } = Path.Combine(_applicationPath, FILENAME);

        [XmlAttribute(AttributeName = "ActiveIndex")]
        public int ActiveIndex { get; set; } = 0;

        [XmlElement(ElementName = "File")]
        public List<TextFile> TextFiles { get; set; }

        public Session()
        {
            TextFiles = new List<TextFile>();

            _writterSettings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = ("\t"),
                OmitXmlDeclaration = true
            };

            if (!Directory.Exists(_applicationPath))
            {
                Directory.CreateDirectory(_applicationPath);
            }
        }

        public void Save()
        {
            var emptyNamespace = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var serializer = new XmlSerializer(typeof(Session));

            using (XmlWriter writer = XmlWriter.Create(FileName, _writterSettings))
            {
                serializer.Serialize(writer, this, emptyNamespace);
            }
        }
    }
}
