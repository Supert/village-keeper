using UnityEngine;

namespace VillageKeeper.Data
{
    public class ResourceData : BindedData
    {
        public BindableField<string> CastleBackground { get; private set; }
        public BindableField<string> MountainsBackground { get; private set; }
        public BindableField<string> VillageBackground { get; private set; }
        public BindableField<string> Cliff { get; private set; }
        public BindableField<string> CastleUpgradeIcon { get; private set; }

        public ResourceData(string id)
        {
            Register(id);
            CastleBackground.Set("Background/Castle/{0}/{1}");
            MountainsBackground.Set("Background/Mountains/{0}");
            VillageBackground.Set("Background/Village/{0}");
            Cliff.Set("UI/BattleMode/Cliff/{0}");
            CastleUpgradeIcon.Set("UI/BuildMode/CastleUpgradeIcon/{0}");
        }
    }

    public class SavedData : BindedData
    {
        public BuildingsDataField Buildings { get; private set; }

        public IntDataField VillageLevel { get; private set; }
        public IntDataField Gold { get; private set; }

        public BoolDataField HasPremium { get; private set; }

        public BoolDataField WasBuildTipShown { get; private set; }
        public BoolDataField WasBattleTipShown { get; private set; }


        public IntDataField MonstersDefeated { get; private set; }

        public BoolDataField IsSoundEffectsEnabled { get; private set; }
        public BoolDataField IsMusicEnabled { get; private set; }

        public SavedData(string id)
        {
            Register(id);
            if (Buildings.Get() == null)
                Buildings.Set(new SerializableBuildingsList());
        }

        public void SaveData()
        {
            PlayerPrefs.Save();
        }
    }
}