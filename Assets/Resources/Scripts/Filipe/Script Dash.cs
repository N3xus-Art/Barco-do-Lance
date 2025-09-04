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

            if (gameObject.GetComponent<MoveTester>().direction.x != 0)
            {
                gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x + (5 * Time.deltaTime * Mathf.Sign(gameObject.GetComponent<MoveTester>().direction.x)), gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
            }

            if (gameObject.GetComponent<MoveTester>().direction.y != 0)
            {
                gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y + (5 * Time.deltaTime * Mathf.Sign(gameObject.GetComponent<MoveTester>().direction.y)), gameObject.transform.localPosition.z);

            }
        }

        
    }
}
