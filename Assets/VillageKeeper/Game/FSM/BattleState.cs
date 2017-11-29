namespace VillageKeeper.FSM
{
    public class BattleState : State<Args>
    {
        public override void Enter()
        {
            base.Enter();
            if (!CoreScript.Instance.Data.WasInBattleTipShown)
            {
                CoreScript.Instance.FSM.Event(new Args(Args.Types.ShowBattleHelp));
                CoreScript.Instance.Data.WasInBattleTipShown = true;
            }
        }

        public override State<Args> Event(Args args)
        {
            if (args.type == Args.Types.ShowBattleHelp)
                return new BattleHelpState();
            if (args.type == Args.Types.Pause)
                return new PauseState();
            if (args.type == Args.Types.RoundFinished)
                return new RoundFinishedState();
            return base.Event(args);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}