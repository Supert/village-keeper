using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ArrowScript : MonoBehaviour {
	private Vector2 _targetPosition;
	SpriteRenderer _shadow;
	SpriteRenderer _sprite;
	Vector2 _velocity;
	// Use this for initialization
	void Start () {
		_sprite = gameObject.AddComponent <SpriteRenderer> ();
		_sprite.sprite = Resources.Load<Sprite> ("arrow");
	}
	IEnumerator InitCoroutine () {
		yield return new WaitForEndOfFrame ();
		_shadow.transform.position = new Vector3 (this.transform.position.x, _targetPosition.y, 0);
		var scaleFactor =  this._sprite.bounds.size.x / _shadow.sprite.bounds.size.x;
		_shadow.transform.localScale = new Vector3 (scaleFactor, scaleFactor, scaleFactor);
		_shadow.gameObject.SetActive (true);
	}
	public void Init (Vector2 initialPosition, Vector2 targetPosition) {
		_targetPosition = targetPosition;
		var deltaPosition = targetPosition - initialPosition;
		var gravity = 9.81f;
		var angleInRads = CoreScript.Instance.Archer.GetAimingAngleInRads ();
		//http://physics.stackexchange.com/questions/60595/solve-for-initial-velocity-of-a-projectile-given-angle-gravity-and-initial-and
		var initialVelocity = (deltaPosition.x / Mathf.Cos (angleInRads)) * Mathf.Sqrt (gravity / (2 * (-deltaPosition.y + Mathf.Tan (angleInRads) * deltaPosition.x)));
		this.transform.localPosition = initialPosition;
		_velocity = new Vector2 (Mathf.Cos (angleInRads), Mathf.Sin (angleInRads)) * initialVelocity;

		var _shadowGO = new GameObject ("arrow shadow");
		_shadowGO.SetActive (false);
		_shadow = _shadowGO.AddComponent <SpriteRenderer> () as SpriteRenderer;
		_shadow.sprite = Resources.Load<Sprite> ("shadow");

		StartCoroutine (InitCoroutine());
	}
	// Update is called once per frame
	void Update () {
		this._velocity -= new Vector2(- CoreScript.Instance.Wind.Strength * Time.deltaTime, 9.81f * Time.deltaTime);
		var newPosition = this.transform.position + ((Vector3) _velocity * Time.deltaTime);
		transform.position = newPosition;
		float angle = Mathf.Atan2(_velocity.y, _velocity.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

		_shadow.transform.position = new Vector3 (this.transform.position.x, _targetPosition.y, 0);


		if (this.transform.position.y < this._targetPosition.y) {
			Destroy (this.gameObject);
			Destroy (_shadow.gameObject);
		}
	}
}
