using UnityEngine;
using System;
public class FarmScript : BuildingScript
{
	public SpriteRenderer cropsSR;
	public Sprite winterSpecialCropsSprite;

	protected void Start () {
		if (CoreScript.Instance.TodaySpecial == CoreScript.Specials.Winter)
			this.cropsSR.sprite = winterSpecialCropsSprite;
	}

	public override BuildingTypes Type {
		get {
			return BuildingTypes.Farm;
		}
	}
}


