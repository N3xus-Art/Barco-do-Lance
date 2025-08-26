using UnityEngine;
using UnityEngine.InputSystem;

public class WaterCollisionCTRL : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name);
        rb.gravityScale = 0;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        rb.gravityScale = 1;
    }
}
