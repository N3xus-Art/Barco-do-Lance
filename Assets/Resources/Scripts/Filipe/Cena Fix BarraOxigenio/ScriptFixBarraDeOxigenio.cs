using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class ScriptFixBarraDeOxigenio : MonoBehaviour
{

    [SerializeField]
    float rotation; 
    
    public float o2 = 100; int MaxO2 = 100;


    void Update()
    {

        if (o2 > 0) { o2 -= 1 * Time.deltaTime; }    

        rotation = -160f + (o2/MaxO2) * (160f - (-160f));

        // tank full oxigen 160, zero oxigen -160
        gameObject.GetComponent<RectTransform>().rotation = Quaternion.Euler(0f, 0f, -rotation);


    }

}
