using Serializer.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Serializer
{
    public class SerializerService : ISerializer
    {
        public void Serialize<T>(ref List<T> trails, string path, int index)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));

            using (TextWriter tw = new StreamWriter($@"{path}\ListOfTrails{index}.xml"))
            {
                serializer.Serialize(tw, trails);
            }
            Console.WriteLine("Saved");
        }

        public List<T> Deserialize<T>(int index, string path, string name)
        {
            List<T> trails = new List<T>();
            using (var document = XmlReader.Create($"{path}/{name}{index}.xml"))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(List<T>));

                trails = (List<T>)deserializer.Deserialize(document);
            }
            return trails;
        }

        public XmlDocument DeserializeXmlDocument(string path, string name)
        {
            XmlDocument trasa;
            using (var document =  XmlReader.Create($@"{path}/{name}.gpx"))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(XmlDocument));

                trasa = ((XmlDocument)deserializer.Deserialize(document));
            }
            return trasa;
        }
    }
}
