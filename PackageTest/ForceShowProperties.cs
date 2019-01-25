using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;

/// <summary>
/// 指定したコンポーネントが持っている変数を表示する
/// </summary>
public class ForceShowProperties : MonoBehaviour
{
    public MonoBehaviour component;

    public List<FieldInfo> fields = new List<FieldInfo>();

    private void OnValidate()
    {
        if(component == null)
        {
            return;
        }

        Type t = component.GetType();

        fields.Clear();

        fields.AddRange(t.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance));
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ForceShowProperties))]
public class ForceShowPropertiesDebug : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.LabelField("データ");

        if(target == null)
        {
            return;
        }
        var subject = target as ForceShowProperties;

        foreach(var data in subject.fields)
        {
            EditorGUILayout.LabelField(data.Name + ":" + data.GetValue(subject.component).ToString());
        }
    }
}
#endif
