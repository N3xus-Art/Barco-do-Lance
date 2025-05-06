using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ScriptPlayerTelaAjuda : MonoBehaviour
{
    public Canvas TelaAjuda;
    public Transform Image;

    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;
    public GameObject button6;

    public int numBotoes = 0;

    public void HabilitarCanva(InputAction.CallbackContext context)
    {
        
        if(context.phase == InputActionPhase.Started){

            if (TelaAjuda != null && TelaAjuda.enabled == false){
                    Debug.Log("doce");

                    button1.SetActive(true);
                    button2.SetActive(true);
                    button3.SetActive(true);
                    button4.SetActive(true);
                    button5.SetActive(true);
                    button6.SetActive(true);

                    if (Image != null)
                    {

                        foreach (Transform child in Image)
                        {
                            Button button = child.GetComponent<Button>();

                            if (button != null)
                            {
                                numBotoes++;
                            }
                        }
                    }

                    TelaAjuda.enabled = true;
            }
        }
    }

    void Start()
    {

    }


    void Update()
    {   

        if (numBotoes == 0){ TelaAjuda.enabled = false; };

    }

}
