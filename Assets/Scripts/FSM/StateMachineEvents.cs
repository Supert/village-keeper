namespace VillageKeeper.FSM
{
    public enum StateMachineEvents
        {
            //Init
            GameInitialized = 0,

            //Menu
            GoToShop = 1,
            GoToBuild = 2,
            
            GoToMenu = 3,
            
            ShowHelp = 4,
            HideHelp = 5,

            GoToBattle = 6,
            
            Pause = 7,
            RoundFinished = 8,

            PauseMenuContinue = 9,
        }
}