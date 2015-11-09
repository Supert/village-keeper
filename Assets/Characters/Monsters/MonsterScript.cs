using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

[RequireComponent (typeof(DelayerScript))]
[RequireComponent (typeof(SpriteRenderer))]
[RequireComponent (typeof(Animator))]
public class MonsterScript : MonoBehaviour
{
	public enum MonsterStates
	{
		Walking,
		Staying,
		Damaged,
		Killed,
	}
	private MonsterStates _state;

	public MonsterStates State {
		get {
			return _state;
		}
		set {
			switch (value) {
			case MonsterStates.Damaged:
				if (_state != MonsterStates.Killed) {
					this._animator.speed = 0f;
					this._sprite.color = new Color (1f, 0.5f, 0.5f , 1f);
					_state = value;
					this.Health--;
				}
				break;
			case MonsterStates.Killed: 
				this._sprite.color = new Color (1f, 0.5f, 0.5f, 1f);
				var ghost = (GameObject) Instantiate (Resources.Load ("Ghost"));
				ghost.transform.localPosition = this.transform.localPosition;
				_state = value;
				break;
			case MonsterStates.Staying:
				this._animator.speed = 0f;
				_state = value;
				break;
			case MonsterStates.Walking:
				this._animator.speed = 1f;
				_state = value;
				break;
			}
			if (MonsterStateChanged != null)
				MonsterStateChanged (this, new MonsterStateChangedEventArgs (value));
		}
	}
	public class MonsterStateChangedEventArgs : EventArgs {
		public MonsterStates NewState;
		public MonsterStateChangedEventArgs (MonsterStates state) {
			NewState = state;
		}
	}
	public event EventHandler<MonsterStateChangedEventArgs> MonsterStateChanged;
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

	public float maxHealth;
	private float? _health = null;
	public float Health {
		get {
			if (_health == null) {
				_health = (float?) maxHealth;
			}
			return (float) _health;
		}
		private set {
			_health = (float?) value;
			if (_health == 0) {
				State = MonsterStates.Killed;
			}
		}
	}

	public RectTransform areaRT;
	public float monsterSpeed;
	public float minDelayBeforePickingNewPosition;
	public float maxDelayBeforePickingNewPosition;

	
	private Vector2 _targetPosition;
	private SpriteRenderer _shadow;
	private SpriteRenderer _sprite;
	private DelayerScript _delayer;
	private Animator _animator;
	private Vector2 _position;
	private Collider2D _collider;

	public Vector2 Size {
		get {
			return (Vector2)this._sprite.bounds.size;
		}
	}

	public Vector2 MaxPosition {
		get {
			Vector3[] v = new Vector3[4];
			areaRT.GetWorldCorners (v);
			return new Vector2 (v [2].x - this.Size.x, v [2].y - this.Size.y / 2);
		}
	}

	public Vector2 MinPosition {
		get {
			Vector3[] v = new Vector3[4];
			areaRT.GetWorldCorners (v);
			return new Vector2 (v [0].x, v [0].y);
		}
	}

	public Vector2 Position {
		get {
			return _position;
		}
		private set {
			this._position = value;
			this.transform.localPosition = new Vector3 (value.x + this.Size.x / 2, value.y + this.Size.y / 2, (value.y - MinPosition.y) / (MaxPosition.y - MinPosition.y));
		}
	}

	void SelectNewTargetPosition ()
	{
		this._delayer.RunUniqueWithRandomDelay (minDelayBeforePickingNewPosition, maxDelayBeforePickingNewPosition, () => {
			this._targetPosition = new Vector2 (UnityEngine.Random.Range (MinPosition.x, MaxPosition.x), UnityEngine.Random.Range (MinPosition.y, MaxPosition.y));
		});

	}

	void Start ()
	{
		this._shadow = this.transform.GetChild (this.transform.childCount - 1).GetComponent<SpriteRenderer> () as SpriteRenderer;
		this._collider = GetComponent<Collider2D> () as Collider2D;
		this._delayer = GetComponent<DelayerScript> () as DelayerScript;
		this._sprite = GetComponent<SpriteRenderer> () as SpriteRenderer;
		this._animator = GetComponent<Animator> () as Animator;
		StartCoroutine (InitCoroutine ());
	}

	IEnumerator InitCoroutine ()
	{
		yield return null;
		this.Position = this._targetPosition = MaxPosition;
	}
	// Update is called once per frame
	void Update ()
	{
		switch (State) {
		case MonsterStates.Walking:		
			if (Position != _targetPosition) {
				Position = Vector2.MoveTowards (Position, _targetPosition, monsterSpeed * Time.deltaTime);
			} else {
				SelectNewTargetPosition ();
				this.State = MonsterStates.Staying;
			}
			break;
		case MonsterStates.Staying:
			if (Position != _targetPosition)
				this.State = MonsterStates.Walking;
			break;
		case MonsterStates.Damaged:
			if (this._sprite.color != Color.white)
				this._sprite.color = Vector4.MoveTowards (this._sprite.color,Color.white, Time.deltaTime / 0.200f);
			else
				this.State = MonsterStates.Walking;
			break;
		case MonsterStates.Killed: 
			if (this._sprite.color.a !=0) {
				this._sprite.color = Vector4.MoveTowards (this._sprite.color, new Vector4 (1, 1, 1, 0), Time.deltaTime / 1f);
				this._shadow.color = Vector4.MoveTowards (this._shadow.color, new Vector4 (1, 1, 1, 0), Time.deltaTime / 1f);
			}
			else {
				if (this.gameObject.activeInHierarchy) {
					this.gameObject.SetActive (false);
				}
			}
			break;
		}
		var scale = this.transform.localScale;
		if ((Position - _targetPosition).x >= 0) {
			scale.x = 1;
		} else
			scale.x = -1;
		this.transform.localScale = scale;
	}
	public bool CheckHitByPosition (Vector3 projectilePosition)
	{
		if (this._collider.OverlapPoint ((Vector2)projectilePosition) && Mathf.Abs (projectilePosition.z - this.transform.position.z) <= this._shadow.bounds.extents.y / (MaxPosition.y - MinPosition.y)) {
			if (State != MonsterStates.Killed)
				this.State = MonsterStates.Damaged;
			return true;
		}
		return false;
	}
}
