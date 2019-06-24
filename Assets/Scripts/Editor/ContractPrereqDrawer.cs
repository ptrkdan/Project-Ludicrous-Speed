using UnityEngine;
using UnityEditor;

public class ContractPrereqDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        property.serializedObject.Update();
        base.OnGUI(position, property, label);
        property.serializedObject.ApplyModifiedProperties();
    }
}
