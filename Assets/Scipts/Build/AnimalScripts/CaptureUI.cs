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

    public void OpenCapture(ICapturable animal)
    {
        if (animal == null) return;
        target = animal;
        currentClicks = 0;
        animalImage.sprite = target.GetSprite();

        if (target.GetSprite() != null)
        {
            animalImage.SetNativeSize();
        }

        animalRect = animalImage.GetComponent<RectTransform>();
        capturePanel.SetActive(true);

        if (coroutineMoviment != null) StopCoroutine(coroutineMoviment);
        coroutineMoviment = StartCoroutine(MoveAnimal());
    }

    private void CloseWithSuccess(){
        capturePanel.SetActive(false);
        if (coroutineMoviment != null) StopCoroutine(coroutineMoviment);

        if (target != null && target.GetGameObject() != null)
        {
            Destroy(target.GetGameObject());
        }
        target = null;
    }

    private void CloseWithoutSuccess(){
        capturePanel.SetActive(false);
        if (coroutineMoviment != null) StopCoroutine(coroutineMoviment);
        target = null;
    }

    public void RegisterClick(){
        if (target == null) return;

        currentClicks++;
        Debug.Log($"Cliques: {currentClicks}/{minClicks}");

        if (currentClicks >= minClicks)
        {
            bool capturaComSucesso = PlayerInventoryHandler.Instance.TryCaptureAnimal(target);

            if (capturaComSucesso)
            {
                Debug.Log("SUCESSO FINAL: Animal capturado e armazenado!");
                CloseWithSuccess();
            }
            else
            {
                Debug.Log("FALHA FINAL: O animal escapou!");
                Vector3 playerPosition = PlayerInventoryHandler.Instance.transform.position;
                target.Run(playerPosition);
                CloseWithoutSuccess();
            }
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
