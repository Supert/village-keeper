using UnityEngine;
using System.Collections;
using System;
[RequireComponent (typeof (WindScript))]
public class CoreScript : MonoBehaviour {
	public enum GameStates
	{
		InMenu,
		InGame,
		Paused,
		RoundFinished,
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
			case GameStates.InGame:
				break;
			case GameStates.Paused:
				break;
			case GameStates.RoundFinished:
				break;
			}
			this._gameState = value;
			if (GameStateChanged != null)
				GameStateChanged (this, new GameStateChangedEventArgs (value));
		}
	}
	public class GameStateChangedEventArgs : EventArgs
	{
		public GameStateChangedEventArgs (GameStates state) {
			this.NewState = state;
		}
		public GameStates NewState;
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
				_ui = GetComponent<UiScript>() as UiScript;
			return _ui;
		}
	}
	public static CoreScript Instance {
		get;
		private set;
	}
	// Use this for initialization
	void Start () {
		Instance = this;
		StartCoroutine (InitCoroutine());
	}
	IEnumerator InitCoroutine () {
		yield return null;
		this._gameState = GameStates.InGame;
		Monster.MonsterStateChanged += (object sender, MonsterScript.MonsterStateChangedEventArgs e) => {
			if (e.NewState == MonsterScript.MonsterStates.Killed)
				this.GameState = GameStates.RoundFinished;
		};
	}
	
	// Update is called once per frame
	void Update () {
	}
}
