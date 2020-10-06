using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateChunks : MonoBehaviour {
    [Header("Biome Chunks")]
    [Space(20)]
    public GameObject plainChunk;
    public GameObject plainChunkLeft;
    [Space(20)]
    [Range(1, 100)]
    int plainChunkWidth;
    [Header("Generation Settings")]
    public int numberOfChunks;
    public long seed;
    GameObject selectedChunk;
    [HideInInspector]
    public int lastX;
    public int lastXN;
    [HideInInspector]
    public int lastNX = 32, lastNXN = -32;

    public void Start() {
        plainChunkWidth=plainChunk.GetComponent<GeneratePlainChunk>().width;
        seed=Random.Range(-84450000, 89084889);
        GameObject yeniChunk;
        selectedChunk=plainChunk;
        yeniChunk=Instantiate(selectedChunk, new Vector2(0, 0), Quaternion.identity);
        Debug.Log("Hello");
        GenerateMiddle();
        Debug.Log("Hello2");
    }
    public void Update() {
        if(Input.GetKeyDown("n")) {
            GenerateL();
        }
        else if(Input.GetKeyDown("m")) {
            GenerateR();
        }
    }

    public void GenerateMiddle() {
        GameObject yeniChunk;
        selectedChunk=plainChunk;
        yeniChunk=Instantiate(selectedChunk, new Vector2(lastXN-plainChunkWidth, 0), Quaternion.identity);
    }

    public void GenerateL() {
        GameObject yeniChunk;
        selectedChunk=plainChunkLeft;
        yeniChunk=Instantiate(selectedChunk, new Vector2(lastXN-plainChunkWidth, 0), Quaternion.identity);
        lastXN-=plainChunkWidth;
    }
    public void GenerateR() {
        GameObject yeniChunk;
        selectedChunk=plainChunk;
        yeniChunk=Instantiate(selectedChunk, new Vector2(lastX+plainChunkWidth, 0), Quaternion.identity);
        lastX+=plainChunkWidth;
    }
}