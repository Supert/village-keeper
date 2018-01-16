using UnityEngine;
using Shibari;

namespace VillageKeeper.Data
{
    public class SavedData : BindableData
    {
        [SerializeValue]
        public BindableField<SerializableBuilding[]> Buildings { get; private set; }

        [SerializeValue]
        public BindableField<int> VillageLevel { get; private set; }
        [SerializeValue]
        public BindableField<int> Gold { get; private set; }

        [SerializeValue]
        public BindableField<bool> HasPremium { get; private set; }

        [SerializeValue]
        public BindableField<bool> WasBuildTipShown { get; private set; }
        [SerializeValue]
        public BindableField<bool> WasBattleTipShown { get; private set; }


        [SerializeValue]
        public BindableField<int> MonstersDefeated { get; private set; }

        [SerializeValue]
        public BindableField<bool> IsSoundEffectsEnabled { get; private set; }
        [SerializeValue]
        public BindableField<bool> IsMusicEnabled { get; private set; }

        public void SaveData()
        {
            PlayerPrefs.Save();
        }
    }
}