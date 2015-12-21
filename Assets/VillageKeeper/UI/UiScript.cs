using UnityEngine;
using System.Collections;

public class UiScript : MonoBehaviour
{
	public HelpMenuScript helpMenu;
	// Use this for initialization
	void Start ()
	{
		CoreScript.Instance.GameStateChanged += (sender, e) => StartCoroutine (OnGameStateChangedCoroutine (e));
		
	}
	private IEnumerator OnGameStateChangedCoroutine (CoreScript.GameStateChangedEventArgs e) {
		yield return null;
		helpMenu.OnGameStateChanged (e);
	}
}

