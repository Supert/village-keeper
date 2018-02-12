using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Shibari.Editor
{

    [CustomPropertyDrawer(typeof(BindableIds))]
    public class BindableIdsDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorStyles.popup.CalcHeight(GUIContent.none, 0);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var bindableIds = PropertyDrawerUtility.GetActualObjectForSerializedProperty<BindableIds>(fieldInfo, property);
            var path = property.FindPropertyRelative("pathInModel"); 

            path.stringValue = MultiLevelDropDownUtility.DrawControl(
                position, 
                label, 
                property.FindPropertyRelative("pathInModel").stringValue, 
                BindableData.GetBindableValuesPaths(Shibari.Model.RootNodeType, Shibari.Model.RootNodeType.Namespace + ".", false, true, bindableIds.allowedValueType).ToList()
            );

            property.serializedObject.ApplyModifiedProperties();
        }
    }
}