using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenShadowScript : MonoBehaviour {
	public float animationTime;
	private bool _isShown;
	public bool IsShown {
		get {
			return _isShown;
		}
		private set {
			if (value)
				this.gameObject.SetActive (true);
			_isShown = value;
		}
	}
	public void Show () {
		this.IsShown = true;
	}
	public void Hide () {
		this.IsShown = false;
	}
	private Button _button;
	public Button ShadowButton {
		get {
			if (_button == null)
				_button = GetComponent<Button> () as Button;
			return _button;
		}
	}
	Image _image;
	// Use this for initialization
	protected virtual void Start () {
		_image = GetComponent<Image>() as Image;
	}
	// Update is called once per frame
	protected virtual void Update () {
		if (IsShown) {
			if (this._image.color != Color.white)
			{
				this._image.color = Vector4.MoveTowards (_image.color, Color.white, animationTime == 0 ? 1 : Time.deltaTime / animationTime);
			}
		} 
		else {
			if (this._image.color == new Color (1f, 1f, 1f, 0f))
				this.gameObject.SetActive (false);
			else
				this._image.color = Vector4.MoveTowards (_image.color, new Color (1f, 1f, 1f, 0f), animationTime == 0 ? 1 : Time.deltaTime / animationTime);
		}
	}
}
