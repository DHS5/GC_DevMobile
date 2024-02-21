using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class EnumValues<T, U> : IEnumerable<U> where T : Enum
{
    [SerializeField] private U[] enumValues;

    public U Get(T enumValue)
    {
        return enumValues[Convert.ToInt32(enumValue)];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return enumValues.GetEnumerator();
    }

    public IEnumerator<U> GetEnumerator()
    {
        for (var i = 0; i < enumValues.Length; i++) yield return enumValues[i];
    }
}

#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(EnumValues<,>))]
public class EnumValuesDrawer : PropertyDrawer
{
    private SerializedProperty p_enumValues;

    private Type enumType;

    private Array enumValues;
    private string[] enumNames;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        enumType = fieldInfo.FieldType;
        while (enumType.IsGenericType) enumType = enumType.GetGenericArguments()[0];

        p_enumValues = property.FindPropertyRelative("enumValues");

        enumValues = Enum.GetValues(enumType);
        enumNames = Enum.GetNames(enumType);

        var arraySize = (int)enumValues.GetValue(enumValues.Length - 1) + 1;
        p_enumValues.arraySize = arraySize;

        EditorGUI.BeginProperty(position, label, property);

        var labelRect = new Rect(position.x, position.y, 150f, EditorGUIUtility.singleLineHeight);
        var valueRect = new Rect(position.x + 155f, position.y, position.width - 155f,
            EditorGUIUtility.singleLineHeight);

        int index;
        foreach (var v in enumValues)
        {
            index = (int)v;
            EditorGUI.LabelField(labelRect, enumNames[index]);
            EditorGUI.PropertyField(valueRect, p_enumValues.GetArrayElementAtIndex(index), GUIContent.none, true);

            labelRect.y += EditorGUIUtility.singleLineHeight;
            valueRect.y += EditorGUIUtility.singleLineHeight;
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        enumType = fieldInfo.FieldType;
        while (enumType.IsGenericType) enumType = enumType.GetGenericArguments()[0];

        return Enum.GetValues(enumType).Length * EditorGUIUtility.singleLineHeight;
    }
}

#endif