using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[RequireComponent (typeof(Text))]
public class GoldLabelScript : MonoBehaviour {
	private Text _text;
	private void SetText () {
		this._text.text = CoreScript.Instance.Statistics.Gold + " Gold";
	}
	void Awake () {
		_text = GetComponent<Text> () as Text;
	}
	void Start () {
		CoreScript.Instance.GameStateChanged += (sender, e) => OnGameStateChanged (e);
	}
	void OnGameStateChanged (CoreScript.GameStateChangedEventArgs e) {
		switch (e.NewState) {
		case CoreScript.GameStates.InBuildMode:
			SetText ();
			break;
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
