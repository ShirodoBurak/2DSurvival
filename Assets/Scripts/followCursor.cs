using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class followCursor : MonoBehaviour {

    public Vector3 MousePos;
    public RaycastHit2D rayHit;
    public RaycastHit2D hit;
    public Tilemap tilemap;
    public bool Placable;
    void Start() {
        Cursor.visible=false;
    }
    void FixedUpdate() {
        
        this.transform.position=new Vector3(Mathf.FloorToInt(MousePos.x)+0.5f, Mathf.FloorToInt(MousePos.y)+0.5f); ;
        if(blockPlacable()) {
            this.gameObject.GetComponent<SpriteRenderer>().enabled=false;
            Cursor.visible=true;
        }
        else {
            this.gameObject.GetComponent<SpriteRenderer>().enabled=true;
            Cursor.visible=false;
        }
    }
    public bool blockPlacable() {
        MousePos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int position = new Vector3Int(Mathf.FloorToInt(MousePos.x), Mathf.FloorToInt(MousePos.y), 0);
        if(tilemap.GetTile(new Vector3Int(position.x, position.y, 0))==null&&tilemap.GetTile(new Vector3Int(position.x-1, position.y, 0))==null&&tilemap.GetTile(new Vector3Int(position.x, position.y-1, 0))==null&&tilemap.GetTile(new Vector3Int(position.x+1, position.y, 0))==null&&tilemap.GetTile(new Vector3Int(position.x, position.y+1, 0))==null) {
            Placable=false;
            return true;
        }
        else {
            Placable=true;
            return false;
        }
    }
}
