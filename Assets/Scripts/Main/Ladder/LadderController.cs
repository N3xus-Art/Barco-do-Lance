using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class LadderController : MonoBehaviour{
    private float vertical;
    private float speed = 8f;
    public float climbDirection = 0f;
    private bool isLadder, isClimbing;

    [SerializeField] private Rigidbody2D rb;

    void Update(){
        if (isLadder && Math.Abs(climbDirection) > 0f) {
            isClimbing = true;
        }

        
    }
    
    public void Climb(InputAction.CallbackContext context)
    {
        climbDirection = context.ReadValue<Vector2>().y;
        if (!isClimbing) climbDirection = 0f;
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(rb.linearVelocityX, vertical * speed);
        }
        else
        {
            rb.gravityScale = 1f;
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
