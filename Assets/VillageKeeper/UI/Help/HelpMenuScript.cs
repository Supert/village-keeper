using UnityEngine;
using UnityEngine.UI;

public class HelpMenuScript : MonoBehaviour
{
    private OffScreenMenuScript offscreen;
    private CoreScript.GameStates previousState;
    public Text tipText;
    public Text tipCounterText;
    public Button closeButton;
    public Button previousButton;
    public Button nextButton;

    private int _currentTipNumber;
    public int CurrentTipNumber
    {
        get
        {
            return _currentTipNumber;
        }
        private set
        {
            _currentTipNumber = value;
            if (_currentTipNumber == 0)
                previousButton.gameObject.SetActive(false);
            else
                previousButton.gameObject.SetActive(true);
            if (_currentTipNumber == _currentTips.Length - 1)
                nextButton.gameObject.SetActive(false);
            else
                nextButton.gameObject.SetActive(true);
            tipText.text = _currentTips[_currentTipNumber];
            tipCounterText.text = "Tip " + (_currentTipNumber + 1).ToString() + "/" + _currentTips.Length;

        }
    }

    private string[] _currentTips;
    private string[] _inBuildModeTips = new string[] {
        "Welcome to Village Keeper, Keeper! We just settled down here, in beautiful Unknown.",
        "Drag and drop farm to build it. Build defenses, too.",
        "One or two wooden watchtowers behind stockade would be enough at first.",
        "Note that enemies always come from right side.",
        "Click red button at top to read these tips again. Good luck, Keeper!"
    };

    private string[] _inBattleTips = new string[] {
        "Whoa! This monster is huge! Well, the bigger it is, the harder it fall.",
        "Do you see me standing at cliff? Swipe there to the left to draw a bow, then click at monster to shoot.",
        "Don't hesitate to shoot as fast as you can. We are not short of arrows.",
        "Click red button at top to read these tips again. To arms, Keeper!"
    };

    void Start()
    {
        offscreen = GetComponent<OffScreenMenuScript>() as OffScreenMenuScript;
        nextButton.onClick.AddListener(() =>
        {
            CurrentTipNumber++;
            CoreScript.Instance.Audio.PlayClick();
        });
        previousButton.onClick.AddListener(() =>
        {
            CurrentTipNumber--;
            CoreScript.Instance.Audio.PlayClick();
        });
        closeButton.onClick.AddListener(() =>
        {
            CoreScript.Instance.GameState = previousState;
            CoreScript.Instance.Audio.PlayClick();
        });
    }

    public void OnGameStateChanged(CoreScript.GameStateChangedEventArgs e)
    {
        switch (e.NewState)
        {
            case CoreScript.GameStates.InBuildMode:
                if (!CoreScript.Instance.Data.WasInBuildTipShown)
                {
                    CoreScript.Instance.GameState = CoreScript.GameStates.InHelp;
                    CoreScript.Instance.Data.WasInBuildTipShown = true;
                }
                else
                {
                    offscreen.Hide();
                }
                break;
            case CoreScript.GameStates.InBattle:
                if (!CoreScript.Instance.Data.WasInBattleTipShown)
                {
                    CoreScript.Instance.GameState = CoreScript.GameStates.InHelp;
                    CoreScript.Instance.Data.WasInBattleTipShown = true;
                }
                else
                {
                    offscreen.Hide();
                }
                break;
            case CoreScript.GameStates.InHelp:
                previousState = e.PreviousState;
                _currentTips = previousState == CoreScript.GameStates.InBattle ? _inBattleTips : _inBuildModeTips;
                CurrentTipNumber = 0;
                offscreen.Show();
                break;
            case CoreScript.GameStates.Paused:
                break;
            default:
                offscreen.Hide();
                break;
        }
    }
}
