using VillageKeeper.FSM;

namespace VillageKeeper.Game.FSM
{
    public class MenuState : State<GameStateArgs>
    {
        public override void Enter()
        {
            base.Enter();
        }

        public override State<GameStateArgs> Event(GameStateArgs args)
        {
            if (args.type == GameStateArgs.Types.GoToShop)
                return new ShopState();
            if (args.type == GameStateArgs.Types.GoToBuild)
                return new BuildState();
            return base.Event(args);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}