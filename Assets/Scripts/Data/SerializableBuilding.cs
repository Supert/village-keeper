using System;

namespace VillageKeeper.Model
{
    [Serializable]
    public class SerializableBuilding
    {
        public BuildingTypes Type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

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