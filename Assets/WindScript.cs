using UnityEngine;
using System.Collections;
[RequireComponent (typeof (DelayerScript))]
public class WindScript : MonoBehaviour {
	public float Strength { get; private set; }
	public float TargetStrength { get; private set; }
	public float maxStrength;
	public float strengthAcceleration;
	public float minTimeBeforeChange;
	public float maxTimeBeforeChange;
	private DelayerScript delayer;
	void Start () {
		this.delayer = this.GetComponent<DelayerScript> () as DelayerScript;
		this.Strength = (Random.value - 0.5f) * 2 * maxStrength;
		this.TargetStrength = (Random.value - 0.5f) * 2 * maxStrength;
	}
	void SelectNewTargetStrength () {
		delayer.RunUniqueWithRandomDelay (minTimeBeforeChange, maxTimeBeforeChange, () => {
			this.TargetStrength = (Random.value - 0.5f) * 2 * maxStrength;
		});

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
