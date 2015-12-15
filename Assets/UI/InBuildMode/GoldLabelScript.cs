using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[RequireComponent (typeof(Text))]
public class GoldLabelScript : MonoBehaviour {
	private Text _text;
	private void SetText () {
		this._text.text = CoreScript.Instance.Data.Gold.ToString ();
	}
	void Awake () {
		_text = GetComponent<Text> () as Text;
	}
	void Start () {
		CoreScript.Instance.GameStateChanged += (sender, e) => OnGameStateChanged (e);
		CoreScript.Instance.Data.DataFieldChanged += (sender, e) => OnDataFieldChanged (e);
	}
	void OnGameStateChanged (CoreScript.GameStateChangedEventArgs e) {
		switch (e.NewState) {
		case CoreScript.GameStates.InBuildMode:
			SetText ();
			break;
		}
	}
	void OnDataFieldChanged (DataScript.DataFieldChangedEventArgs e) {
		if (e.FieldChanged == DataScript.DataFields.Gold)
			SetText ();
	}
	// Update is called once per frame
	void Update () {
	
	}
}
