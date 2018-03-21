using Shibari;

namespace VillageKeeper.Model
{
    public class UiData : BindableData
    {
        public PauseMenu PauseMenu { get; } = new PauseMenu();

        public HelpMenu HelpMenu { get; } = new HelpMenu();

        public BuildingPicker BuildingPicker { get; } = new BuildingPicker();

        public CastleMenu CastleMenu { get; } = new CastleMenu();
    }
}