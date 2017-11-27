public class InGameScreenShadowScript : ScreenShadowScript
{
    protected override void Start()
    {
        base.Start();

        CoreScript.Instance.GameStateChanged += (object sender, CoreScript.GameStateChangedEventArgs e) =>
        {
            switch (e.NewState)
            {
                case CoreScript.GameStates.InHelp:
                case CoreScript.GameStates.Paused:
                case CoreScript.GameStates.RoundFinished:
                    Show();
                    break;
                default:
                    Hide();
                    break;
            }
        };
    }
}

