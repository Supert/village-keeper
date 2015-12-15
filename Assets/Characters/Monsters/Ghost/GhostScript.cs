using UnityEngine;
using System.Collections;

public class GhostScript : MonoBehaviour {
	private SpriteRenderer _sprite;
	// Use this for initialization
	void Start () {
		this._sprite = GetComponent<SpriteRenderer> () as SpriteRenderer;
		this._sprite.color = new Vector4 (1, 1, 1, 0);
		this._sprite.sortingLayerName = "Characters";
		this._sprite.sortingOrder = 1;
	}
	private bool _isGoingToFade = false;
	// Update is called once per frame
	void Update () {
		if (_isGoingToFade) {
			var lp = this.transform.localPosition;
			lp.y += 3f * Time.deltaTime;
			this.transform.localPosition = lp;
			if (this._sprite.color.a != 0)
				this._sprite.color = Vector4.MoveTowards (this._sprite.color, new Vector4 (1, 1, 1, 0), Time.deltaTime / 3f);
			else
				this.gameObject.SetActive (false);
		} else if (this._sprite.color.a != 1) {
			this._sprite.color = Vector4.MoveTowards (this._sprite.color, new Vector4 (1, 1, 1, 1), Time.deltaTime / 0.25f);
		} else {
			_isGoingToFade = true;	
		}
	
	}
}
