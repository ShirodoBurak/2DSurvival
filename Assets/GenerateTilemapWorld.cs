using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Unity.Jobs;

public class GenerateTilemapWorld : MonoBehaviour {
    [Header("Surface Blocks")]
    public TileBase grass;
    public TileBase dirt;
    [Header("Underground Blocks")]
    public TileBase stone;
    [Header("World Settings")]
    public int chunkWidth;
    [Space(10)]
    public int mapSizeX;
    public int mapSizeY;
    [Space(10)]
    public int X;
    public int Y;
    public int Seed;
    public int Octaves;
    public float Scale, Persistence, Lacunarity, additionalLacunarity, smoothness;
    int lastX;
    float[,] noiseMap;
    float[,] noiseMap2;
    float[,] noiseMapSum;
    int AOEOP20_EatHROLA2 = 0;
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.M))
        {
            AOEOP20_EatHROLA2 = 0;
            noiseMap = Noise.GenerateNoiseMap(mapSizeX, mapSizeY, Seed, Scale, Octaves, Persistence, Lacunarity, new Vector2(X, Y));
            noiseMap2 =Noise.GenerateNoiseMap(mapSizeX, mapSizeY, Seed, Scale, Octaves, Persistence, Lacunarity+additionalLacunarity, new Vector2(X, Y));
            int index=0;
            foreach (var item in noiseMap)
            {
                noiseMap[0, index] = noiseMap[0, index] + (noiseMap2[0, index] / smoothness);
                index++;
            }
            index = 0;
            ThreadWorldGeneration(new Job());
            /*
            foreach (float item in noiseMapSum)
            {
                int i = Mathf.FloorToInt(item * 100);   
                this.gameObject.GetComponent<Tilemap>().SetTile(new Vector3Int(AOEOP20_EatHROLA2++, i, 0), dirt);
            }
            */
        }
    }

    void ThreadWorldGeneration(Job job)
    {
        TileBase[] tiles = { dirt, grass, stone };
        job.lastX = lastX;
        lastX=lastX + 16;
        job.noiseMap = noiseMap;
        job.tilemap = this.gameObject.GetComponent<Tilemap>();
        job.AOEOP20_EatHROLA2 = AOEOP20_EatHROLA2;
        job.blockList = tiles;
        job.chunkWidth = chunkWidth;
        job.Execute();
    }
    IEnumerator OptimizedWorldGeneration() {
        int currentLastX = lastX;
        lastX = lastX + 16;
        for (int j = 0; j<chunkWidth; j++) {
            int h = Mathf.FloorToInt(noiseMap[0,currentLastX+j] * 100);
            this.gameObject.GetComponent<Tilemap>().SetTile(new Vector3Int(AOEOP20_EatHROLA2++, j, 0), dirt);
            for (int i = 0; i<h; i++) {
                int randomDirtHeight = Random.Range(15, 19);
                if(i == h-1) {
                    this.gameObject.GetComponent<Tilemap>().SetTile(new Vector3Int(j+currentLastX, i, 0), grass);
                }
                else if(i < h && i >h-randomDirtHeight) {
                    this.gameObject.GetComponent<Tilemap>().SetTile(new Vector3Int(j+currentLastX, i, 0), dirt);
                }
                else {
                    this.gameObject.GetComponent<Tilemap>().SetTile(new Vector3Int(j+currentLastX, i, 0), stone);
                }

                
            }
            yield return new WaitForSeconds(0.001f);
        }
    }
}
