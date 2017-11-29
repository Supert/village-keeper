using System;

namespace VillageKeeper.FSM
{
    public class InitState : State
    {
        public override States Type { get { return States.Init; } }

        public override void Enter()
        {
            base.Enter();
        }

        public override States Event(StateMachineEvents type, params object[] args)
        {
            if (type == StateMachineEvents.GameInitialized)
                return States.Menu;
            return base.Event(type, args);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}