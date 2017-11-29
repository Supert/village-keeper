namespace VillageKeeper.FSM
{
    public class BattleHelpState : State<Args>
    {
        public override void Enter()
        {
            CoreScript.Instance.UiManager.OnBattleHelpEntered();
            base.Enter();
        }

        public override State<Args> Event(Args args)
        {
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