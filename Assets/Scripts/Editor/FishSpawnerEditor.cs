using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FishSpawner))]
public class FishSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (!EditorApplication.isPlaying) return;

        FishSpawner fishSpawner = (FishSpawner)target;
        if (GUILayout.Button("Spawn Fish"))
        {
            fishSpawner.SpawnFish();
        }
        if (GUILayout.Button("Despawn Fish"))
        {
            fishSpawner.DespawnFish();
        }
    }
}
