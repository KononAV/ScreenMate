using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(PublicReadonlyAttribute))]
public class PublicPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        PublicReadonlyAttribute custom = (PublicReadonlyAttribute)attribute;
        EditorGUI.PropertyField(position, property, label);

        FieldInfo field = fieldInfo;
        System.Type declaringType = field.DeclaringType;

        Debug.Log($"name: {field.Name}");
        Debug.Log($"class: {declaringType.Name}");
        //publicAttribute.Generate(declaringType);
    }
}
