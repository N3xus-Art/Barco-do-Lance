using UnityEngine;
using UnityEngine.UI;

public class ScriptTesteBarraOxigenio : MonoBehaviour
{

    public Image BarraOxigenio;
    public GameObject player;
    public float MaxOxigen, OxigenCost, Oxigen;

    void Update()
    {
        Oxigen = player.GetComponent<ScriptTesteHud>().Oxigenio;
        Oxigen -= OxigenCost;

        if (Oxigen < 0) {Oxigen = 0;}
        
        BarraOxigenio.fillAmount = Oxigen / MaxOxigen; 
    }
    
}
