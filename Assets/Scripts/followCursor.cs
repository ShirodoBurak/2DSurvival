using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followCursor : MonoBehaviour {

    public Vector3 MousePos;
    public RaycastHit2D rayHit;
    public RaycastHit2D hit;
   
    void Start() {
        Cursor.visible=false;
        StartCoroutine(BreakBlock());
    }
    void FixedUpdate() {
        hit=Physics2D.Raycast(origin: MousePos, direction: Vector2.zero);
        MousePos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
       
        this.transform.position=new Vector3(MousePos.x, MousePos.y, 0);
    }
    IEnumerator BreakBlock() {
        while (true)
        {
            if(Input.GetMouseButtonDown(0)) {
                if(hit.collider!=null&&hit.collider.gameObject.name!="Player") {
                    Destroy(hit.collider.gameObject);
                }
            }
            yield return new WaitForSeconds(.001f);
        }
    }
}
