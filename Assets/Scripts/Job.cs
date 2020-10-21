using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Job : IJob {
    public int lastX;
    public int chunkWidth;
    public TileBase[] blockList;
    public Tilemap tilemap;
    float[,] noiseMap;
    public int mapSizeX;
    public int mapSizeY;
    public int X;
    public int Y;
    public int Seed;
    public int Octaves;
    public int additionalHeight;
    public float Scale, Persistence, Lacunarity, additionalLacunarity, smoothness;
    float[,] noiseMap2;
    BoundsInt positions;

    public void Execute() {
        //noiseMap=Noise.GenerateNoiseMap(1, lastX+16, Seed, Scale, Octaves, Persistence, Lacunarity, new Vector2(X, Y));
        //noiseMap2=Noise.GenerateNoiseMap(1, lastX+16, Seed, Scale, Octaves, Persistence, Lacunarity+additionalLacunarity, new Vector2(X, Y));
        int currentLastX = lastX;
        lastX=lastX+16;
        for(int j = 0; j<16/*chunkWidth*/; j++) {
            int i = 0;
        placeblock:
            int h = 16;//Mathf.FloorToInt((noiseMap[0, currentLastX+j]+noiseMap2[0, currentLastX+j]/smoothness)*100)+additionalHeight;
            int randomDirtHeight = 6;
            if(i==h) {
                tilemap.SetTile(new Vector3Int(j+currentLastX, i, 0), blockList[1]);
                
            }
            else if(i<h&&i>h-randomDirtHeight) {

                tilemap.SetTile(new Vector3Int(j+currentLastX, i, 0), blockList[0]);
            }
            else {
                tilemap.SetTile(new Vector3Int(j+currentLastX, i, 0), blockList[2]);
            }
            if(i<16/*h*/) { i++; goto placeblock; }
        }
    }
}
