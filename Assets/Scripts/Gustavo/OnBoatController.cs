using UnityEngine;
using UnityEngine.InputSystem;

public class OnBoatController : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] private Rigidbody2D rb;

    void Update()
    {

    }
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boat"))
        {
            waterMovement = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Boat"))
        {
            waterMovement = true;
        }
    }*/
}
