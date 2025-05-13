using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
public class ScriptPlayerMovement : MonoBehaviour
{

    bool isMoving;
    Vector2 direction;

    public Transform Item;

    float range = 2f;

    public GameObject Tecla;

    public void Move(InputAction.CallbackContext context)
    {
        
        if (context.phase == InputActionPhase.Performed){

        isMoving = true;
        direction = context.ReadValue<Vector2>();

        }

        if (context.phase == InputActionPhase.Canceled){

            isMoving = false;

        }

    }

    void Update()
    {
        if (isMoving == true){

            transform.position += new Vector3(direction.x,direction.y,0) * Time.deltaTime * 5;

        }

        if (Vector3.Distance(transform.position, Item.position) >= range){

            if (!Tecla.activeSelf){
            Tecla.SetActive(true);
            }

        }else{

            Tecla.SetActive(false);

        }

        

    }
}
