using VillageKeeper.FSM;

namespace VillageKeeper.Game.FSM
{
    public class BattleHelpState : State<GameStateArgs>
    {
        public override void Enter()
        {
            base.Enter();
        }

        public override State<GameStateArgs> Event(GameStateArgs args)
        {
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