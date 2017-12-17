using System;
using System.Xml.Serialization;

namespace VillageKeeper.Data
{
    [Serializable]
    public class SerializableBuilding
    {
        public BuildingTypes type;
        public int x;
        public int y;

        public SerializableBuilding()
        {
        }

        public SerializableBuilding(BuildingTypes type, int x, int y)
        {
            this.type = type;
            this.x = x;
            this.y = y;
        }
    }
}