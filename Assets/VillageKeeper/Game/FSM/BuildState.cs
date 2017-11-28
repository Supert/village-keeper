using VillageKeeper.FSM;

namespace VillageKeeper.Game.FSM
{
    public class BuildState : State<GameStateArgs>
    {
        public override void Enter()
        {
            base.Enter();
        }

        public override State<GameStateArgs> Event(GameStateArgs args)
        {
            if (args.type == GameStateArgs.Types.GoToMenu)
                return new MenuState();
            if (args.type == GameStateArgs.Types.ShowBuildHelp)
                return new BuildHelpState();
            if (args.type == GameStateArgs.Types.GoToBattle)
                return new BattleState();
            return base.Event(args);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}