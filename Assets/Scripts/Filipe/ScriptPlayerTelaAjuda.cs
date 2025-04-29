using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class ScriptPlayerTelaAjuda : MonoBehaviour
{


    public void CriarCanva(InputAction.CallbackContext context)
    {

    if(context.phase == InputActionPhase.Started){

        GameObject canvasGo = new GameObject("UI Canvas");
        Canvas canvas = canvasGo.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

    }

    }


    void Update()
    {

    }

}
