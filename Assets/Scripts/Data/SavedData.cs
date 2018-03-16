using UnityEngine;
using Shibari;

namespace VillageKeeper.Model
{
    public class SavedData : BindableData
    {
        private const string PLAYER_PREFS_LOCATION = "villagekeeper_serialized";
        private const string DEFAULT_SAVED_DATA_LOCATION = "Data/DefaultSavedData";

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

        public void LoadDefaults()
        {
            Deserialize(Resources.Load<TextAsset>(DEFAULT_SAVED_DATA_LOCATION).text);
        }

        public void Load()
        {
            if (!PlayerPrefs.HasKey(PLAYER_PREFS_LOCATION))
                LoadDefaults();
            else
                Deserialize(PlayerPrefs.GetString(PLAYER_PREFS_LOCATION));
        }

        public void Save()
        {
            PlayerPrefs.SetString(PLAYER_PREFS_LOCATION, Serialize());
            PlayerPrefs.Save();
        }
    }
}