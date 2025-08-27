using UnityEngine;
using UnityEngine.UI;

public class QuitConfirmation : MonoBehaviour
{
    public GameObject ConfirmExit;

    void Start()
    {
       ConfirmExit.SetActive(false);
    }

    public void ShowQuitConfirmation()
    {
        ConfirmExit.SetActive(true);
    }

    public void Sim()
    {
        Application.Quit();
    }

    public void Não()
    {
        ConfirmExit.SetActive(false);
    }
}