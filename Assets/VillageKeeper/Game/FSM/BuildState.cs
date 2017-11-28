using VillageKeeper.FSM;

namespace VillageKeeper.Game.FSM
{
    public class BuildState : State<Args>
    {
        public override void Enter()
        {
            base.Enter();
        }

        public override State<Args> Event(Args args)
        {
            if (args.type == Args.Types.GoToMenu)
                return new MenuState();
            if (args.type == Args.Types.ShowBuildHelp)
                return new BuildHelpState();
            if (args.type == Args.Types.GoToBattle)
                return new BattleState();
            return base.Event(args);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}