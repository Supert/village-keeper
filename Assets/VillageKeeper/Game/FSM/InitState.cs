using VillageKeeper.FSM;

namespace VillageKeeper.Game.FSM
{
    public class InitState : State<GameStateArgs>
    {
        public override void Enter()
        {
            base.Enter();
        }

        public override State<GameStateArgs> Event(GameStateArgs args)
        {
            if (args.type == GameStateArgs.Types.GameInitialized)
                return new MenuState();
            return base.Event(args);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}