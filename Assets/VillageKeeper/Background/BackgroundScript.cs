using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BackgroundScript : MonoBehaviour {
	public Image mountains;
	public Image village;
	public Sprite winterMountains;
	public Sprite winterVillage;
	// Use this for initialization
	void Start () {
		if (CoreScript.Instance.TodaySpecial == CoreScript.Specials.Winter) {
			mountains.sprite = winterMountains;
			village.sprite = winterVillage;
		}
	}
}
