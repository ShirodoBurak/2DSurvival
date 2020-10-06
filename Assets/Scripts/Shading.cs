using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shading : MonoBehaviour {
    float distance;
    float maxDistance;
    float brightness;
    public CharacterController2D charMove;
    // Start is called before the first frame update
    void Start() {
        maxDistance = this.gameObject.GetComponent<CircleCollider2D>().radius;
    }
    void OnTriggerStay2D(Collider2D collision) {
        if(charMove.body.velocity!=Vector2.zero) {
            Proccess(collision);
        }
    }
    void Proccess(Collider2D other) {
        distance=Vector2.Distance(this.gameObject.transform.position, other.gameObject.transform.position);
        brightness = 1-distance/maxDistance;
        other.gameObject.GetComponent<SpriteRenderer>().color=new Color(brightness, brightness, brightness);
    }

    private void OnTriggerExit2D(Collider2D other) {
        other.gameObject.GetComponent<SpriteRenderer>().color=Color.black;
    }


}
