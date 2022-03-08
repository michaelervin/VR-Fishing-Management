using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Hook))]
public class HookEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (!EditorApplication.isPlaying) return;

        Hook hook = (Hook)target;
        if (GUILayout.Button("Add Bait"))
        {
            hook.AddBait();
        }
    }
}
