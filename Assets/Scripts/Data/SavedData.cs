using UnityEngine;
using Shibari;

namespace VillageKeeper.Data
{
    public class SavedData : BindableData
    {
        public SerializableField<SerializableBuildingsList> Buildings { get; private set; }

        public SerializableField<int> VillageLevel { get; private set; }
        public SerializableField<int> Gold { get; private set; }

        public SerializableField<bool> HasPremium { get; private set; }

        public SerializableField<bool> WasBuildTipShown { get; private set; }
        public SerializableField<bool> WasBattleTipShown { get; private set; }


        public SerializableField<int> MonstersDefeated { get; private set; }

        public SerializableField<bool> IsSoundEffectsEnabled { get; private set; }
        public SerializableField<bool> IsMusicEnabled { get; private set; }

        public override void Init(string id)
        {
            if (Buildings.Get() == null)
                Buildings.Set(new SerializableBuildingsList());
        }

        public void SaveData()
        {
            PlayerPrefs.Save();
        }
    }
}