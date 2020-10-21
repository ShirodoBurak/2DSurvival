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

public class GenerateTilemapWorld : MonoBehaviour {
    [Header("Surface Blocks")]
    public Tile grass;
    public Tile dirt;
    [Header("Underground Blocks")]
    public Tile stone;
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
    public GameObject ItemBox;
    Tilemap tilemap;
    Tile selectedTile;
    private Vector3Int mousePos() {
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        return new Vector3Int(Mathf.FloorToInt(mousepos.x), Mathf.FloorToInt(mousepos.y), 0);
    }
    void Start() {
        tilemap=this.gameObject.GetComponent<Tilemap>();
        Tile[] tiles = { dirt, grass, stone };
        selectedTile=tiles[0];
        ItemBox.GetComponent<Image>().sprite=tiles[0].sprite;
    }
    void Update() {
        if(Input.GetKeyDown(KeyCode.M)) {
            ThreadWorldGeneration(new Job());
        }
        if(Input.GetMouseButton(0)) {
            tilemap.SetTile(mousePos(), null);
        }else if(Input.GetMouseButton(1) && cursor.Placable) {
            tilemap.SetTile(mousePos(), selectedTile);
        }
        if(Input.GetKeyDown(KeyCode.E)) {
            changeItem(true);
        }else if(Input.GetKeyDown(KeyCode.Q)) {
            changeItem(false);
        }

    }
    void changeItem(bool r)/*Right = True - Left = False*/ {
        Tile[] tiles = { dirt, grass, stone };
        if(r) {
            if(selectedTile==tiles[2]) {
                selectedTile=tiles[0];
                ItemBox.GetComponent<Image>().sprite=tiles[0].sprite;
            }
            else if(selectedTile==tiles[0]) {
                selectedTile=tiles[1];
                ItemBox.GetComponent<Image>().sprite=tiles[1].sprite;
            }
            else if(selectedTile==tiles[1]) {
                selectedTile=tiles[2];
                ItemBox.GetComponent<Image>().sprite=tiles[2].sprite;
            }
        }
        else {
            if(selectedTile==tiles[2]) {
                selectedTile=tiles[1];
                ItemBox.GetComponent<Image>().sprite=tiles[1].sprite;
            }
            else if(selectedTile==tiles[0]) {
                selectedTile=tiles[2];
                ItemBox.GetComponent<Image>().sprite=tiles[2].sprite;
            }
            else if(selectedTile==tiles[1]) {
                selectedTile=tiles[0];
                ItemBox.GetComponent<Image>().sprite=tiles[0].sprite;
            }
        }
    }
    void ThreadWorldGeneration(Job job) {
        TileBase[] tiles = { dirt, grass, stone };
        job.lastX=lastX;
        lastX=lastX+16;
        job.additionalHeight=this.additionalHeight;
        job.chunkWidth=this.chunkWidth;
        job.tilemap=this.gameObject.GetComponent<Tilemap>();
        job.mapSizeX=this.mapSizeX;
        job.mapSizeY=this.mapSizeY;
        job.X=this.X;
        job.Y=this.Y;
        job.Seed=this.Seed;
        job.Octaves=this.Octaves;
        job.Scale=this.Scale;
        job.Persistence=this.Persistence;
        job.Lacunarity=this.Lacunarity;
        job.additionalLacunarity=this.additionalLacunarity;
        job.smoothness=this.smoothness;
        job.blockList=tiles;
        job.Execute();
    }
}
