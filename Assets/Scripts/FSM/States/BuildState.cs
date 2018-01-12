namespace VillageKeeper.FSM
{
    public class BuildState : State
    {
        public override States Type { get { return States.Build; } }

        public override void Enter()
        {
            base.Enter();
            if (!Core.Instance.Data.Saved.WasBuildTipShown.Get())
            {
                Core.Instance.FSM.Event(StateMachineEvents.ShowHelp);
                Core.Instance.Data.Saved.WasBuildTipShown.Set(true);
            }
        }

        public override States Event(StateMachineEvents type, params object[] args)
        {
            if (type == StateMachineEvents.GoToMenu)
                return States.Menu;
            if (type == StateMachineEvents.ShowHelp)
                return States.BuildHelp;
            if (type == StateMachineEvents.GoToBattle)
            {
                Core.Instance.Monster.Init();
                return States.Battle;
            }
            return base.Event(type, args);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}