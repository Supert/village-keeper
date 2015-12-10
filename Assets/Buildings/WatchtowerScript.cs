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
}
