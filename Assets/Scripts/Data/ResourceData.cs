namespace VillageKeeper.Data
{
    public class ResourceData : BindedData
    {
        public BindableField<string> CastleBackground { get; private set; }
        public BindableField<string> MountainsBackground { get; private set; }
        public BindableField<string> VillageBackground { get; private set; }
        public BindableField<string> Cliff { get; private set; }
        public BindableField<string> CastleUpgradeIcon { get; private set; }
        public BindableField<string> ArrowBar { get; private set; }

        public ResourceData(string id)
        {
            Register(id);
            CastleBackground.Set("Background/Castle/{0}/{1}");
            MountainsBackground.Set("Background/Mountains/{0}");
            VillageBackground.Set("Background/Village/{0}");
            Cliff.Set("UI/BattleMode/Cliff/{0}");
            CastleUpgradeIcon.Set("UI/BuildMode/CastleUpgradeIcons/{0}");
            ArrowBar.Set("UI/BattleMode/ArrowBar/{0}");
        }
    }
}