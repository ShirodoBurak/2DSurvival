using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Job : IJob
{
    public int lastX;
    public int chunkWidth;
    public TileBase[] blockList;
    public Tilemap tilemap;
    public float[,] noiseMap;
    public int AOEOP20_EatHROLA2;

    public void Execute()
    {
        int currentLastX = lastX;
        lastX = lastX + 16;
        for (int j = 0; j < chunkWidth; j++)
        {
            int h = Mathf.FloorToInt(noiseMap[0, currentLastX + j] * 100);
            tilemap.SetTile(new Vector3Int(AOEOP20_EatHROLA2++, j, 0), blockList[0]);
            for (int i = 0; i < h; i++)
            {
                int randomDirtHeight = Random.Range(15, 19);
                if (i == h - 1)
                {
                    tilemap.SetTile(new Vector3Int(j + currentLastX, i, 0), blockList[1]);
                }
                else if (i < h && i > h - randomDirtHeight) { 
                
                    tilemap.SetTile(new Vector3Int(j + currentLastX, i, 0), blockList[0]);
                }
                else
                {
                    tilemap.SetTile(new Vector3Int(j + currentLastX, i, 0), blockList[2]);
                }
            }
        }
    }
}
