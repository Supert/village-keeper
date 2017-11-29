namespace VillageKeeper.FSM
{
    public class Args
    {
        public enum Types
        {
            //Init
            GameInitialized,

            //Menu
            GoToShop,
            GoToBuild,
            
            GoToMenu,
            
            //Build
            ShowBuildHelp,
            GoToBattle,

            //Battle
            ShowBattleHelp,
            Pause,
            RoundFinished,
        }

        public Types type;
        public object[] args;

        public Args(Types type, params object[] args)
        {
            this.type = type;
            this.args = args;
        }
    }
}