namespace VillageKeeper.FSM
{
    public class BuildHelpState : State<Args>
    {
        public override void Enter()
        {
            base.Enter();
            CoreScript.Instance.UiManager.OnBuildHelpEntered();
        }

        public override State<Args> Event(Args args)
        {
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