using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
public class UIOxigenHandler : MonoBehaviour
{
    public PlayerHandlerDeath Player;
    public Image BarraOxigenio;
    public Image FadeImage;


    public void Fade(bool FadeAway = true)
    {
        if (FadeAway == false)
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                FadeImage.color = new Color(0, 0, 0, i);
            }
        }
        else
        {
            for (float i = 0; i <= 255; i += 1 * Time.deltaTime)
            {
                Debug.Log("Escurecendo a imagem");
                FadeImage.color = new Color(0, 0, 0, i);

            }
        }

    }

    void Update()
    {

        BarraOxigenio.fillAmount = Player.o2 / Player.MaxOxygen;

        if (Player.o2 <= 0)
        {
            Debug.Log("Sem oxigenio");
            Fade();
        }

    }
}
