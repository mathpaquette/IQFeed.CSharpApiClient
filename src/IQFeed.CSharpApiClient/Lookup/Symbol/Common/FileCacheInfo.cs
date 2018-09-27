using System;
using System.Xml.Serialization;

namespace IQFeed.CSharpApiClient.Lookup.Symbol.Common
{
    [Serializable]
    public class FileCacheInfo
    {
        private FileCacheInfo()
        {
            
        }

        public FileCacheInfo(DateTime lastModificationTimestamp, string checksum)
        {
            LastModificationTimestamp = lastModificationTimestamp;
            Checksum = checksum;
        }
        
        public DateTime LastModificationTimestamp { get; set; }
        public string Checksum { get; set; }

        /// <summary>
        /// Saves to an xml file
        /// </summary>
        /// <param name="filename">File path of the new xml file</param>
        public void Save(string filename)
        {
            using (var writer = new System.IO.StreamWriter(filename))
            {
                var serializer = new XmlSerializer(this.GetType());
                serializer.Serialize(writer, this);
                writer.Flush();
            }
        }

        /// <summary>
        /// Load an object from an xml file
        /// </summary>
        /// <param name="FileName">Xml file name</param>
        /// <returns>The object created from the xml file</returns>
        public static FileCacheInfo Load(string filename)
        {
            using (var stream = System.IO.File.OpenRead(filename))
            {
                var serializer = new XmlSerializer(typeof(FileCacheInfo));
                return serializer.Deserialize(stream) as FileCacheInfo;
            }
        }
    }
}