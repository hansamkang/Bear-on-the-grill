using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor (typeof (TestTileGenerator))]
public class TestGeberatirEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TestTileGenerator mapGen = (TestTileGenerator)target;

        if (DrawDefaultInspector())
        {
            if (mapGen.autoUpdate)
            {
                mapGen.GenerateMap();
            }
        }

        if(GUILayout.Button("Generate"))
        {
            mapGen.GenerateMap();
        }
        if (GUILayout.Button("Clean"))
        {
            mapGen.CleanMap();
        }
        //base.OnInspectorGUI();
    }
}
