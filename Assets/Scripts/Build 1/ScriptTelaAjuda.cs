using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ScriptTelaAjuda : MonoBehaviour
{
    public Transform Imagem;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;
    public GameObject button6;

    public int numBotoes = 0;

    void OnEnable(){

        button1.SetActive(true);
        button2.SetActive(true);
        button3.SetActive(true);
        button4.SetActive(true);
        button5.SetActive(true);
        button6.SetActive(true);       

        foreach (Transform child in Imagem)
        {
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                numBotoes++;
            }
        }
    }
    

    void Update()
    {   

        if (numBotoes <= 0){ gameObject.SetActive(false); };

    }

}