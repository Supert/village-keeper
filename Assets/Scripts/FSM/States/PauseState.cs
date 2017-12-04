using System;

namespace VillageKeeper.FSM
{
    public class PauseState : State
    {
        public override States Type { get { return States.Pause; } }

        public override void Enter()
        {
            base.Enter();
        }

        public override States Event(StateMachineEvents type, params object[] args)
        {
            if (type == StateMachineEvents.PauseMenuContinue)
                return States.Battle;
            if (type == StateMachineEvents.GoToMenu)
                return States.Menu;

            return base.Event(type, args);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}