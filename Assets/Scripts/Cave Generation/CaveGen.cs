using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CaveGen : MonoBehaviour
{

    public GameObject test;
    public GameObject selectedTile;
    public GameObject hitbox;
    public GameObject Baba;
    GameObject evlat;
    [Space(30)]

    public int width;
    public int height;

    [Header("LastX and Y, and LastNX and last NY")]
    public int lastX;
    public int lastY;
    public int lastNX;
    public int lastNY;

    [HideInInspector]
    public int randomHeight;
    public string seed;
    public bool useRandomSeed;

    [Range(0, 100)]
    public int randomFillPercent;
    int[,] map;
    [Space(20)]
    public GameObject TrashCan;

    void Start()
    {
        GenerateMap();
        CreateCaves();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            randomHeight = UnityEngine.Random.Range(0,60);
            seed = UnityEngine.Random.Range(0, 9999999999).ToString();
            lastX += lastX = -width / 2 + x + 8;
            GenerateMap();
            CreateCaves();
        }
    }
    void GenerateMap()
    {
        map = new int[width, height];
        RandomFillMap();


        for (int i = 0; i < 4; i++)
        {
            SmoothMap();
        }
    }


    void RandomFillMap()
    {
        System.Random r = new System.Random(seed.GetHashCode());
        for (int x = 0; x < width; x++)
        {
            for (y = 0; y < height; y++)
            {

                map[x, y] = ( r.Next(0, 100) < randomFillPercent ) ? 1 : 0;

            }
        }

    }

    void SmoothMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int neighbourWallTiles = GetSurroundingWallCount(x, y);

                if (neighbourWallTiles > 4)
                    map[x, y] = 1;
                else if (neighbourWallTiles < 4)
                    map[x, y] = 0;

            }
        }
    }

    int GetSurroundingWallCount(int gridX, int gridY)
    {
        int wallCount = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                if (neighbourX >= 1 && neighbourX < width && neighbourY >= 0 && neighbourY < height)
                {
                    if (neighbourX != gridX || neighbourY != gridY)
                    {
                        wallCount += map[neighbourX, neighbourY];
                    }
                }
                else
                {
                    wallCount++;
                }
            }
        }

        return wallCount;
    }
    int x;
    int y;
    void CreateCaves()
    {
        if (map != null)
        {
            for (x = 0; x < width; x++)
            {
                for (y = 0; y < height; y++)
                {
                    lastY = -height / 2 + y + randomHeight;
                    selectedTile = ( map[x, y] == 1 ) ? selectedTile = test : selectedTile = hitbox;
                    evlat = Instantiate(selectedTile, new Vector2(lastX + -width / 2 + x + 24, lastY), Quaternion.identity) as GameObject;
                    if (selectedTile = hitbox)
                    {
                        evlat.transform.parent = Baba.transform;
                    }
                    else
                    {

                        evlat.transform.parent = TrashCan.transform;

                    }
                }
            }
            for (int i = 0; i < TrashCan.transform.childCount; i++)
            {
                Destroy(TrashCan.transform.GetChild(i));
            }
        }
    }
}