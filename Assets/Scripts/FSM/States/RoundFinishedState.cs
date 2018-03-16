using VillageKeeper.Model;

namespace VillageKeeper.FSM
{
    public class RoundFinishedState : State
    {
        public override States Type { get { return States.RoundFinished; } }

        public override void Enter()
        {
            base.Enter();
            Core.Data.Saved.Gold.Set(Core.Data.Saved.Gold + Core.Data.Game.RoundFinishedBonusGold);
        }

        public override States Event(StateMachineEvents type, params object[] args)
        {
            if (type == StateMachineEvents.PauseMenuContinue)
                return States.Build;
            if (type == StateMachineEvents.GoToMenu)
                return States.Menu;
            return base.Event(type, args);
        }

        public override void Exit()
        {
            base.Exit();
            Core.Data.Saved.Save();
        }
    }
}