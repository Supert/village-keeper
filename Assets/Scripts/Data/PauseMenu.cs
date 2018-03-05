using Shibari;

namespace VillageKeeper.Model
{
    public class PauseMenu : BindableData
    {
        [ShowInEditor]
        public CalculatedValue<string> CurrentPauseMenuTitle { get; } = new CalculatedValue<string>(
            () =>
            {
                if (Core.Data.Common.FsmState.Get() == FSM.States.Pause)
                    return Core.Data.Localization.Pause;
                if (Core.Data.Common.FsmState.Get() == FSM.States.RoundFinished)
                    return Core.Data.Localization.EndRoundTitle;
                return "";
            },
            Core.Data.Common.FsmState,
            Core.Data.Localization.Pause,
            Core.Data.Localization.EndRoundTitle);

        [ShowInEditor]
        public CalculatedValue<string> PauseMenuBody { get; } = new CalculatedValue<string>(
            () =>
            {
                if (Core.Data.Common.FsmState == FSM.States.Pause)
                    return "";
                else
                    return string.Format(Core.Data.Localization.RoundFinishedBodyFormat, Core.Data.Game.RoundFinishedBonusGold);
            },
            Core.Data.Game.RoundFinishedBonusGold,
            Core.Data.Localization.RoundFinishedBodyFormat);
    }
}