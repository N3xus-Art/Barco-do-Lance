using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CaptureUI : MonoBehaviour{
    public static CaptureUI Instance;

    [Header("UI")]
    [SerializeField] private GameObject capturePanel;
    [SerializeField] private GameObject captureArea;
    [SerializeField] private Image animalImage;

    [Header("Configuração")]
    [SerializeField] private int minClicks = 3;
    [SerializeField] private float movmentTime = 1.5f;
    private ICapturable target;
    private Sprite animalSprite => target.GetSprite();
    private RectTransform animalRect;
    private Coroutine coroutineMoviment;
    private int currentClicks;

    private void Awake(){
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        capturePanel.SetActive(false);
    }

    public void OpenCapture(ICapturable animal){
        target = animal;
        currentClicks = 0;
        animalImage.sprite = animalSprite;
        // Ajusta o tamanho do RectTransform baseado no tamanho do sprite
        if (animalSprite != null){
            float w = animalSprite.rect.width;
            float h = animalSprite.rect.height;
            animalImage.SetNativeSize(); // mantém tamanho original
            animalImage.rectTransform.sizeDelta = new Vector2(w, h);
        }
        animalRect = animalImage.GetComponent<RectTransform>();
        capturePanel.SetActive(true);
        target.startCapture();
        if (coroutineMoviment != null) StopCoroutine(coroutineMoviment);
        coroutineMoviment = StartCoroutine(MoveAnimal());
    }

    public void CloseCapture(){
        capturePanel.SetActive(false);
        if (coroutineMoviment != null) StopCoroutine(coroutineMoviment);
        Destroy(target.GetGameObject());
        target = null;
    }

    public void RegisterClick(){
        if (target == null) return;
        currentClicks++;
        Debug.Log($"Cliques: {currentClicks}/{minClicks}");
        if (currentClicks >= minClicks) {
            // alvo.FinalizarCaptura();
            CloseCapture();
        }
    }

    private IEnumerator MoveAnimal(){
        RectTransform area = captureArea.GetComponent<RectTransform>();
        while (true){
            Vector2 alvoPos = new Vector2(
                Random.Range(-area.rect.width / 2, area.rect.width / 2),
                Random.Range(-area.rect.height / 2, area.rect.height / 2)
            );
            Vector2 inicioPos = animalRect.anchoredPosition;
            float t = 0;
            while (t < movmentTime) {
                t += Time.deltaTime;
                animalRect.anchoredPosition = Vector2.Lerp(inicioPos, alvoPos, t / movmentTime);
                yield return null;
            }
        }
    }
}
