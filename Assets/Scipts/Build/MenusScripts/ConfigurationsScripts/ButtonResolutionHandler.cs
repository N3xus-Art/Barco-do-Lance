using UnityEngine;
using UnityEngine.UI;

public class ButtonResolutionHandler : MonoBehaviour
{
    public ResolutionHandler ResolutionHandler;

    public bool Voltar;

    public void AumentarResolution() { if (ResolutionHandler.Index < ResolutionHandler.MaxIndex) { ResolutionHandler.Index++; } }
    public void DiminuirResolution() { if (ResolutionHandler.Index > 0) { ResolutionHandler.Index--; } }


    public void Update()
    {
        if (Voltar == true) { if (ResolutionHandler.Index == 0) { gameObject.GetComponent<Image>().enabled = false; } else { gameObject.GetComponent<Image>().enabled = true; } }

        if (Voltar == false) { if (ResolutionHandler.Index == ResolutionHandler.MaxIndex) { gameObject.GetComponent<Image>().enabled = false; } else { gameObject.GetComponent<Image>().enabled = true; } }
    }

}
