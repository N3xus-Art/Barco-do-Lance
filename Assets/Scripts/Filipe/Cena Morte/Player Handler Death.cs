using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandlerDeath : MonoBehaviour
{
    public float o2, MaxOxygen, OxigenCost;

    public int velocity;

    Vector2 direction;

    public bool Morreu = false, isMoving;

    public void Move(InputAction.CallbackContext context)
    {

        if (context.phase == InputActionPhase.Performed)
        {
            isMoving = true;
            direction = context.ReadValue<Vector2>();


        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            isMoving = false;

        }


    }
    void Update()
    {
        if (o2 > 0) { o2 = o2 - OxigenCost; }
    
        if (isMoving && Morreu == false)
        {
            
            transform.position += new Vector3(direction.x, direction.y, 0) * Time.deltaTime * velocity;

        }

    }

}