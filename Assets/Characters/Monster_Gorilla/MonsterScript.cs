using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;	
[RequireComponent (typeof(DelayerScript))]
[RequireComponent (typeof(Image))]
public class MonsterScript : MonoBehaviour {
	public enum SectorValues
	{
		Close,
		Middle,
		Far,
	}
	public SectorValues Sector {
		get {
			if (this.Position.x < this.MaxPosition.x / 3)
				return SectorValues.Close;
			if (this.Position.x >= this.MaxPosition.x / 3 && this.Position.x < this.MaxPosition.x * 2 / 3)
				return SectorValues.Middle;
			return SectorValues.Far;
		}
	}
	public List<Sprite> MonsterWalkingSprites;
	private int _currentSpriteIndex = 0;
	public RectTransform areaRT;
	private RectTransform rt;
	private Vector2 _targetPosition;
	public float monsterSpeed;
	public float minDelayBeforePickingNewPosition;
	public float maxDelayBeforePickingNewPosition;
	private Image _shadow;
	
	private DelayerScript _delayer;
	private Image _image;
	private Vector2 _position;
	public Vector2 MaxPosition {
		get {
			return new Vector2 (areaRT.rect.width - rt.rect.width, areaRT.rect.height - rt.rect.height / 2);
		}
	}
	public Vector2 Position {
		get {
			return _position;
		}
		private set {
			this._position = value;
			//debug
			this.rt.anchoredPosition = new Vector2 (value.x + areaRT.offsetMin.x + rt.rect.width/2, value.y);
			//this.rt.offsetMin = new Vector2 (MaxPosition.x + areaRT.offsetMin.x + this.rt.rect.width, value.y);
			//this.rt.offsetMax = new Vector2 (MaxPosition.x + areaRT.offsetMin.x + this.rt.rect.width, value.y);
			//this.rt.offsetMin = new Vector2 (0, value.y);
			//this.rt.offsetMax = new Vector2 (0, value.y);
			//this.rt.offsetMin = new Vector2 (value.x + areaRT.offsetMin.x, value.y);
			//this.rt.offsetMax = new Vector2 (value.x + areaRT.offsetMax.x, value.y);
		}
	}
	void SelectNewTargetPosition () {
		this._delayer.RunUniqueWithRandomDelay (minDelayBeforePickingNewPosition, maxDelayBeforePickingNewPosition, () => {
			this._targetPosition = new Vector2 (Random.Range (0, MaxPosition.x), Random.Range (0,  MaxPosition.y));
		});

	}

	void Start () {
		this.rt = GetComponent <RectTransform> () as RectTransform;
		this._delayer = GetComponent<DelayerScript> () as DelayerScript;
		//workaround, thanks to unity 5.1.3
		this._shadow = this.transform.GetChild (this.transform.childCount - 1).GetComponent<Image> () as Image;
		this._image = GetComponent<Image> () as Image;
		StartCoroutine (InitCoroutine ());
	}
	IEnumerator InitCoroutine () {
		yield return null;
		this.rt.localPosition = Vector2.zero;
		this.Position = new Vector2 (MaxPosition.x, 0);
	}
	// Update is called once per frame
	void Update () {
		if (Position != _targetPosition) {
			Position = Vector2.MoveTowards (Position, _targetPosition, monsterSpeed * Time.deltaTime);
			_delayer.RunUniqueWithFixedDelay (0.2f, () => {
				if (this._currentSpriteIndex >= MonsterWalkingSprites.Count - 1) 
					this._currentSpriteIndex = 0;
				else
					this._currentSpriteIndex++;
				this._image.sprite = MonsterWalkingSprites[_currentSpriteIndex];
			});
		}
		else {
			SelectNewTargetPosition ();
		}
		var scale = this.transform.localScale;
		if ((Position - _targetPosition).x >= 0) {
			scale.x = 1;
		} else
			scale.x = -1;
		this.transform.localScale = scale;
		//workaround, thanks to unity 5.1.3
		_shadow.preserveAspect = !_shadow.preserveAspect;
	}
}
