using Shibari;

namespace VillageKeeper.Model
{
    public class HelpMenu : BindableData
    {
        public AssignableValue<int> CurrentHelpTipIndex { get; } = new AssignableValue<int>();

        [ShowInEditor]
        public CalculatedValue<string> CurrentTip { get; }

        [ShowInEditor]
        public CalculatedValue<string> CurrentTipCounter { get; }
        
        [ShowInEditor]
        public CalculatedValue<bool> IsPreviousTipButtonInteractable { get; }

        [ShowInEditor]
        public CalculatedValue<bool> IsNextTipButtonInteractable { get; }

        public HelpMenu()
        {
            Core.Data.Common.FsmState.OnValueChanged += () => CurrentHelpTipIndex.Set(0);
            IsPreviousTipButtonInteractable = new CalculatedValue<bool>(() => CurrentHelpTipIndex > 0, CurrentHelpTipIndex);
            IsNextTipButtonInteractable = new CalculatedValue<bool>(() => CurrentHelpTipIndex + 1 < (Core.Data.Localization.CurrentTips.Get()?.Length ?? 0), Core.Data.Localization.CurrentTips, CurrentHelpTipIndex);

            CurrentTipCounter = new CalculatedValue<string>(
            () =>
            {
                int totalTips = 0;
                if (Core.Data.Common.FsmState.Get() == FSM.States.BattleHelp)
                    totalTips = Core.Data.Localization.BattleHelpTips.Get().Length;
                if (Core.Data.Common.FsmState.Get() == FSM.States.BuildHelp)
                    totalTips = Core.Data.Localization.BuildHelpTips.Get().Length;
                return string.Format(Core.Data.Localization.TipCounterFormat, CurrentHelpTipIndex + 1, totalTips);
            },
            Core.Data.Common.FsmState,
            Core.Data.Localization.BattleHelpTips,
            Core.Data.Localization.BuildHelpTips,
            Core.Data.Localization.TipCounterFormat,
            CurrentHelpTipIndex);

            CurrentTip = new CalculatedValue<string>(
               () => Core.Data.Localization.CurrentTips.Get()?[CurrentHelpTipIndex] ?? "",
               CurrentHelpTipIndex,
               Core.Data.Localization.CurrentTips);
        }

        [ShowInEditor]
        protected void NextHelpTip()
        {
            CurrentHelpTipIndex.Set(CurrentHelpTipIndex + 1);
        }

        [ShowInEditor]
        protected void PreviousHelpTip()
        {
            CurrentHelpTipIndex.Set(CurrentHelpTipIndex - 1);
        }
    }
}