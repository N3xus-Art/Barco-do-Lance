using UnityEngine;
using UnityEngine.InputSystem;

public class LadderController : MonoBehaviour{
    [SerializeField] private PlayerInputManager playerinputManager;
    [SerializeField] private Rigidbody2D rb;
    private float vertical;
    private bool isLadder, isClimbing;


    void Update(){
        vertical = Input.GetAxis("Vertical");
        if (isLadder && Mathf.Abs(vertical) > 0f){
            isClimbing = true;
        }
    }

    private void FixedUpdate(){
        if (isClimbing){
            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(rb.linearVelocityX, vertical * playerinputManager.speed);
        }else{
            rb.gravityScale = 1f;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Ladder")){
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if (collision.CompareTag("Ladder")){
            isLadder = false;
            isClimbing = false;
        }
    }
}


