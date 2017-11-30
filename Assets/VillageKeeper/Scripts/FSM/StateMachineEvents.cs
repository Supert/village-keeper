namespace VillageKeeper.FSM
{
    public enum StateMachineEvents
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
}