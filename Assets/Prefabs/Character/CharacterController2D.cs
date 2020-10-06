using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour {
    
    public float speed = 10.0f;
    public float jumpForce = 20.0f;
    public float airDrag = 0.8f;

    public Rigidbody2D body;
    private SpriteRenderer spriteRenderer;

    public Vector2 currentVelocity;
    private float previousPositionY;

    // Start is called before the first frame update
    void Start() {
        body=GetComponent<Rigidbody2D>();
        spriteRenderer=GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {
        Move();
        previousPositionY=transform.position.y;
    }

    private void Move() {
        float velocity = Input.GetAxis("Horizontal")*speed;
        bool isJumping = Input.GetKey(KeyCode.Space);

        velocity*=airDrag;

        // Horizontal Movement
        body.velocity=Vector2.SmoothDamp(body.velocity, new Vector2(velocity, body.velocity.y), ref currentVelocity, 0.02f);

        // Initiate Jump
        if(isJumping) {
            body.AddForce(new Vector2(0, jumpForce));
        }

        // Cancel Jump
        if(!isJumping&&body.velocity.y>0.01f) {
            body.velocity=new Vector2(body.velocity.x, body.velocity.y*0.95f);
        }

        if(velocity<0) spriteRenderer.flipX=true;
        else if(velocity>0)
            spriteRenderer.flipX=false;
    }
}