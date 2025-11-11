using System.Collections;
using UnityEngine;

public class FadeHandler : MonoBehaviour
{
    [SerializeField] private PlayerHandler _PlayerHander;
    [SerializeField] private CanvasGroup _CanvasGroup;
    [SerializeField] private float FadeDuration = 5.0f;

    public void FadeOut(){

        StartCoroutine(Fade(_CanvasGroup, _CanvasGroup.alpha, 1, FadeDuration));

    }

    public void FadeIn() { 
    
        StartCoroutine(Fade(_CanvasGroup, _CanvasGroup.alpha, 0, FadeDuration));

    }

    public void StopFade()
    {

        StopAllCoroutines();

    }

    private IEnumerator Fade(CanvasGroup cg, float start, float end, float durantion)
    {

        float ElapsedTime = 0.0f;

        while (ElapsedTime < durantion)
        {

            ElapsedTime += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, ElapsedTime / durantion);
            yield return null;

        }
        cg.alpha = end;

        if (end == 1)
        {
            _PlayerHander.Morrer();

        }

        if (end == 0) { 
        
            _PlayerHander.Spawn();
        
        }
    }
}
