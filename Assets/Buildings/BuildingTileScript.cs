using UnityEngine;
using System.Collections;
[RequireComponent (typeof (SpriteRenderer))]
public class BuildingTileScript : MonoBehaviour {
	public Sprite tileSpriteDefault;
	public Sprite tileSpriteHighlighted;
	private SpriteRenderer _spriteRenderer;
	// Use this for initialization
	public enum BuildingTileStates {
		Active,
		Highlightened,
		Disabled,
	}
	public int gridX;
	public int gridY;
	public BuildingScript Building = null;
	private BuildingTileStates _state;
	public BuildingTileStates State {
		get {
			return _state;
		}
		set {
			switch (value) {
			case BuildingTileStates.Active:
				this.gameObject.SetActive (true);
				this._spriteRenderer.sprite = tileSpriteDefault;
				break;
			case BuildingTileStates.Disabled:
				this.gameObject.SetActive (false);
				break;
			case BuildingTileStates.Highlightened:
				this.gameObject.SetActive (true);
				this._spriteRenderer.sprite = tileSpriteHighlighted;
				break;
			}
			_state = value;
		}
	}
	void Awake () {
		this._spriteRenderer = GetComponent<SpriteRenderer> () as SpriteRenderer;
	}
	void Start () {
		this.transform.localScale *= CoreScript.Instance.BuildingsArea.CellWorldSize.x / this._spriteRenderer.bounds.size.x * 0.9f;
	}
	
	// Update is called once per frame
	void Update () {
		switch (CoreScript.Instance.GameState) {
		case CoreScript.GameStates.InBuildMode:
			this._spriteRenderer.color = Vector4.MoveTowards (this._spriteRenderer.color, new Vector4 (1, 1, 1, 1), Time.deltaTime / 0.25f);
			break;
		case CoreScript.GameStates.InBattle:
			this._spriteRenderer.color = Vector4.MoveTowards (this._spriteRenderer.color, new Vector4 (1, 1, 1, 0), Time.deltaTime / 0.25f);
			break;
		}
	}
}
