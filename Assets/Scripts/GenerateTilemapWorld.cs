using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Unity.Jobs;
using System.ComponentModel;
using Unity.Collections;
using UnityEngine.UI;
using System.Runtime.InteropServices.WindowsRuntime;
using System;

public class GenerateTilemapWorld : MonoBehaviour {
    [Header("Surface Blocks")]
    public Tile[] TileList;
    [Header("World Settings")]
    public int chunkWidth;
    [Space(10)]
    public int mapSizeX;
    public int mapSizeY;
    public int additionalHeight;
    [Space(10)]
    public int X;
    public int Y;
    public int Seed;
    public int Octaves;
    public float Scale, Persistence, Lacunarity, additionalLacunarity, smoothness;
    public followCursor cursor;
    int lastX;
    [Space(20)]
    public GameObject PlaceSilhouette;
    public int HotbarIndex = 0;
    Tilemap tilemap;
    Tile selectedTile;
    float[,] noiseMap;
    float[,] noiseMap2;
    private Vector3Int mousePos() {
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        return new Vector3Int(Mathf.FloorToInt(mousepos.x), Mathf.FloorToInt(mousepos.y), 0);
    }
    void Start() {
        noiseMap=Noise.GenerateNoiseMap(1, mapSizeY, Seed, Scale, Octaves, Persistence, Lacunarity, new Vector2(X, Y));
        noiseMap2=Noise.GenerateNoiseMap(1, mapSizeY+100000, Seed, Scale, Octaves, Persistence, Lacunarity+additionalLacunarity, new Vector2(X, Y));
        tilemap=this.gameObject.GetComponent<Tilemap>();
        selectedTile=TileList[0];
        PlaceSilhouette.GetComponent<SpriteRenderer>().sprite=TileList[0].sprite;
    }
    void Update() {

        if(Input.GetKeyDown(KeyCode.M)) {
            ThreadWorldGeneration(new Job());
        }
        if(Input.GetMouseButton(0)) {
            tilemap.SetTile(mousePos(), null);
        }
        else if(Input.GetMouseButton(1)&&cursor.Placable) {
            tilemap.SetTile(mousePos(), selectedTile);
        }
        if(Input.GetKeyDown(KeyCode.E)) {
            changeItem(true);
        }
        else if(Input.GetKeyDown(KeyCode.Q)) {
            changeItem(false);
        }
    }
    void changeItem(bool r)/*Incr = True - Decr = False*/ {
        if(r) {
            HotbarIndex++;
            if(HotbarIndex>TileList.Length-1) {
                HotbarIndex=0;
            }
            else if(HotbarIndex<0) {
                HotbarIndex=TileList.Length-1;
            }
            selectedTile=TileList[HotbarIndex];
            PlaceSilhouette.GetComponent<SpriteRenderer>().sprite=TileList[HotbarIndex].sprite;
        }
    }
    void ThreadWorldGeneration(Job job) {
        job.lastX=lastX;
        lastX=lastX+16;
        job.additionalHeight=this.additionalHeight;
        job.chunkWidth=this.chunkWidth;
        job.mapSizeX=this.mapSizeX;
        job.mapSizeY=this.mapSizeY;
        job.X=this.X;
        job.noiseMap=noiseMap;
        job.noiseMap2=noiseMap2;
        job.Y=this.Y;
        job.Seed=this.Seed;
        job.Octaves=this.Octaves;
        job.Scale=this.Scale;
        job.Persistence=this.Persistence;
        job.Lacunarity=this.Lacunarity;
        job.additionalLacunarity=this.additionalLacunarity;
        job.smoothness=this.smoothness;
        job.Execute();
    }
}
