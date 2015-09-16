using UnityEngine;
using System.Collections;
[RequireComponent (typeof (Canvas))]
public class AreasScalerScript : MonoBehaviour {
	
	public RectTransform cliffAreaRT;
	public RectTransform monsterAreaRT;
	public RectTransform shootingAreaRT;
	private RectTransform _rectTransform;
	// Use this for initialization
	void Start () {
		this._rectTransform = GetComponent<RectTransform> ();
		shootingAreaRT.offsetMin = new Vector2 (cliffAreaRT.rect.width, 0);
		shootingAreaRT.offsetMax = new Vector2 (0, - _rectTransform.rect.height * 0.65f);
		monsterAreaRT.offsetMin = new Vector2 (cliffAreaRT.rect.width + 20f, 20f);
		monsterAreaRT.offsetMax = new Vector2 (-20f, - _rectTransform.rect.height * 0.65f - 20f);
		//monsterAreaRT.offsetMax = new Vector2 (20, 20);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
