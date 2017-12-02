using System.Xml.Serialization;

namespace VillageKeeper.Data
{
    [XmlType("SerializableBuilding")]
    public class SerializableBuilding
    {
        [XmlElement("BuildingType")]
        public BuildingTypes Type;
        [XmlElement("X")]
        public int X;
        [XmlElement("Y")]
        public int Y;
        public SerializableBuilding()
        {
        }
        public SerializableBuilding(BuildingTypes type, int x, int y)
        {
            Type = type;
            X = x;
            Y = y;
        }
    }
}