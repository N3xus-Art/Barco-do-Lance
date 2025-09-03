using UnityEngine;
using UnityEngine.InputSystem;

public class MoveTester : MonoBehaviour
{
    bool isMoving;
    Vector2 direction;
    public float speed;
    public float horizontalMoviment = 1;
    public float verticalMoviment;

    public void Move(InputAction.CallbackContext context)
    {

        if (context.phase == InputActionPhase.Performed)
        {
            isMoving = true;
            direction = context.ReadValue<Vector2>();
            horizontalMoviment = context.ReadValue<Vector2>().x;
            verticalMoviment = context.ReadValue<Vector2>().y;


        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            isMoving = false;

        }
    }

     void Update()
    {

        if (isMoving)
        {

            transform.position += new Vector3(direction.x, direction.y, 0) * Time.deltaTime * speed;

        }

    }
}
