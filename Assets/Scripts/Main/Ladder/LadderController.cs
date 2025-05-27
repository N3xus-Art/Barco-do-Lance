using System;
using UnityEngine;
using UnityEngine.AI;

public class LadderController : MonoBehaviour{
    private float vertical;
    private float speed = 8f;
    private bool isLadder, isClimbing;

    [SerializeField] private Rigidbody2D rb;

    void Update(){
        vertical = Input.GetAxis("Vertical");
        if (isLadder && Math.Abs(vertical) > 0f) {
            isClimbing = true;
        }

        
    }

    private void FixedUpdate(){
        if (isClimbing) {
            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(rb.linearVelocityX, vertical * speed);
        }else{
            rb.gravityScale = 4f;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }



    }

    private void OnTriggerExit2D(Collider2D collision){
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
            


        }




    }
}
