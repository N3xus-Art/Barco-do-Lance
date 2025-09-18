using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CapturaUI : MonoBehaviour
{
    public static CapturaUI Instance;

    [Header("UI")]
    [SerializeField] private GameObject painelCaptura;
    [SerializeField] private GameObject areaCaptura;
    [SerializeField] private Image imagemAnimal;

    [Header("Configuração")]
    [SerializeField] private int cliquesNecessarios = 3;
    [SerializeField] private float tempoMovimento = 1.5f;

    private ICapturavel alvo;
    private Sprite animalSprite => alvo.GetSprite();
    private RectTransform animalRect;
    private Coroutine movimentoCoroutine;
    private int cliquesAtuais;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        painelCaptura.SetActive(false);
    }

    public void AbrirCaptura(ICapturavel animal)
    {
        alvo = animal;
        cliquesAtuais = 0;

        imagemAnimal.sprite = animalSprite;

        // Ajusta o tamanho do RectTransform baseado no tamanho do sprite
        if (animalSprite != null)
        {
            float w = animalSprite.rect.width;
            float h = animalSprite.rect.height;
            imagemAnimal.SetNativeSize(); // mantém tamanho original
            imagemAnimal.rectTransform.sizeDelta = new Vector2(w, h);
        }

        animalRect = imagemAnimal.GetComponent<RectTransform>();

        painelCaptura.SetActive(true);
        alvo.IniciarCaptura();

        if (movimentoCoroutine != null) StopCoroutine(movimentoCoroutine);
        movimentoCoroutine = StartCoroutine(MoverAnimal());
    }

    public void FecharCaptura()
    {
        painelCaptura.SetActive(false);
        if (movimentoCoroutine != null) StopCoroutine(movimentoCoroutine);
        Destroy(alvo.GetGameObject());
        alvo = null;
    }

    public void RegistrarClique()
    {
        if (alvo == null) return;

        cliquesAtuais++;
        Debug.Log($"Cliques: {cliquesAtuais}/{cliquesNecessarios}");

        if (cliquesAtuais >= cliquesNecessarios)
        {
         // alvo.FinalizarCaptura();
            FecharCaptura();
        }
    }

    private IEnumerator MoverAnimal()
    {
        RectTransform area = areaCaptura.GetComponent<RectTransform>();

        while (true)
        {
            Vector2 alvoPos = new Vector2(
                Random.Range(-area.rect.width / 2, area.rect.width / 2),
                Random.Range(-area.rect.height / 2, area.rect.height / 2)
            );

            Vector2 inicioPos = animalRect.anchoredPosition;
            float t = 0;

            while (t < tempoMovimento)
            {
                t += Time.deltaTime;
                animalRect.anchoredPosition = Vector2.Lerp(inicioPos, alvoPos, t / tempoMovimento);
                yield return null;
            }
        }
    }
}
