using UnityEngine;
using System.Collections;
public class WindScript : MonoBehaviour {
	public float Strength { get; private set; }
	public float TargetStrength { get; private set; }
	public float maxStrength;
	public float strengthAcceleration;
	public float minTimeBeforeChange;
	public float maxTimeBeforeChange;

	private bool _isSelectNewTargetStrengthCoroutineRunning = false;
	void Start () {
		this.Strength = (Random.value - 0.5f) * 2 * maxStrength;
		this.TargetStrength = (Random.value - 0.5f) * 2 * maxStrength;
	}
	void SelectNewTargetStrength () {
		if (!_isSelectNewTargetStrengthCoroutineRunning) {
			_isSelectNewTargetStrengthCoroutineRunning = true;
			StartCoroutine (SelectNewTargetStrengthCoroutine());
		}
	}
	IEnumerator SelectNewTargetStrengthCoroutine () {
		yield return new WaitForSeconds (Random.Range (this.minTimeBeforeChange, this.maxTimeBeforeChange));
		this.TargetStrength = (Random.value - 0.5f) * 2 * maxStrength;
		_isSelectNewTargetStrengthCoroutineRunning = false;
	}
	// Update is called once per frame
	void Update () {
		if (Mathf.Abs (Strength - TargetStrength) * Time.deltaTime >= strengthAcceleration * Time.deltaTime) {
			if (Strength > TargetStrength)
				Strength -= strengthAcceleration * Time.deltaTime;
			else
				Strength += strengthAcceleration * Time.deltaTime;
		}
		else {
			Strength = TargetStrength;
			SelectNewTargetStrength ();
		}
	}
}
