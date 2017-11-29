namespace VillageKeeper.FSM
{
    public class BattleState : State
    {
        public override States Type { get { return States.Battle; } }

        public override void Enter()
        {
            base.Enter();
            if (!CoreScript.Instance.Data.WasInBattleTipShown)
            {
                CoreScript.Instance.FSM.Event(StateMachineEvents.ShowBattleHelp);
                CoreScript.Instance.Data.WasInBattleTipShown = true;
            }
        }

        public override States Event(StateMachineEvents type, params object[] args)
        {
            if (type == StateMachineEvents.ShowBattleHelp)
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