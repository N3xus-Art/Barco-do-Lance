using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class OperationUI : MonoBehaviour{
    public static OperationUI Instance;
    [Header("Referências")]
    public GameObject operationPanel;
    public Image animalImage;
    public Transform trashArea;
    public GameObject[] trashPrefabs;
    private List<GameObject> activeTrashs = new List<GameObject>();
    private IOperable operationTarget;

    private void Awake() {
        Instance = this;
        operationPanel.SetActive(false);
    }
    public void OpenOperation(IOperable target) {
        operationTarget = target;

        operationPanel.SetActive(true);
        animalImage.sprite = target.GetSprite();

        // Limpa lixos antigos
        foreach (var trash in activeTrashs) Destroy(trash);
        activeTrashs.Clear();

        // Spawna alguns lixos aleatórios
        int qtd = Random.Range(2, 5);
        RectTransform areaRT = trashArea.GetComponent<RectTransform>();

        for (int i = 0; i < qtd; i++) {
            GameObject trashPrefab = trashPrefabs[Random.Range(0, trashPrefabs.Length)];
            GameObject trash = Instantiate(trashPrefab, trashArea);

            RectTransform rt = trash.GetComponent<RectTransform>();

            if (rt != null && areaRT != null) {
                // Pega metade do tamanho da área
                float halfWidth = areaRT.rect.width / 2f;
                float halfHeight = areaRT.rect.height / 2f;

                // Sorteia uma posição dentro da área
                float randomX = Random.Range(-halfWidth, halfWidth);
                float randomY = Random.Range(-halfHeight, halfHeight);

                rt.localScale = Vector3.one;
                rt.anchoredPosition = new Vector2(randomX, randomY);
            }

            activeTrashs.Add(trash);
        }
    }

    public void EndOperation() {
        Time.timeScale = 1f;
        operationPanel.SetActive(false);
        operationTarget?.FinalizeOperation();
        operationTarget = null;
    }
}
