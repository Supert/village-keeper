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
            
            ShowHelp,
            HideHelp,

            GoToBattle,
            
            Pause,
            RoundFinished,
        }
}