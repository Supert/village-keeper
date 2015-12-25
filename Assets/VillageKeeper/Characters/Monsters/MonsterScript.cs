using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.UI;
using DeenGames.Utils.AStarPathFinder;
using DeenGames.Utils;
[RequireComponent (typeof(DelayerScript))]
[RequireComponent (typeof(SpriteRenderer))]
[RequireComponent (typeof(Animator))]
[RequireComponent (typeof(Collider2D))]
public class MonsterScript : MonoBehaviour
{	
	
	public enum SectorValues
	{
		Close,
		Middle,
		Far,
	}
	private Animator _animator;
	private SpriteRenderer _sprite;
	private SpriteRenderer _shadow;
	private Collider2D _collider;

	public int maxHealth;
	private int? _health = null;
	public int Health {
		get {
			if (_health == null) {
				_health = (int?) maxHealth;
			}
			return (int) _health;
		}
		private set {
			_health = (int?) value;
			if (_health == 0) {
				Kill ();
			}
		}
	}
	public Vector2 Size {
		get {
			return (Vector2)this._sprite.bounds.size;
		}
	}
	public float monsterSpeed;
	public bool IsAgressive {
		get;
		private set;
	}
	
	public SectorValues Sector {
		get {
			if (this.transform.localPosition.x < (this.MaxPosition.x / 3))
				return SectorValues.Close;
			if (this.transform.localPosition.x >= this.MaxPosition.x / 3 && this.transform.localPosition.x < (this.MaxPosition.x * 2 / 3))
				return SectorValues.Middle;
			return SectorValues.Far;
		}
	}
	public Vector2 MinPosition {
		get {
			var buildingsArea = CoreScript.Instance.BuildingsArea;
			return buildingsArea.GetWorldPositionByGridPosition (new Vector2 (0, 0))+ GridToWorldOffset;
		}
	}
	public Vector2 MaxPosition {
		get {
			var buildingsArea = CoreScript.Instance.BuildingsArea;
			return buildingsArea.GetWorldPositionByGridPosition (new Vector2 (buildingsArea.numberOfColumns, buildingsArea.numberOfRows - 1)) + GridToWorldOffset;
		}
	}
	public Vector2 HiddenPosition {
		get {
			var x = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, 0, 0)).x;
			return new Vector2 (x + this.Size.x / 2, MaxPosition.y);
		}
	}
	
	private Vector2 TargetPosition {
		get;
		set;
	}
	public Vector2 GridToWorldOffset {
		get {
			return new Vector2 (CoreScript.Instance.BuildingsArea.CellWorldSize.x / 2, this.Size.y / 3);
		}
	}
	public Vector2 GridPosition {
		get {
			var buildingsArea = CoreScript.Instance.BuildingsArea;
			var result = buildingsArea.GetClosestGridPositionIgnoringGridLimits (new Vector2 (transform.localPosition.x, transform.localPosition.y) - GridToWorldOffset);
			return result;
		}
	}

	private List<PathFinderNode> _waypointsInGrid = new List<PathFinderNode> ();

	private Vector2? Waypoint {
		get {
			var buildingsArea = CoreScript.Instance.BuildingsArea;
			if (_waypointsInGrid.Count > 0 && (Vector2) transform.localPosition == (buildingsArea.GetWorldPositionByGridPosition (_waypointsInGrid[0].X, _waypointsInGrid[0].Y)) + GridToWorldOffset)
				_waypointsInGrid.RemoveAt(0);
			if (_waypointsInGrid.Count > 0)
				return (Vector2?) buildingsArea.GetWorldPositionByGridPosition (_waypointsInGrid[0].X, _waypointsInGrid[0].Y) + GridToWorldOffset;
			return null;
		}
	}
	private List<PathFinderNode> GetPathToRandomTarget (bool isAgressive) {
		byte[,] pathGrid = new byte[16, 16];
		List<Point> possibleTargets = new List<Point> ();
		var buildingsArea = CoreScript.Instance.BuildingsArea;
		for (int i = 0; i < buildingsArea.numberOfColumns; i++)
			for (int j = 0; j < buildingsArea.numberOfRows; j++) {
				if (buildingsArea.IsCellFree (new Vector2 (i, j)) && 
					(i == (buildingsArea.numberOfColumns - 1) ? true : buildingsArea.IsCellFree (new Vector2 (i + 1, j)))) {
					pathGrid [i, j] = PathFinderHelper.EMPTY_TILE;
				} else
					pathGrid [i, j] = PathFinderHelper.BLOCKED_TILE;
			}
		for (int j = 0; j < buildingsArea.numberOfRows; j++) {
			string s = "";
			for (int i = 0; i < buildingsArea.numberOfColumns; i++) {
				s+= pathGrid[i,j] == PathFinderHelper.EMPTY_TILE ? "O" : "X";
			}
		}
		for (int i = buildingsArea.numberOfColumns; i < 16; i++)
			for (int j = 0; j < buildingsArea.numberOfRows; j++)
				pathGrid [i, j] = PathFinderHelper.EMPTY_TILE;
		for (int i = 1; i < buildingsArea.numberOfColumns + 1; i++) {
			for (int j = 0; j < buildingsArea.numberOfRows; j++) {
				if (isAgressive) {
					if (buildingsArea.GetCell (i - 1, j) != null && buildingsArea.GetCell(i-1, j).Building != null)
						possibleTargets.Add (new Point (i, j));
				} else
					possibleTargets.Add (new Point (i, j));
			}
		}
		var pathFinder = new PathFinder (pathGrid);
		pathFinder.Diagonals = false;
		
		var monsterPoint = new Point ((int) GridPosition.x, (int) GridPosition.y);
		var paths = new List<List<PathFinderNode>> ();
		foreach (var t in possibleTargets) {
			var path = pathFinder.FindPath (monsterPoint, t);
			if (path != null)
				paths.Add (new List<PathFinderNode> (path));
		}
		if (paths == null || paths.Count == 0)
			return null;
		return paths [(int)UnityEngine.Random.Range (0, paths.Count)];
	}
	public bool CheckHitByPosition (Vector3 projectilePosition)
	{
		if (this._collider.OverlapPoint ((Vector2)projectilePosition)) {
		    if  (Mathf.Abs (projectilePosition.z - this.transform.localPosition.y) <= this._sprite.bounds.extents.y) {
				TakeDamage ();
				return true;
			}
		}
		return false;
	}
	public void TakeDamage () {
		this.Health--;
		this._sprite.color = new Color (1f, 0.5f, 0.5f , 1f);
		CoreScript.Instance.Audio.PlayMonsterHit ();
	}
	public void Kill () {
		this._sprite.color = new Color (1f, 0.5f, 0.5f, 1f);
		var ghost = (GameObject) Instantiate (Resources.Load ("Ghost"));
		ghost.transform.localPosition = this.transform.localPosition;
		CoreScript.Instance.GameState = CoreScript.GameStates.RoundFinished;
	}
	

	void MoveTowardsColor (Color targetColor, float animationDuration) {
		if (this._sprite.color != targetColor)
			this._sprite.color = Vector4.MoveTowards (this._sprite.color, targetColor, Time.deltaTime / animationDuration);
	}
	private Vector2? GetAdjacentBuildingCell () {
		var buildingsArea = CoreScript.Instance.BuildingsArea;
		if (GridPosition.x - 1 >= buildingsArea.numberOfColumns || GridPosition.x - 1 < 0)
			return null;	
		if (!buildingsArea.IsCellFree (GridPosition - new Vector2 (1, 0)))
			return GridPosition - new Vector2 (1, 0);
		return null;
		
	}
	private void Attack (Vector2 buildingCell) {
		this._animator.SetTrigger ("Attack");
		var scale = this.transform.localScale;
		scale.x = Mathf.Abs (scale.x);
		this.transform.localScale = scale;
		CoreScript.Instance.BuildingsArea.DamageCell (buildingCell);
		ChooseNewBehaviour ();
	}
	private float _agressiveness;
	private void ChooseNewBehaviour () {
		IsAgressive = UnityEngine.Random.value < _agressiveness;
	}
	private void SetWaypoints (List<PathFinderNode> waypoints) {
		this._waypointsInGrid = waypoints;
		TargetPosition = CoreScript.Instance.BuildingsArea.GetWorldPositionByGridPosition (waypoints.Last ().X, waypoints.Last ().Y) + GridToWorldOffset;
		var scale = this.transform.localScale;
		if (((Vector2) transform.localPosition - TargetPosition).x >= 0) {
			scale.x = Mathf.Abs (scale.x);
		} else
			if (((Vector2) transform.localPosition - TargetPosition).x < 0)
				scale.x = - Mathf.Abs (scale.x);
		this.transform.localScale = scale;
	}
	private void Move () {
		if (Waypoint != null && Waypoint.HasValue) {
			transform.localPosition = Vector2.MoveTowards (transform.localPosition, Waypoint.Value, monsterSpeed * Time.deltaTime);

		}
		if ((Vector2)transform.localPosition == TargetPosition) {
			ChooseNewBehaviour ();
		}

	}
	private void SetMaxHealthAndAgressiveness () {
		float buildingsHealth = 0;
		float buildingsCost = 0;
		foreach (var b in CoreScript.Instance.BuildingsArea.buildings) {
			buildingsHealth += b.MaxHealth;
			buildingsCost += b.GoldCost;
		}
		float pointsPool = buildingsHealth / 2 * (1 + CoreScript.Instance.Data.VillageLevel * 0.25f);
		float minHealthPossible = 10 * (3 * buildingsCost / 800 + 1);
		float maxHealthPossible = 100;
		if (pointsPool < minHealthPossible) {
			this.maxHealth = (int)minHealthPossible;
			this._agressiveness = pointsPool / minHealthPossible;
		} else {
			if (pointsPool > maxHealthPossible) {
				maxHealth = (int) maxHealthPossible;
				_agressiveness = 1f;
			} else {
				this._agressiveness = UnityEngine.Random.Range (pointsPool / maxHealthPossible, 1);
				this.maxHealth = (int) (pointsPool / _agressiveness);
			}
		}
	}
	void Awake () {
		this._sprite = GetComponent<SpriteRenderer> () as SpriteRenderer;
		this._collider = GetComponent<Collider2D> () as Collider2D;
		this._animator = GetComponent<Animator> () as Animator;
		this._shadow = this.transform.GetChild (this.transform.childCount - 1).GetComponent<SpriteRenderer> () as SpriteRenderer;
	}
	void Start ()
	{
		var ls = this.transform.localScale;
		ls *= (CoreScript.Instance.BuildingsArea.CellWorldSize.y * 2) / (this._sprite.bounds.size.y);
		this.transform.localScale = ls;
		CoreScript.Instance.GameStateChanged += (sender, e) => {
			if (e.NewState == CoreScript.GameStates.InBuildMode) {
				this.transform.localPosition = HiddenPosition;
				this.gameObject.SetActive (false);
			}
			if (e.NewState == CoreScript.GameStates.InBattle) {
				_animator.speed = 1f;
				if (! (e.PreviousState == CoreScript.GameStates.Paused || e.PreviousState == CoreScript.GameStates.InHelp)) {
					this._shadow.color = Color.white;
					this._sprite.color = Color.white;

					SetMaxHealthAndAgressiveness ();
					Health = maxHealth;

					this._waypointsInGrid.Clear ();
					this.transform.localPosition = TargetPosition = HiddenPosition;

					this.gameObject.SetActive (true);
				}
			}
			if (e.NewState == CoreScript.GameStates.RoundFinished || e.NewState == CoreScript.GameStates.Paused || e.NewState == CoreScript.GameStates.InHelp) {
				_animator.speed = 0f;
			}
		};
	}
	void Update () {
		switch (CoreScript.Instance.GameState) {
		case CoreScript.GameStates.InBattle:
			MoveTowardsColor (Color.white, 0.200f);

			if (_animator.GetCurrentAnimatorStateInfo (0).IsName ("GorillaWandering")) {
				if ((Vector2) transform.localPosition == this.TargetPosition) {
					if (IsAgressive) {
						var adjacentBuildingCellCoordinates = GetAdjacentBuildingCell ();
						if (adjacentBuildingCellCoordinates != null)
							Attack (adjacentBuildingCellCoordinates.Value);
						else {
							var waypoints = GetPathToRandomTarget (true);
							if (waypoints == null)
								waypoints = GetPathToRandomTarget (false);
							SetWaypoints (waypoints);
						}
					} else {
						SetWaypoints (GetPathToRandomTarget (false));

					}
				} else {
					Move ();
				}
			}
		
			break;
		case CoreScript.GameStates.RoundFinished:
			MoveTowardsColor (new Color (1, 1, 1, 0), 1f);
			break;
		}
		var lp = this.transform.localPosition;
		lp.z = lp.y;
		this.transform.localPosition = lp;
	}
}