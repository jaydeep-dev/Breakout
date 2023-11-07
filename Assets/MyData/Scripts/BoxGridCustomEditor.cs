using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BoxGridController))]
public class BoxGridCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Generate Grid"))
        {
            ((BoxGridController)target).ExecuteGrid();
        }
        
        if (GUILayout.Button("Destroy Grid"))
        {
            var boxGrid = (BoxGridController)target;
            foreach (Transform box in boxGrid.transform)
            {
                DestroyImmediate(box.gameObject);
            }
        }
    }
}
