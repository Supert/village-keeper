using UnityEngine;

namespace VillageKeeper.Data
{
    public class SerializableBuildingsArray : SerializableArray<SerializableBuilding>
    {
        [SerializeField]
        private SerializableBuilding[] values = new SerializableBuilding[0];

        public override SerializableBuilding[] Values
        {
            get
            {
                return values;
            }
            set
            {
                values = value;
            }
        }
    }
}