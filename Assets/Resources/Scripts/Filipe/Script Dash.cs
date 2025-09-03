using UnityEngine;
using UnityEngine.InputSystem;

public class ScriptDash : MonoBehaviour
{
    bool isDashing = false; 

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {

            isDashing = true;

        }
        else if (context.phase == InputActionPhase.Canceled){ isDashing = false; }
    }

    private void Update()
    {
        if (isDashing) {

            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x + (5 * Time.deltaTime) * gameObject.GetComponent<MoveTester>().horizontalMoviment, gameObject.transform.localPosition.y,gameObject.transform.localPosition.z);
        
        }

        gameObject.GetComponent<Transform>().localScale = new Vector3(gameObject.GetComponent<MoveTester>().horizontalMoviment, 1,1);
    }
}
