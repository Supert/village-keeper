using UnityEngine;
using System.Collections;
using System;
[RequireComponent (typeof (WindScript))]
public class CoreScript : MonoBehaviour {
	public enum GameStates
	{
		InMenu,
		InShop,
		InBattle,
		Paused,
		RoundFinished,
		InBuildMode,
	}
	private GameStates _gameState;
	public GameStates GameState {
		get {
			return _gameState;
		}
		set {
			switch (value) {
			case GameStates.InMenu:
				break;
			case GameStates.InBattle:
				break;
			case GameStates.Paused:
				break;
			case GameStates.RoundFinished:
				break;
			case GameStates.InShop:
				break;
			}
			var previousState = _gameState;
			this._gameState = value;
			if (GameStateChanged != null)
				GameStateChanged (this, new GameStateChangedEventArgs (value, previousState));
		}
	}
	public class GameStateChangedEventArgs : EventArgs
	{
		public GameStateChangedEventArgs (GameStates newState, GameStates previousState) {
			this.NewState = newState;
			this.PreviousState = previousState;
		}
		public GameStates NewState;
		public GameStates PreviousState;
	}
	public event EventHandler<GameStateChangedEventArgs> GameStateChanged;
	private MonsterScript _monster;
	public MonsterScript Monster {
		get {
			if (_monster == null)
				_monster = FindObjectOfType (typeof(MonsterScript)) as MonsterScript;
			return _monster;
		}
	}
	private ArcherScript _archer;
	public ArcherScript Archer {
		get {
			if (_archer == null)
				_archer = FindObjectOfType (typeof(ArcherScript)) as ArcherScript;
			return _archer;
		}
	}
	private WindScript _wind;
	public WindScript Wind {
		get {
			if (_wind == null)
				_wind = FindObjectOfType (typeof(WindScript)) as WindScript;
			return _wind;
		}
	}
	private ControlsScript _controls;
	public ControlsScript Controls {
		get {
			if (_controls == null)
				_controls = FindObjectOfType (typeof(ControlsScript)) as ControlsScript;
			return _controls;
		}
	}
	private UiScript _ui;
	public UiScript UI {
		get {
			if (_ui == null)
				_ui = FindObjectOfType<UiScript>() as UiScript;
			return _ui;
		}
	}
	private MainMenuScript _mainMenu;
	public MainMenuScript MainMenu {
		get {
			if (_mainMenu = null) {
				_mainMenu = FindObjectOfType (typeof (MainMenuScript)) as MainMenuScript;
			}
			return _mainMenu;
		}
	}
	private StatisticsScript _statistics;
	public StatisticsScript Statistics {
		get {
			if (_statistics == null) {
				_statistics = GetComponent<StatisticsScript> () as StatisticsScript;
			}
			return _statistics;
		}
	}
	private BuildingsAreaScript _buildingsArea;
	public BuildingsAreaScript BuildingsArea {
		get {
			if (_buildingsArea == null) {
				_buildingsArea = FindObjectOfType <BuildingsAreaScript> ();
			}
			return _buildingsArea;
		}
	}
	public static CoreScript Instance {
		get;
		private set;
	}
	void Awake () {
		Instance = this;
	}
	// Use this for initialization
	void Start () {
		StartCoroutine (InitCoroutine());
	}
	IEnumerator InitCoroutine () {
		yield return null;
		this._gameState = GameStates.InMenu;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
