using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
public class UIOxigenHandler : MonoBehaviour
{
    public PlayerHandlerDeath Player;
    public Image BarraOxigenio;
    public float FadeTime;
    public CanvasGroup FadeImage;

    public void Fade(bool FadeAway = true)
    {
        if (FadeAway)
        {
            if (FadeImage.alpha < 1)
            {

                FadeImage.alpha += FadeTime * Time.deltaTime;

            }
        }
        else
        {
         
          if (FadeImage.alpha > 1)
            {

                FadeImage.alpha -= FadeTime * Time.deltaTime;

            }
            
        }

    }

    void Update()
    {

        BarraOxigenio.fillAmount = Player.o2 / Player.MaxOxygen;

        if (Player.o2 <= 0)
        {
            if (FadeImage.alpha < 1)
            {
                Debug.Log("Sem Oxigênio");
                Fade(true);
            }
            else
            {
                Fade(false);
            }

        }

    }
}
