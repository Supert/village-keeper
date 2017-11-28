using VillageKeeper.FSM;

namespace VillageKeeper.Game.FSM
{
    public class BattleState : State<Args>
    {
        public override void Enter()
        {
            base.Enter();
        }

        public override State<Args> Event(Args args)
        {
            if (args.type == Args.Types.ShowBattleHelp)
                return new BattleHelpState();
            if (args.type == Args.Types.Pause)
                return new PauseState();
            return base.Event(args);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}