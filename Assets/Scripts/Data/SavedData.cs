using UnityEngine;
using Shibari;

namespace VillageKeeper.Model
{
    public class SavedData : BindableData
    {
        [SerializeValue]
        public PrimaryValue<SerializableBuilding[]> Buildings { get; private set; }

        [SerializeValue]
        public PrimaryValue<int> VillageLevel { get; private set; }
        [SerializeValue, ShowInEditor]
        public PrimaryValue<int> Gold { get; private set; }

        [SerializeValue]
        public PrimaryValue<bool> HasPremium { get; private set; }

        [SerializeValue]
        public PrimaryValue<bool> WasBuildTipShown { get; private set; }
        [SerializeValue]
        public PrimaryValue<bool> WasBattleTipShown { get; private set; }


        [SerializeValue]
        public PrimaryValue<int> MonstersDefeated { get; private set; }

        [SerializeValue]
        public PrimaryValue<bool> IsSoundEffectsEnabled { get; private set; }
        [SerializeValue]
        public PrimaryValue<bool> IsMusicEnabled { get; private set; }

        public void SaveData()
        {
            PlayerPrefs.Save();
        }
    }
}