using UnityEngine;
using System;
public class FarmScript : BuildingScript
{
	public FarmScript ()
	{
	}
	public override BuildingTypes Type {
		get {
			return BuildingTypes.Farm;
		}
	}
}


