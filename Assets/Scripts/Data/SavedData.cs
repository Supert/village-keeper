using UnityEngine;
using Shibari;

namespace VillageKeeper.Model
{
    public class SavedData : BindableData
    {
        [SerializeValue]
        public AssignableValue<SerializableBuilding[]> Buildings { get; } = new AssignableValue<SerializableBuilding[]>();

        [SerializeValue]
        public AssignableValue<int> VillageLevel { get; } = new AssignableValue<int>();
        [SerializeValue, ShowInEditor]
        public AssignableValue<int> Gold { get; } = new AssignableValue<int>();

        [SerializeValue]
        public AssignableValue<bool> HasPremium { get; } = new AssignableValue<bool>();

        [SerializeValue]
        public AssignableValue<bool> WasBuildTipShown { get; } = new AssignableValue<bool>();
        [SerializeValue]
        public AssignableValue<bool> WasBattleTipShown { get; } = new AssignableValue<bool>();


        [SerializeValue]
        public AssignableValue<int> SlainedMonstersCount { get; } = new AssignableValue<int>();

        [SerializeValue]
        public AssignableValue<bool> IsSoundEffectsEnabled { get; } = new AssignableValue<bool>();
        [SerializeValue]
        public AssignableValue<bool> IsMusicEnabled { get; } = new AssignableValue<bool>();

        public void SaveData()
        {
            PlayerPrefs.Save();
        }
    }
}