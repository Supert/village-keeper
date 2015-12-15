using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class CastleScript : MonoBehaviour {
	private Image _image;
	public List<Sprite> castleSprites;
	public void SetSprite () {
		if (this._image.sprite != castleSprites[CoreScript.Instance.Data.VillageLevel])
			this._image.sprite = castleSprites[CoreScript.Instance.Data.VillageLevel];
	}
	void Awake () {
		_image = GetComponent<Image> () as Image;
	}
	void OnGameStateChanged (CoreScript.GameStateChangedEventArgs e) {
		switch (e.NewState) {
		case CoreScript.GameStates.InBuildMode:
			this.SetSprite ();
			break;
		}
	}
	void OnDataFieldChanged (DataScript.DataFieldChangedEventArgs e) {
		switch (e.FieldChanged) {
		case DataScript.DataFields.VillageLevel:
			this.SetSprite ();
			break;
		}
	}
	void Start () {
		CoreScript.Instance.GameStateChanged += (sender, e) => OnGameStateChanged (e);
		CoreScript.Instance.Data.DataFieldChanged += (sender, e) => OnDataFieldChanged (e);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
