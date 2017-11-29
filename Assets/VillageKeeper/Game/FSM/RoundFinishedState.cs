namespace VillageKeeper.FSM
{
    public class RoundFinishedState : State<Args>
    {
        public override void Enter()
        {
            base.Enter();
        }

        public override State<Args> Event(Args args)
        {
            if (args.type == Args.Types.GoToBuild)
                return new BuildState();
            if (args.type == Args.Types.GoToMenu)
                return new MenuState();
            return base.Event(args);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}