using UnityEngine;
using UnityEngine.UI;

public class UIOxigenHandler : MonoBehaviour
{
    public PlayerHandlerDeath Player;

    public GameObject GO_Player;
    public Image BarraOxigenio;
    public float FadeTime;
    public GameObject TextoMorte;
    public float TimerDeath = 0;
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
         
          if (FadeImage.alpha > 0)
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
            if (FadeImage.alpha < 1 && Player.Morreu == false)
            {
                Fade(true);

                if (FadeImage.alpha >= 1) { Player.Morreu = true; TimerDeath = 5; }
            }


            if (TimerDeath > 0)
            {
                TimerDeath -= Time.deltaTime;

                if (TextoMorte.activeSelf == false && TimerDeath < 4.5) { TextoMorte.SetActive(true); }

                if (TimerDeath <= 0) { TextoMorte.SetActive(false); }
            }
            else
            {

                if (Player.Morreu == true)
                {

                    if (FadeImage.alpha > 0){

                        Fade(false);
                        GO_Player.transform.localPosition = new Vector3(0, 0, 0);

                    }else { Player.Morreu = false; Player.o2 = Player.MaxOxygen; }

                }

            }



        }

    }
}
