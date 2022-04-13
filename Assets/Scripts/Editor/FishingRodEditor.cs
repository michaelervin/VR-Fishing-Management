using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FishingRod))]
public class FishingRodEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (!EditorApplication.isPlaying) return;

        FishingRod fishingRod = (FishingRod)target;
        if(GUILayout.Button("Launch Bobber"))
        {
            fishingRod.LaunchBobber();
        }
        if (GUILayout.Button("Reel Bobber"))
        {
            fishingRod.ReelBobber();
        }
    }
}
