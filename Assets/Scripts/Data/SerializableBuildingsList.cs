using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace VillageKeeper.Data
{
    [XmlRoot("BuildingsList")]
    [XmlInclude(typeof(SerializableBuilding))]
    public class SerializableBuildingsList
    {
        [XmlArray("Buildings")]
        public List<SerializableBuilding> list = new List<SerializableBuilding>();
    }
}