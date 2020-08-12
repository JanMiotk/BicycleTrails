using System.Collections.Generic;
using System.Xml;

namespace Serializer.Interfaces
{
    public interface ISerializer
    {
        List<T> Deserialize<T>(int indeks, string scierzka, string nazwa);
        XmlDocument DeserializeXmlDocument(string scierzkaDoGpx, string nazwa);
        void Serialize<T>(ref List<T> TrasyZTraseo, string PathForTrails, int indeks);
    }
}