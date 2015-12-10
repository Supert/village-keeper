using UnityEngine;
using System.Collections;

public class WallScript : BuildingScript {
	public byte wallLevel;
	public override BuildingTypes Type {
		get {
			if (wallLevel == 0)
				return BuildingTypes.WallWooden;
			else return BuildingTypes.WallStone;
		}
	}
}
