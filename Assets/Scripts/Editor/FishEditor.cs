using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Fish))]
public class FishEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (!EditorApplication.isPlaying) return;

        Fish fish = (Fish)target;
        if(GUILayout.Button("Detatch hook"))
        {
            fish.DetachHook();
        }
    }
}
