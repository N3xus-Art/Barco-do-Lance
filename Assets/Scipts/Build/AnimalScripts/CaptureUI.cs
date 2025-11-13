using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CaptureUI : MonoBehaviour
{
    public static CaptureUI Instance;

    [Header("UI")]
    [SerializeField] private GameObject capturePanel;
    [SerializeField] private GameObject captureArea;
    [SerializeField] private Image animalImage;

    [Header("Configuração")]
    [SerializeField] private int minClicks = 3;
    [SerializeField] private float movementTime = 1.5f;
    private ICapturable target;
    private Sprite animalSprite => target != null ? target.GetSprite() : null;
    private RectTransform animalRect;
    private Coroutine coroutineMoviment;
    private int currentClicks;

    // Armazena a escala X original para o flip
    private float originalScaleX;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        if (capturePanel != null) capturePanel.SetActive(false);
    }

    // Abre a UI de captura para o animal especificado
    public void OpenCapture(ICapturable animal)
    {
        if (animal == null) return;
        if (captureArea == null)
        {
            Debug.LogError("CaptureUI: captureArea não está atribuído no Inspector.");
            return;
        }

        target = animal;
        currentClicks = 0;
        animalImage.sprite = target.GetSprite();

        if (target.GetSprite() != null)
        {
            animalImage.SetNativeSize();
        }

        if (animalImage.transform.parent != captureArea.transform)
        {
            animalImage.transform.SetParent(captureArea.transform, false);
        }

        animalRect = animalImage.GetComponent<RectTransform>();

        animalRect.anchorMin = new Vector2(0.5f, 0.5f);
        animalRect.anchorMax = new Vector2(0.5f, 0.5f);
        animalRect.pivot = new Vector2(0.5f, 0.5f);
        animalRect.anchoredPosition = Vector2.zero;

        // Guarda a escala X positiva original
        originalScaleX = Mathf.Abs(animalRect.localScale.x);

        capturePanel.SetActive(true);

        if (coroutineMoviment != null) StopCoroutine(coroutineMoviment);
        coroutineMoviment = StartCoroutine(MoveAnimal());
    }

    // Fecha a UI de captura com sucesso na caputura
    private void CloseWithSuccess()
    {
        capturePanel.SetActive(false);
        if (coroutineMoviment != null) StopCoroutine(coroutineMoviment);

        // Restaura a escala original ao fechar
        Vector3 escala = animalRect.localScale;
        escala.x = originalScaleX;
        animalRect.localScale = escala;

        target = null;
    }

    // Fecha a UI de captura sem sucesso na captura
    private void CloseWithoutSuccess()
    {
        capturePanel.SetActive(false);
        if (coroutineMoviment != null) StopCoroutine(coroutineMoviment);

        // Restaura a escala original ao fechar
        Vector3 escala = animalRect.localScale;
        escala.x = originalScaleX;
        animalRect.localScale = escala;

        target = null;
    }

    // Registra um clique do jogador na UI de captura
    public void RegisterClick()
    {
        if (target == null) return;

        currentClicks++;
        Debug.Log($"Cliques: {currentClicks}/{minClicks}");

        if (currentClicks >= minClicks)
        {
            bool capturaComSucesso = PlayerInventoryHandler.Instance.TryCaptureAnimal(target);

            if (capturaComSucesso)
            {
                Debug.Log("SUCESSO: Animal capturado e armazenado!");
                CloseWithSuccess();
            }
            else
            {
                Debug.Log("FALHA: O animal escapou!");
                Vector3 playerPosition = PlayerInventoryHandler.Instance.transform.position;
                target.Run(playerPosition);
                CloseWithoutSuccess();
            }
        }
    }

    // Coroutine que move o animal aleatoriamente dentro da área de captura
    private IEnumerator MoveAnimal()
    {
        if (captureArea == null)
        {
            Debug.LogError("MoveAnimal: captureArea é null.");
            yield break;
        }
        if (animalRect == null)
        {
            Debug.LogError("MoveAnimal: animalRect é null.");
            yield break;
        }
        RectTransform area = captureArea.GetComponent<RectTransform>();
        if (area == null)
        {
            Debug.LogError("MoveAnimal: captureArea não possui RectTransform.");
            yield break;
        }

        yield return null;

        while (true)
        {
            float maxX = (area.rect.width / 2f) - (animalRect.rect.width / 2f);
            float minX = -maxX;
            float maxY = (area.rect.height / 2f) - (animalRect.rect.height / 2f);
            float minY = -maxY;

            if (minX > maxX) minX = maxX = 0;
            if (minY > maxY) minY = maxY = 0;

            Vector2 alvoPos = new Vector2(
                Random.Range(minX, maxX),
                Random.Range(minY, maxY)
            );

            Vector2 inicioPos = animalRect.anchoredPosition;

            // --- LÓGICA DE FLIP ADICIONADA --- > gerada pelo Gemini e não testada
            float direcaoX = alvoPos.x - inicioPos.x;
            Vector3 escala = animalRect.localScale;

            // Se o alvo está à direita (e não estamos muito perto)
            if (direcaoX > 0.1f)
            {
                escala.x = originalScaleX; // Vira para a direita
            }
            // Se o alvo está à esquerda (e não estamos muito perto)
            else if (direcaoX < -0.1f)
            {
                escala.x = -originalScaleX; // Vira para a esquerda
            }
            // Se a mudança for muito pequena (ex: movimento só vertical),
            // ele mantém a direção que já estava.

            animalRect.localScale = escala;
            // --- FIM DA LÓGICA DE FLIP ---

            float t = 0f;
            float mt = Mathf.Max(0.01f, movementTime);

            while (t < mt)
            {
                t += Time.deltaTime;
                animalRect.anchoredPosition = Vector2.Lerp(inicioPos, alvoPos, t / mt);
                yield return null;
            }

            animalRect.anchoredPosition = alvoPos;
            yield return new WaitForSeconds(0.1f);
        }
    }
}