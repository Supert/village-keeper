using System.Linq;
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
    public class GameData : BindableData
    {
        public CalculatedValue<int> RoundFinishedBonusGold { get; } = new CalculatedValue<int>(
            () =>
            {
                if (Core.Data.Common.FsmState == FSM.States.RoundFinished)
                {
                    int villageLevel = Core.Data.Saved.VillageLevel;
                    SerializableBuilding[] buildings = Core.Data.Saved.Buildings;
                    int farms = buildings?.Count(c => c.Type == BuildingTypes.Farm) ?? 0;
                    int windmills = buildings?.Count(c => c.Type == BuildingTypes.Windmill) ?? 0;
                    return Core.Data.Balance.CalculateRoundFinishedBonusGold(villageLevel, farms, windmills);
                }
                return 0;
            },
            Core.Data.Common.FsmState);

        [ShowInEditor]
        public AssignableValue<float> ClampedMonsterHealth { get; } = new AssignableValue<float>();

        [ShowInEditor]
        public AssignableValue<float> ClampedArrowForce { get; } = new AssignableValue<float>();

        public CalculatedValue<bool> IsArrowForceOverThreshold { get; }

        public GameData()
        {
            IsArrowForceOverThreshold = new CalculatedValue<bool>(() => ClampedArrowForce >= Core.Data.Balance.ArrowForceThreshold, ClampedArrowForce, Core.Data.Balance.ArrowForceThreshold);
        }

        [ShowInEditor]
        protected void OnSoundEffectsButtonClicked()
        {
            Core.Data.Saved.IsSoundEffectsEnabled.Set(!Core.Data.Saved.IsSoundEffectsEnabled.Get());
        }

        [ShowInEditor]
        protected void OnMusicButtonClicked()
        {
            Core.Data.Saved.IsMusicEnabled.Set(!Core.Data.Saved.IsMusicEnabled.Get());
        }
    }
}