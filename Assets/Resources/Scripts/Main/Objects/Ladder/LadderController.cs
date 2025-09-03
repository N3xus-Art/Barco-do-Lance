using UnityEngine;
using UnityEngine.InputSystem;

public class LadderController : MonoBehaviour{
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] private Rigidbody2D rb;
    private float vertical;
    private bool isLadder, isClimbing;


    void Update(){
        vertical = Input.GetAxis("Vertical");
        if (isLadder && Mathf.Abs(vertical) > 0f){
            isClimbing = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Ladder")){
            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(rb.linearVelocityX, (vertical * playerInputHandler.speed));
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if (collision.CompareTag("Ladder")){
            rb.gravityScale = 1f;
        }
    }
}


