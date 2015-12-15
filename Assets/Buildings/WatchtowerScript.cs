using UnityEngine;
using System.Collections;

public class WatchtowerScript : BuildingScript {
	public byte towerLevel;
	public override BuildingTypes Type {
		get {
			if (towerLevel == 0)
				return BuildingTypes.WatchtowerWooden;
			else
				return BuildingTypes.WatchtowerStone;
		}
	}
	private bool _isLoaded = true;
	public void Shoot () {
		if (_isLoaded) {
			var _targetPosition = CoreScript.Instance.Monster.transform.localPosition;
			var arrow = new GameObject ("arrow", typeof(ArrowScript)).GetComponent <ArrowScript> ();
			var initialPosition = (Vector2) this.transform.position;
			var vectorToCalcAngle = (Vector2) _targetPosition + new Vector2 (0, CoreScript.Instance.BuildingsArea.CellWorldSize.y) - (Vector2) initialPosition;
			var angle = Mathf.Atan2 (vectorToCalcAngle.y, vectorToCalcAngle.x);
			arrow.Init (initialPosition, _targetPosition, angle);
			this._isLoaded = false;
			StartCoroutine (ReloadCoroutine ());
		}
	}
	private IEnumerator ReloadCoroutine () {
		if (towerLevel == 0)
			yield return new WaitForSeconds (2f);
		else
			yield return new WaitForSeconds (1f);
		_isLoaded = true;
	}
	protected override void Update () {
		base.Update ();
		if (CoreScript.Instance.GameState == CoreScript.GameStates.InBattle) {
			if (_isLoaded && Vector2.Distance ((Vector2)this.transform.position, (Vector2)CoreScript.Instance.Monster.transform.localPosition) < CoreScript.Instance.BuildingsArea.CellWorldSize.x * 4) {
				Shoot ();
			}
		}
	}
}
