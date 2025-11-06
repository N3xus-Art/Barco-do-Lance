using UnityEngine;

public class FadeHandler : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private PlayerHandler _PlayerHandler;
    [SerializeField] private CanvasGroup FadeImage;
    [SerializeField] private float Speed;

    public void Fade(bool fade)
    {
        if (fade)
        {
            if (FadeImage.alpha < 1)
            {

                FadeImage.alpha += Speed * Time.deltaTime;

            }
            else
            {

                _PlayerHandler.Morrer();

            }
        }
        else
        {

            if (FadeImage.alpha > 0)
            {

                FadeImage.alpha -= Speed * Time.deltaTime;

            }
            else
            {


            }

        }

    }

}
