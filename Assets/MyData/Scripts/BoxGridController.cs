using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BoxGridController : MonoBehaviour
{
    [SerializeField] private int row;
    [SerializeField] private int col;
    [SerializeField] private float xSpacing;
    [SerializeField] private float ySpacing;
    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private Transform startPos;

    public void ExecuteGrid()
    {
        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }

        Vector3 position = startPos.position;
        int counter = 1;
        for (int i = 0; i < row; i++)
        {
            for(int j = 0; j < col; j++, counter++)
            {
                var box = Instantiate(boxPrefab, position, Quaternion.identity, transform);
                box.name = "Box " + counter;
                position.x += xSpacing;
            }

            position.x = startPos.position.x;
            position.y -= ySpacing;
        }
    }
}