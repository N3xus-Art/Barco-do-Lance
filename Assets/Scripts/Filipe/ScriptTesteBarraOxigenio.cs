using UnityEngine;
using UnityEngine.UI;

public class ScriptTesteBarraOxigenio : MonoBehaviour
{

    public Image BarraOxigenio;
    public GameObject player;
    public float MaxOxygen, OxygenCost, Oxygen;

    void Update()
    {
        Oxygen = player.GetComponent<ScriptTesteHud>().Oxigenio;
        Oxygen -= OxygenCost;

        if (Oxygen < 0) {Oxygen = 0;}
        
        BarraOxigenio.fillAmount = Oxygen / MaxOxygen; 
    }
    
}
