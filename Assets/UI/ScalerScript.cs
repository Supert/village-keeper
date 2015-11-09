using UnityEngine;
using System.Collections;
[RequireComponent (typeof (Canvas))]
public class ScalerScript : MonoBehaviour {
	
	public RectTransform cliffAreaRT;
	public RectTransform monsterAreaRT;
	public RectTransform archerRT;
	private RectTransform _rectTransform;
	// Use this for initialization
	void Start () {
		this._rectTransform = GetComponent<RectTransform> ();
		archerRT.anchoredPosition = new Vector2 (cliffAreaRT.rect.width * 0.8f, 0);
		//archerRT.offsetMin = new Vector2 (cliffAreaRT.rect.width, cliffAreaRT.rect.width);
		//archerRT.offsetMax = archerRT.offsetMin + new Vector2 (archerRT.rect.width, archerRT.rect.height);


		monsterAreaRT.offsetMin = new Vector2 (cliffAreaRT.rect.width, 0);
		monsterAreaRT.offsetMax = new Vector2 (0, - _rectTransform.rect.height * 0.65f);
		//monsterAreaRT.offsetMax = new Vector2 (20, 20);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
