using System.Reflection;
using UnityEditor;
using UnityEngine;
using publicAttribute = AGLogic.Generation;

[CustomPropertyDrawer(typeof(PublicPropertyAttribute))]
public class PublicPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        PublicPropertyAttribute custom = (PublicPropertyAttribute)attribute;
        EditorGUI.PropertyField(position, property, label);

        FieldInfo field = fieldInfo;
        System.Type declaringType = field.DeclaringType;

        Debug.Log($"name: {field.Name}");
        Debug.Log($"class: {declaringType.Name}");
        publicAttribute.Generate(declaringType, field);
    }
}
