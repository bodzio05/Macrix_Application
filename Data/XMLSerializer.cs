using System.IO;
using System.Xml.Serialization;

namespace Macrix_Application.Data
{
    public class XMLSerializer
    {
        public static T DeserializeToObject<T>(string filepath) where T : class
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StreamReader sr = new StreamReader(filepath))
            {
                return (T)serializer.Deserialize(sr);
            }
        }

        public static void SerializeToXml<T>(T anyobject, string xmlFilePath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(anyobject.GetType());
            using (StreamWriter writer = new StreamWriter(xmlFilePath))
            {
                xmlSerializer.Serialize(writer, anyobject);
            }
        }
    }
}
