using UnityEngine;
using Shibari;

namespace VillageKeeper.Model
{
    public class SavedData : BindableData
    {
        [SerializeValue]
        public AssignableValue<SerializableBuilding[]> Buildings { get; private set; }

        [SerializeValue]
        public AssignableValue<int> VillageLevel { get; private set; }
        [SerializeValue, ShowInEditor]
        public AssignableValue<int> Gold { get; private set; }

        [SerializeValue]
        public AssignableValue<bool> HasPremium { get; private set; }

        [SerializeValue]
        public AssignableValue<bool> WasBuildTipShown { get; private set; }
        [SerializeValue]
        public AssignableValue<bool> WasBattleTipShown { get; private set; }


        [SerializeValue]
        public AssignableValue<int> MonstersDefeated { get; private set; }

        [SerializeValue]
        public AssignableValue<bool> IsSoundEffectsEnabled { get; private set; }
        [SerializeValue]
        public AssignableValue<bool> IsMusicEnabled { get; private set; }

        public void SaveData()
        {
            PlayerPrefs.Save();
        }
    }
}