using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class CapturaUI : MonoBehaviour
{
    public static CapturaUI Instance;

    private Coroutine movimentCoroutine;
    public GameObject painelCaptura;
    public Image animalImage;
    public Transform AreaCaptura;

    private ICapturavel alvoCaptura;

    private IEnumerator SincronizeMovement()
    {
        var animalGO = alvoCaptura.GetGameObject();
        var canvas = animalImage.canvas;
        var camera = Camera.main;

        while (painelCaptura.activeSelf && animalGO != null)
        {
            // Pega a posição do animal no mundo
            Vector3 posMundo = animalGO.transform.position;

            // Converte para posição de tela
            Vector3 posTela = camera.WorldToScreenPoint(posMundo);

            // Converte para posição local do canvas
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                animalImage.rectTransform.parent as RectTransform,
                posTela,
                canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : camera,
                out Vector2 posLocal
            );

            animalImage.rectTransform.localPosition.Equals(posLocal);

            yield return null;
        }
    }

    private void Awake()
    {
        Instance = this;
        painelCaptura.SetActive(false);
    }

    public void AbrirCaptura(ICapturavel alvo)
    {
        alvoCaptura = alvo;

        painelCaptura.SetActive(true);
        animalImage.sprite = alvo.GetSprite();

        if (movimentCoroutine != null)
            StopCoroutine(movimentCoroutine);
        movimentCoroutine = StartCoroutine(SincronizeMovement());
    }
    public void EncerrarCaptura()
    {
        painelCaptura.SetActive(false);
        if (movimentCoroutine != null)
        {
            StopCoroutine(movimentCoroutine);
            movimentCoroutine = null;
        }
        Destroy(alvoCaptura.GetGameObject());
        alvoCaptura = null;
    }
}
