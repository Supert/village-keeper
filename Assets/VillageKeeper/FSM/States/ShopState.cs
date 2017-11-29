using System;

namespace VillageKeeper.FSM
{
    public class ShopState : State
    {
        public override States Type { get { return States.Shop; } }

        public override void Enter()
        {
            base.Enter();
        }

        public override States Event(StateMachineEvents type, params object[] args)
        {
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