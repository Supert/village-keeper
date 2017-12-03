using System;

namespace VillageKeeper.FSM
{
    public class BuildState : State
    {
        public override States Type { get { return States.Build; } }

        public override void Enter()
        {
            base.Enter();
            if (!CoreScript.Instance.SavedData.WasBuildTipShown.Get())
            {
                CoreScript.Instance.FSM.Event(StateMachineEvents.ShowBuildHelp);
                CoreScript.Instance.SavedData.WasBuildTipShown.Set(true);
            }
        }

        public override States Event(StateMachineEvents type, params object[] args)
        {
            if (type == StateMachineEvents.GoToMenu)
                return States.Menu;
            if (type == StateMachineEvents.ShowBuildHelp)
                return States.BuildHelp;
            if (type == StateMachineEvents.GoToBattle)
                return States.Battle;
            return base.Event(type, args);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}