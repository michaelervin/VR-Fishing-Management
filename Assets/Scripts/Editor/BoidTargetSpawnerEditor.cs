using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BoidTargetSpawner))]
public class BoidTargetSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        BoidTargetSpawner fishSpawner = (BoidTargetSpawner)target;
        if (GUILayout.Button("Spawn Target"))
        {
            fishSpawner.SpawnTarget();
        }
        if (GUILayout.Button("Despawn Target"))
        {
            fishSpawner.DespawnTarget();
        }
    }
}
