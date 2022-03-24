using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FishContainerTransfer))]
public class FishContainerTransferEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (!EditorApplication.isPlaying) return;

        FishContainerTransfer transfer = (FishContainerTransfer)target;
        if (GUILayout.Button("Transfer Fish"))
        {
            transfer.Transfer();
        }
    }
}
