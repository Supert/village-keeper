namespace VillageKeeper.Data
{
    public class CommonData : BindedData
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

        public CommonData(string prefix)
        {
            InitDataFields(prefix);
        }
    }
}