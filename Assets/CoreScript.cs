using UnityEngine;
using System.Collections;
[RequireComponent (typeof (WindScript))]
public class CoreScript : MonoBehaviour {
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
	public static CoreScript Instance {
		get;
		private set;
	}
	// Use this for initialization
	void Start () {
		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
