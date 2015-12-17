using UnityEngine;
using System.Collections;

public class InGameScreenShadowScript : ScreenShadowScript
{

	// Use this for initialization
	protected override void Start ()
	{
		base.Start ();
		CoreScript.Instance.GameStateChanged += (object sender, CoreScript.GameStateChangedEventArgs e) => {
			switch (e.NewState) {
			case CoreScript.GameStates.InHelp:
			case CoreScript.GameStates.Paused:
			case CoreScript.GameStates.RoundFinished:
				this.Show ();
				break;
			default:
				this.Hide ();
				break;
			}
		};
	}
	protected override void Update ()
	{
		base.Update ();
	}

}

