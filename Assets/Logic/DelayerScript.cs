using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Reflection;
public class DelayerScript : MonoBehaviour
{
	List<MethodInfo> _runningActions = new List<MethodInfo> ();
	public void RunUniqueWithFixedDelay (float delay, Action action) {
		if (!_runningActions.Contains (action.Method)) {
			_runningActions.Add (action.Method);
			StartCoroutine (RunUniqueActionCoroutine (action, delay));
		}
	}
	public void RunUniqueWithRandomDelay (float minDelay, float maxDelay, Action action) {
		if (!_runningActions.Contains (action.Method)) {
			_runningActions.Add (action.Method);
			var delay = UnityEngine.Random.Range (minDelay, maxDelay);
			StartCoroutine (RunUniqueActionCoroutine (action, delay));
		}
	}
	public void RunWithFixedDelay (float delay, Action action) {
		StartCoroutine (RunActionCoroutine (action, delay));
	}
	public void RunWithRandomDelay (float minDelay, float maxDelay, Action action) {
		var delay = UnityEngine.Random.Range (minDelay, maxDelay);
		StartCoroutine (RunActionCoroutine (action, delay));
	}
	private IEnumerator RunUniqueActionCoroutine (Action action, float delay = 0) {
			yield return new WaitForSeconds (delay);
			action.Invoke ();
			_runningActions.Remove (action.Method);
	}
	private IEnumerator RunActionCoroutine (Action action, float delay = 0) {
		yield return new WaitForSeconds (delay);
		action.Invoke ();
	}
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

