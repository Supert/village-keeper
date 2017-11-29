using System.Collections.Generic;

namespace VillageKeeper.FSM
{
    public partial class StateMachine
    {
        public States current;

        private Dictionary<States, State> states = new Dictionary<States, State>
        {
            { States.Empty, null },
            { States.BattleHelp, new BattleHelpState() },
            { States.Battle, new BattleState() },
            {States.BuildHelp, new BuildHelpState() },
            {States.Build, new BuildState() },
            {States.Init, new InitState() },
            {States.Menu, new MenuState() },
            {States.Pause, new PauseState() },
            {States.RoundFinished, new RoundFinishedState() },
            {States.Shop, new ShopState() }
        };


        public States Current
        {
            get
            {
                return current;
            }
        }

        public StateMachine()
        {
            current = States.Init;
            states[current].Enter();
        }

        public void Event(StateMachineEvents type, params object[] args)
        {
            States result = states[current].Event(type, args);
            if (result == States.Empty)
                return;

            states[current].Exit();
            current = result;
            states[current].Enter();
        }
    }
}