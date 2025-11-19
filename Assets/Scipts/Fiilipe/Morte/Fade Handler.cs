using System;
using System.Collections;
using UnityEngine;

public class FadeHandler : MonoBehaviour
{
    [SerializeField] private CanvasGroup _CanvasGroup;
    [SerializeField] private float FadeDuration = 5.0f;
    [SerializeField] private float Start = 0;

    
    public void FadeOut(float Time,Action Function = null){

        // Come�a a courotina do valor do fade pra at� chegar a 1 (completamente escuro)
        StartCoroutine(Fade(_CanvasGroup, _CanvasGroup.alpha, 1, FadeDuration, Function));

    }

    public void FadeIn(float Time,Action Function = null) {

        // Come�a a courotina do valor do fade pra at� chegar a 0 (completamente vazio)
        StartCoroutine(Fade(_CanvasGroup, _CanvasGroup.alpha, 0, FadeDuration, Function));
      
    }

    public void StopFade()
    {
        // Para a coroutina e seta o fade pro valor inicial
        StopAllCoroutines();
        _CanvasGroup.alpha = Start;
    }

    private IEnumerator Fade(CanvasGroup cg, float start, float end, float durantion, Action function = null)
    {
        // seta o numero inicial do fade, pra se precisar parar o fade
        Start = start;

        // peguei esse c�digo de fade de um v�deo mas acho que da pra entender o que ele faz aqui kkssksksksks
        float ElapsedTime = 0.0f;

        while (ElapsedTime < durantion)
        {

            ElapsedTime += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, ElapsedTime / durantion);
            yield return null;

        }

        // P�s Fade
        cg.alpha = end;

        // Checa se tem alguma fun��o pra executar depois do fade
        if (function != null) {

            // roda a fun��o
            function();
        
        }
    }
}
