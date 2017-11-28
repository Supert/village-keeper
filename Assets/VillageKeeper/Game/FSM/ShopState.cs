using VillageKeeper.FSM;

namespace VillageKeeper.Game.FSM
{
    public class ShopState : State<GameStateArgs>
    {
        public override void Enter()
        {
            base.Enter();
        }

        public override State<GameStateArgs> Event(GameStateArgs args)
        {
            if (args.type == GameStateArgs.Types.GoToMenu)
                return new MenuState();
            return base.Event(args);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}