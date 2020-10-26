using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataToWorld : MonoBehaviour
{
    Vector2Int chunkPosition;
    void LoadChunk() {

    }
    private string[] ReadLines(string path) {
        path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments)+"/shirodo";
        return null;
    }
    private Vector3Int convertLineToCoorinates(string line) {
        return new Vector3Int(0,0,0);
    }
}
