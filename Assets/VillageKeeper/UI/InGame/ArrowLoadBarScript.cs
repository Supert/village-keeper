using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ArrowLoadBarScript : BarScript
{
	public Sprite fullyLoadedBarFillerSprite;
	public Sprite partiallyLoadedBarFillerSprite;
	private RectTransform _rect;
	private Image _fillerImage;
	// Use this for initialization
	protected override void Start () {
		base.Start ();
		this._fillerImage = barFillerImage.GetComponent<Image> () as Image;
		
		this._rect = this.GetComponent<RectTransform> () as RectTransform;
		this.MaxValue = _rect.rect.width / _rect.localScale.x;
		this.minValue = 0;
	}
	// Update is called once per frame
	protected override void Update ()
	{
		base.Update ();
		if (CoreScript.Instance.GameState == CoreScript.GameStates.InBattle) { 
			if (!Input.GetMouseButton (0) && RelativeCurrentValue < 1)
				RelativeCurrentValue -= Time.deltaTime * 5;
			else
				CurrentValue = -CoreScript.Instance.Controls.TouchDeltaPosition.x * 3f;
			if (RelativeCurrentValue == 1f) { 
				if (_fillerImage.sprite != fullyLoadedBarFillerSprite)
					this._fillerImage.sprite = fullyLoadedBarFillerSprite;
			} else {
				if (_fillerImage.sprite != partiallyLoadedBarFillerSprite)
					this._fillerImage.sprite = partiallyLoadedBarFillerSprite;
			}
		}
	}
	protected override void Awake () {
		base.Awake ();
	}
}

