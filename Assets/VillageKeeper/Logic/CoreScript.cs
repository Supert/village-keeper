using UnityEngine;
using AssemblyCSharp;
using System.Collections;
using System;
using Soomla.Store;
[RequireComponent(typeof(WindScript))]
public class CoreScript : MonoBehaviour
{
    public enum GameStates
    {
        InMenu,
        InShop,
        InBattle,
        Paused,
        RoundFinished,
        InBuildMode,
        InHelp,
    }

    private GameStates gameState;
    public GameStates GameState
    {
        get
        {
            return gameState;
        }
        set
        {
            var previousState = gameState;
            gameState = value;
            if (GameStateChanged != null)
                GameStateChanged(this, new GameStateChangedEventArgs(value, previousState));
        }
    }

    public class GameStateChangedEventArgs : EventArgs
    {
        public GameStateChangedEventArgs(GameStates newState, GameStates previousState)
        {
            NewState = newState;
            PreviousState = previousState;
        }
        public GameStates NewState;
        public GameStates PreviousState;
    }

    public event EventHandler<GameStateChangedEventArgs> GameStateChanged;

    public enum Specials
    {
        None,
        Winter,
    }

    private Specials? todaySpecial = null;
    public Specials TodaySpecial
    {
        get
        {
            if (todaySpecial == null)
            {
                var winterSpecialBeginning = new DateTime(2012, 12, 24); //December 24th
                var winterSpecialEnd = new DateTime(2012, 1, 14); // January 14th
                if (CheckIfDateIsInPeriodOfYear(DateTime.Today, winterSpecialBeginning, winterSpecialEnd))
                    todaySpecial = (Specials?)Specials.Winter;
                else
                    todaySpecial = (Specials?)Specials.None;
            }
            return todaySpecial.Value;
        }
    }

    //Day and Month matter only
    private bool CheckIfDateIsInPeriodOfYear(DateTime date, DateTime beginning, DateTime end)
    {
        var _date = new DateTime(2012, date.Month, date.Day);
        var _beginning = new DateTime(2012, beginning.Month, beginning.Day);
        var _end = new DateTime(2012, end.Month, end.Day);
        if (_beginning > _end)
            _end = _end.AddYears(1);
        if (_date >= _beginning && _date <= _end)
            return true;
        _date = _date.AddYears(1);
        if (_date >= _beginning && _date <= _end)
            return true;
        return false;
    }

    private MonsterScript monster;
    public MonsterScript Monster
    {
        get
        {
            if (monster == null)
                monster = FindObjectOfType(typeof(MonsterScript)) as MonsterScript;
            return monster;
        }
    }

    private ArcherScript archer;
    public ArcherScript Archer
    {
        get
        {
            if (archer == null)
                archer = FindObjectOfType(typeof(ArcherScript)) as ArcherScript;
            return archer;
        }
    }

    private WindScript wind;
    public WindScript Wind
    {
        get
        {
            if (wind == null)
                wind = FindObjectOfType(typeof(WindScript)) as WindScript;
            return wind;
        }
    }
    private ControlsScript controls;
    public ControlsScript Controls
    {
        get
        {
            if (controls == null)
                controls = FindObjectOfType(typeof(ControlsScript)) as ControlsScript;
            return controls;
        }
    }

    private MainMenuScript mainMenu;
    public MainMenuScript MainMenu
    {
        get
        {
            if (mainMenu == null)
            {
                mainMenu = FindObjectOfType(typeof(MainMenuScript)) as MainMenuScript;
            }
            return mainMenu;
        }
    }
    private DataScript data;
    public DataScript Data
    {
        get
        {
            if (data == null)
            {
                data = GetComponent<DataScript>() as DataScript;
            }
            return data;
        }
    }
    private BuildingsAreaScript buildingsArea;
    public BuildingsAreaScript BuildingsArea
    {
        get
        {
            if (buildingsArea == null)
            {
                buildingsArea = FindObjectOfType<BuildingsAreaScript>();

            }
            return buildingsArea;
        }
    }
    private AudioScript audioScript;
    public AudioScript Audio
    {
        get
        {
            if (audioScript == null)
            {
                audioScript = GetComponent<AudioScript>();
            }
            return audioScript;
        }
    }

    public static CoreScript Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //SoomlaStore.Initialize(new EconomyAssets());
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        StartCoroutine(InitCoroutine());
    }

    IEnumerator InitCoroutine()
    {
        yield return null;
        yield return null;
        gameState = GameStates.InMenu;
    }
}