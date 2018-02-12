using VillageKeeper.Model;

namespace VillageKeeper.FSM
{
    public class BattleState : State
    {
        public override States Type { get { return States.Battle; } }

        public override void Enter()
        {
            base.Enter();
            if (!Core.Data.Saved.WasBattleTipShown)
            {
                Core.Instance.FSM.Event(StateMachineEvents.ShowHelp);
                Core.Data.Saved.WasBattleTipShown.Set(true);
            }
        }

        public override States Event(StateMachineEvents type, params object[] args)
        {
            if (type == StateMachineEvents.ShowHelp)
                return States.BattleHelp;
            if (type == StateMachineEvents.Pause)
                return States.Pause;
            if (type == StateMachineEvents.RoundFinished)
                return States.RoundFinished;
            return base.Event(type, args);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}