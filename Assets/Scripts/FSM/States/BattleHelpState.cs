namespace VillageKeeper.FSM
{
    public class BattleHelpState : State
    {
        public override States Type { get { return States.BattleHelp; } }

        public override void Enter()
        {
            base.Enter();
        }

        public override States Event(StateMachineEvents type, params object[] args)
        {
            if (type == StateMachineEvents.HideHelp)
                return States.Battle;
            return base.Event(type, args);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}