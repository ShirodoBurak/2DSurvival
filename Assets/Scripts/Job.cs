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
    public float[,] noiseMap;
    public float[,] noiseMap2;
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
    public void Execute() {
        int clx = lastX;
        int CY = 0;
        int CX = lastX/chunkWidth;
        lastX=lastX+16;
        for(int j = 0; j<chunkWidth; j++) {
            int i = 0;
        #region Block placement
        pb:
            int h = Mathf.FloorToInt((noiseMap[0, clx+j]+noiseMap2[0, clx+j]/smoothness)*100)+additionalHeight;
            int X = j+clx;
            if(i==h) {
                writeToData(X-CX*chunkWidth, i-CY*chunkWidth, "grass", CY, CX);
            }
            else if(i<h&&i>h-8) {
                writeToData(X-CX*chunkWidth, i-CY*chunkWidth, "dirt", CY, CX);
            }
            else {
                writeToData(X-CX*chunkWidth, i-CY*chunkWidth, "stone", CY, CX);
            }
            if(i==chunkWidth*CY){CY++;} // If height equals chunkHeight times chunkWidth, Increase chunkHeight.
            if(i<h) { i++; goto pb; }
        #endregion
        }
        bytes=Encoding.ASCII.GetBytes(data);
        Thread wrtData = new Thread(new ThreadStart(WriteData));
        wrtData.Start();
    }
    #region Data management
    void writeToData(int X, int Y, string T, int CY /*Chunk height (Multiplied by chunkWidth)*/,int CX) {
        data+=X+"/"+Y+"/"+T+"/"+CY+"/"+CX+'\n';
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
        File.WriteAllText(path+"/chunk-"+index+".txt", data);
    }
    public string[] readData(int position) {
        string d = Encoding.ASCII.GetString(CLZF2.Decompress(File.ReadAllBytes(path+"/shirodo/chunk-"+position+".dat")));
        return d.Split('\n'); ;
    }
    #endregion
}
