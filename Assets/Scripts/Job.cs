using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Job : IJob {
    public int lastX;
    public int chunkWidth;
    public TileBase[] tiles;
    public Tilemap tilemap;
    float[,] noiseMap;
    float[,] noiseMap2;
    public int mapSizeX;
    public int mapSizeY;
    public int X;
    public int Y;
    public int Seed;
    public int Octaves;
    public int additionalHeight;
    public float Scale, Persistence, Lacunarity, additionalLacunarity, smoothness;
    byte[] bytes;
    string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments)+"/shirodo";
    string data = "";
    public void noiseMaps() {
        noiseMap=Noise.GenerateNoiseMap(1, mapSizeY, Seed, Scale, Octaves, Persistence, Lacunarity, new Vector2(X, Y));
        noiseMap2=Noise.GenerateNoiseMap(1, mapSizeY, Seed, Scale, Octaves, Persistence, Lacunarity+additionalLacunarity, new Vector2(X, Y));
    }
    public void Execute() {
        noiseMaps();
        int clx = lastX;
        lastX=lastX+16;
        for(int j = 0; j<chunkWidth; j++) {
            int i = 0;
        #region Block placement
        pb:
            int h = Mathf.FloorToInt((noiseMap[0, clx+j]+noiseMap2[0, clx+j]/smoothness)*100)+additionalHeight;
            int randomDirtHeight = 6;
            int X = j+clx;
            if(i==h) {
                tilemap.SetTile(new Vector3Int(X, i, 0), tiles[1]);
                writeToData(X, i, tiles[1].name);
            }
            else if(i<h&&i>h-randomDirtHeight) {

                tilemap.SetTile(new Vector3Int(X, i, 0), tiles[0]);
                writeToData(X, i, tiles[0].name);
            }
            else {
                tilemap.SetTile(new Vector3Int(X, i, 0), tiles[2]);
                writeToData(X, i, tiles[2].name);
            }
            if(i<h) { i++; goto pb; }
        #endregion
        }
        bytes=Encoding.ASCII.GetBytes(data);
        Thread wrtData = new Thread(new ThreadStart(WriteData));
        wrtData.Start();
    }
    #region Data management
    void writeToData(int X, int Y, string T) {
        data+="X"+X+"Y"+Y+"T"+T+'\n';
    }
    void createFiles() {
        int index = lastX/16;
        createDirectories();
        File.Create(path+"/chunk-"+index+".dat", 4096, FileOptions.None).Dispose();
        File.Create(path+"/chunk-"+index+".txt", 4096, FileOptions.None).Dispose();
    }
    void createDirectories() {
        Directory.CreateDirectory(path);
    }
    void WriteData() {
        int index = lastX/16;
        createFiles();
        File.WriteAllBytes(path+"/chunk-"+index+".dat", CLZF2.Compress(bytes));
        File.WriteAllText(path+"/chunk-"+index+".txt", readData(lastX/16));
    }
    string readData(int position) {
        return Encoding.ASCII.GetString(CLZF2.Decompress(File.ReadAllBytes(path+"/shirodo/chunk-"+position+".dat")));
    }
    #endregion
}
