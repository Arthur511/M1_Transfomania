using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(AnimationEntry))]
public class AnimationEntryDrawer : PropertyDrawer
{
    private static AnimatorControllerParameterType GetParamType(SerializedProperty property)
    {
        var typeProp = property.FindPropertyRelative("parameterType");

        if (typeProp == null)
            return AnimatorControllerParameterType.Float;

        return (AnimatorControllerParameterType)typeProp.intValue;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        float lineH = EditorGUIUtility.singleLineHeight;
        float spacing = EditorGUIUtility.standardVerticalSpacing;
        Rect rect = new Rect(position.x, position.y, position.width, lineH);

        property.isExpanded = EditorGUI.Foldout(rect, property.isExpanded, label, true);
        if (!property.isExpanded) { EditorGUI.EndProperty(); return; }

        EditorGUI.indentLevel++;

        void DrawField(string propName)
        {
            rect.y += lineH + spacing;
            EditorGUI.PropertyField(rect, property.FindPropertyRelative(propName));
        }

        DrawField("parameterName");
        DrawField("parameterType");

        switch (GetParamType(property))
        {
            case AnimatorControllerParameterType.Float:
                DrawField("floatValue");
                break;

            case AnimatorControllerParameterType.Int:
                DrawField("intValue");
                break;

            case AnimatorControllerParameterType.Bool:
                DrawField("boolValue");
                break;

            case AnimatorControllerParameterType.Trigger:
                DrawField("crossFadeDuration");
                DrawField("layer");
                break;
        }

        EditorGUI.indentLevel--;
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (!property.isExpanded)
            return EditorGUIUtility.singleLineHeight;

        float lineH = EditorGUIUtility.singleLineHeight;
        float spacing = EditorGUIUtility.standardVerticalSpacing;

        int lineCount = 3;

        switch (GetParamType(property))
        {
            case AnimatorControllerParameterType.Float:
            case AnimatorControllerParameterType.Int:
            case AnimatorControllerParameterType.Bool:
                lineCount += 1;
                break;
            case AnimatorControllerParameterType.Trigger:
                lineCount += 2;
                break;
        }

        return lineCount * (lineH + spacing);
    }
}