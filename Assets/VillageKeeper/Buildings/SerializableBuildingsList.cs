using System;
using System.Collections.Generic;
using System.Xml.Serialization;
namespace AssemblyCSharp
{
    [XmlRoot("BuildingsList")]
    [XmlInclude(typeof(SerializableBuilding))]
    public class SerializableBuildingsList
    {
        [XmlArray("Buildings")]
        public List<SerializableBuilding> list = new List<SerializableBuilding>();

    }

    [XmlType("SerializableBuilding")]
    public class SerializableBuilding
    {
        [XmlElement("BuildingType")]
        public BuildingScript.BuildingTypes Type;
        [XmlElement("X")]
        public int X;
        [XmlElement("Y")]
        public int Y;
        public SerializableBuilding()
        {
        }
        public SerializableBuilding(BuildingScript.BuildingTypes type, int x, int y)
        {
            Type = type;
            X = x;
            Y = y;
        }
    }
}

