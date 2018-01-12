namespace VillageKeeper.FSM
{
    public class MenuState : State
    {
        public override States Type { get { return States.Menu; } }

        public override void Enter()
        {
            base.Enter();
        }

        public override States Event(StateMachineEvents type, params object[] args)
        {
            if (type == StateMachineEvents.GoToShop)
                return States.Shop;
            if (type == StateMachineEvents.GoToBuild)
                return States.Build;
            return base.Event(type, args);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}