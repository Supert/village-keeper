using UnityEngine;
using System.Collections;
[RequireComponent (typeof (WindScript))]
public class CoreScript : MonoBehaviour {
	public MonsterScript Monster;
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
