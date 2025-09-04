using UnityEngine;
using UnityEngine.InputSystem;

public class MoveTester : MonoBehaviour
{
    bool isMoving;
    public Vector2 direction;
    public float speed;
    public bool isDiagonal;

    public void Move(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            isMoving = true;
            direction = context.ReadValue<Vector2>();
            if (Mathf.Abs(direction.x) > 0 && Mathf.Abs(direction.x) < 1) { direction.x = 1 * Mathf.Sign(direction.x); }


            if (!isDiagonal)
            {
               if (direction.x != 0)
                {
                    direction.y = 0;
                }
            }
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
            // Como você usa 2D, o movimento está no plano XY (x,y,0)
            transform.position += new Vector3(direction.x, direction.y, 0) * Time.deltaTime * speed;
        }
    }
}
