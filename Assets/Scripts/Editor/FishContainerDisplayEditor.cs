using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FishContainerDisplay))]
public class FishContainerDisplayEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (!EditorApplication.isPlaying) return;

        FishContainerDisplay container = (FishContainerDisplay)target;
        if (GUILayout.Button("Display Objects"))
        {
            container.DisplayObjects();
        }
    }
}
