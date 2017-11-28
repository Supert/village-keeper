using VillageKeeper.FSM;

namespace VillageKeeper.Game.FSM
{
    public class MenuState : State<Args>
    {
        public override void Enter()
        {
            base.Enter();
        }

        public override State<Args> Event(Args args)
        {
            if (args.type == Args.Types.GoToShop)
                return new ShopState();
            if (args.type == Args.Types.GoToBuild)
                return new BuildState();
            return base.Event(args);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}