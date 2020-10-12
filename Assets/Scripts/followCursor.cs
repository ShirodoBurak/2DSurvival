using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class followCursor : MonoBehaviour {

    public Vector3 MousePos;
    public RaycastHit2D rayHit;
    public RaycastHit2D hit;
    public Tilemap tilemap;
    void Start() {
        Cursor.visible=false;
    }
    void FixedUpdate() {
        MousePos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int position = new Vector3Int(Mathf.FloorToInt(MousePos.x), Mathf.FloorToInt(MousePos.y), 0);
        this.transform.position=new Vector3(Mathf.FloorToInt(MousePos.x)+0.5f, Mathf.FloorToInt(MousePos.y)+0.5f); ;
        if(tilemap.GetTile(new Vector3Int(position.x, position.y, 0))==null&&tilemap.GetTile(new Vector3Int(position.x-1, position.y, 0))==null&&tilemap.GetTile(new Vector3Int(position.x, position.y-1, 0))==null&&tilemap.GetTile(new Vector3Int(position.x+1, position.y, 0))==null&&tilemap.GetTile(new Vector3Int(position.x, position.y+1, 0))==null) {
            this.gameObject.GetComponent<SpriteRenderer>().enabled=false;
            Cursor.visible=true;
        }
        else {
            this.gameObject.GetComponent<SpriteRenderer>().enabled=true;
            Cursor.visible=false;
        }
    }
}
