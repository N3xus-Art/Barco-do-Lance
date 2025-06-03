using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HelpingController : MonoBehaviour
{
    
    [SerializeField] private GameObject HelpingScreen;
    [SerializeField] private Transform Image;
    [SerializeField] private GameObject button1, button2, button3, button4, button5, button6;
    [SerializeField] private int buttonsCont = 0;

    public void HabilitarCanva(InputAction.CallbackContext context) {
        if(context.phase == InputActionPhase.Started){
            if (HelpingScreen != null && HelpingScreen.activeSelf == false){
                    HelpingScreen.SetActive(true);
                    button1.SetActive(true);
                    button2.SetActive(true);
                    button3.SetActive(true);
                    button4.SetActive(true);
                    button5.SetActive(true);
                    button6.SetActive(true);       
                    if (Image != null){
                        foreach (Transform child in Image){
                            Button button = child.GetComponent<Button>();
                            if (button != null){
                                buttonsCont++;
    }}}}}}
    void Update(){   
        if (buttonsCont <= 0){ HelpingScreen.SetActive(false); };
    }
}
