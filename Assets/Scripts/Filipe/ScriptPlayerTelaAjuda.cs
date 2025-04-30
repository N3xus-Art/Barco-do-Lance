using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ScriptPlayerTelaAjuda : MonoBehaviour
{
    public Canvas TelaAjuda;

    public void HabilitarCanva(InputAction.CallbackContext context)
    {
        
        if(context.phase == InputActionPhase.Started){

            if (TelaAjuda != null)
                {

                    if (TelaAjuda.enabled == false){TelaAjuda.enabled = true;}else{TelaAjuda.enabled = false;}

                }

            }

    }


    void Update()
    {

    }

}
