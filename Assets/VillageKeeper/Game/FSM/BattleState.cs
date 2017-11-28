using VillageKeeper.FSM;

namespace VillageKeeper.Game.FSM
{
    public class BattleState : State<GameStateArgs>
    {
        public override void Enter()
        {
            base.Enter();
        }

        public override State<GameStateArgs> Event(GameStateArgs args)
        {
            if (args.type == GameStateArgs.Types.ShowBattleHelp)
                return new BattleHelpState();
            if (args.type == GameStateArgs.Types.Pause)
                return new PauseState();
            return base.Event(args);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}