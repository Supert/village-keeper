using UnityEngine;
using System.Collections;
using UnityEditor;
[CustomEditor (typeof (WindScript))]
public class WindScriptEditor : Editor {
	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();
		var windScript = (WindScript)target;
		EditorGUILayout.FloatField ("Wind Strength", windScript.Strength);
		EditorGUILayout.FloatField ("Target Strength", windScript.TargetStrength);
		/*windScript.maxStrength = EditorGUILayout.FloatField ("Max Strength Possible")
		windScript.strengthAcceleration = EditorGUILayout.FloatField ("Wind Acceleration", windScript.strengthAcceleration);
		windScript.minTimeBeforeChange = EditorGUILayout.FloatField ("Min delay before wind change", windScript.minTimeBeforeChange);
		windScript.maxTimeBeforeChange = EditorGUILayout.FloatField ("Max delay before wind change", windScript.minTimeBeforeChange);*/

	}
}
