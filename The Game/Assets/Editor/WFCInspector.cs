using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(WFCExample))]
public class WFCInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        WFCExample myScript = (WFCExample)target;

        if (GUILayout.Button("Create tilemap"))
        {
            for (int i = 0; i < myScript.outputImageArray.Length; i++) {
                myScript.CreateWFCArray(i);
                myScript.CreateTilemap();
            }
        }
        if (GUILayout.Button("Save tilemap"))
        {
            myScript.SaveTilemap();
        }
        if (GUILayout.Button("Create random tilemap")) {
            myScript.CreateRandomGrid();
        }

    }
}
