using UnityEngine;

public class WaterCollisionCTRL : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        rb.gravityScale = 0f;
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        rb.gravityScale = 1f;
    }
}
