using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour{
    private Character character;
    private Animator animator;
    
    private Rigidbody2D rigidBody;
    private Vector3 speed;
    private bool lookDirection = true;

    void Awake(){
        character = gameObject.GetComponent<Character>();
        animator = character.animator;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update(){
        speed = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * character.speed;
        if((speed.x > 0 && lookDirection == false) || (speed.x < 0 && lookDirection == true)){
            CharacterFlip();
        }

        CharacterMove();
    }

    void FixedUpdate(){
        rigidBody.velocity = speed;
        
    }

    void CharacterMove(){
        animator.SetFloat("speed", Pythagoras(speed.x, speed.y));
        // transform.position += speed;
    }

    void CharacterFlip(){
        lookDirection = !lookDirection;

        transform.Rotate(0f, 180f, 0f);
    }

    float Pythagoras(float a, float b){
        return Mathf.Sqrt(Mathf.Pow(a, 2) + Mathf.Pow(b, 2));
    }
}
