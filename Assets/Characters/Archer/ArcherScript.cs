using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[RequireComponent (typeof (RectTransform))]
[RequireComponent (typeof(Image))]
public class ArcherScript : MonoBehaviour
{
	public ArrowLoadBarScript arrowBar;
	private RectTransform _rect;
	private bool _isLoaded;
	public bool IsLoaded {
		get {
			if (arrowBar.RelativeCurrentValue == 1)
				return true;
			else return false;
		}
	}
	public enum ArcherAimingValues
	{
		Unloaded = 0,
		AimingDown,
		AimingStraight,
		AimingUp,
	}
	private ArcherAimingValues _state = 0;
	public ArcherAimingValues State  {
		get {
			return _state;
		}
		private set {
			_state = value;
			switch (value) {
			case (ArcherAimingValues.AimingDown):
				this.image.sprite = archerReadyAimingDownSprite;
				break;
			case (ArcherAimingValues.AimingStraight):
				this.image.sprite = archerReadyAimingStraightSprite;
				break;
			case (ArcherAimingValues.AimingUp):
				this.image.sprite = archerReadyAimingUpSprite;
				break;
			case (ArcherAimingValues.Unloaded):
				this.image.sprite = archerUnloadedSprite;
				break;
			}
		}
	}
	private Image image;
	public Sprite archerUnloadedSprite;
	public Sprite archerReadyAimingUpSprite;
	public Sprite archerReadyAimingStraightSprite;
	public Sprite archerReadyAimingDownSprite;
	// Use this for initialization
	void Start ()
	{
		this.image = GetComponent<Image> () as Image;
		this._rect = GetComponent<RectTransform> () as RectTransform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (IsLoaded) {
			if (CoreScript.Instance.Monster.Sector == MonsterScript.SectorValues.Close)
				this.State = ArcherAimingValues.AimingDown;
			else {
				if (CoreScript.Instance.Monster.Sector == MonsterScript.SectorValues.Middle)
					this.State = ArcherAimingValues.AimingStraight;
				else
					this.State = ArcherAimingValues.AimingUp;
			}
		} else
			this.State = ArcherAimingValues.Unloaded;
	}
	public float GetAimingAngleInRads () {
		switch (this.State) {
		case (ArcherAimingValues.AimingDown):
			return Mathf.Deg2Rad * (-11f); 
		case (ArcherAimingValues.AimingStraight):
			return 0f;
		case (ArcherAimingValues.AimingUp):
			return Mathf.Deg2Rad * 9f;
		}
		return 0f;
	}
	public void Shoot (Vector2 targetPosition) {
		if (IsLoaded) {
			var _targetPosition = targetPosition;
			var arrow = new GameObject ("arrow", typeof(ArrowScript)).GetComponent <ArrowScript> ();
			var initialPosition = (Vector2) this.transform.position + (Vector2) _rect.TransformVector (new Vector2 (this._rect.rect.width / 2, this._rect.rect.height * 0.6f));
			arrow.Init (initialPosition, _targetPosition, GetAimingAngleInRads ());
			CoreScript.Instance.Audio.PlayArrowShot ();
		}
	}
}
