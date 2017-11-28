public class MonsterHealthBarScript : BarScript
{
    private OffScreenMenuScript offscreenMenu;

    protected override void Awake()
    {
        base.Awake();
        offscreenMenu = GetComponent<OffScreenMenuScript>() as OffScreenMenuScript;
    }

    protected override void Start()
    {
        base.Start();
        CoreScript.Instance.GameStateChanged += (sender, e) => OnGameStateChanged(e);
    }

    void OnGameStateChanged(CoreScript.GameStateChangedEventArgs e)
    {
        switch (e.NewState)
        {
            case CoreScript.GameStates.InBattle:
                offscreenMenu.Show();
                MaxValue = CoreScript.Instance.Monster.maxHealth;
                minValue = 0;
                break;
            case CoreScript.GameStates.Paused:
            case CoreScript.GameStates.InHelp:
                break;
            default:
                offscreenMenu.Hide();
                break;
        }
    }

    protected void Update()
    {
        MaxValue = CoreScript.Instance.Monster.maxHealth;
        CurrentValue = CoreScript.Instance.Monster.Health;
    }
}