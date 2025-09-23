using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class OperacaoUI : MonoBehaviour
{
    public static OperacaoUI Instance;

    [Header("Referências")]
    public GameObject painelOperacao;
    public Image animalImage;
    public Transform areaLixos;
    public GameObject[] prefabsLixos;
    public Ferramenta faca;
    public Ferramenta alicate;

    private List<GameObject> lixosAtivos = new List<GameObject>();
    private IOperavel alvoOperacao;
    private Ferramenta ferramentaAtual;

    private void Awake()
    {
        Instance = this;
        painelOperacao.SetActive(false);
    }

    public void AbrirOperacao(IOperavel alvo)
    {
        alvoOperacao = alvo;

        painelOperacao.SetActive(true);
        animalImage.sprite = alvo.GetSprite();

        // Limpa lixos antigos
        foreach (var lixo in lixosAtivos) Destroy(lixo);
        lixosAtivos.Clear();

        // Spawna alguns lixos aleatórios
        int qtd = Random.Range(2, 5);
        RectTransform areaRT = areaLixos.GetComponent<RectTransform>();

        for (int i = 0; i < qtd; i++)
        {
            GameObject lixoPrefab = prefabsLixos[Random.Range(0, prefabsLixos.Length)];
            GameObject lixo = Instantiate(lixoPrefab, areaLixos);

            RectTransform rt = lixo.GetComponent<RectTransform>();

            if (rt != null && areaRT != null)
            {
                // Pega metade do tamanho da área
                float halfWidth = areaRT.rect.width / 2f;
                float halfHeight = areaRT.rect.height / 2f;

                // Sorteia uma posição dentro da área
                float randomX = Random.Range(-halfWidth, halfWidth);
                float randomY = Random.Range(-halfHeight, halfHeight);

                rt.localScale = Vector3.one;
                rt.anchoredPosition = new Vector2(randomX, randomY);
            }

            lixosAtivos.Add(lixo);
        }
    }

    public void EncerrarOperacao()
    {
        Time.timeScale = 1f;
        painelOperacao.SetActive(false);
        alvoOperacao?.FinalizarOperacao();
        alvoOperacao = null;
        ferramentaAtual = null;
    }

    public void SelecionarFerramenta(string nomeFerramenta)
    {
        if (nomeFerramenta == "Faca") ferramentaAtual = faca;
        else if (nomeFerramenta == "Alicate") ferramentaAtual = alicate;

        Debug.Log("Ferramenta atual: " + ferramentaAtual?.tipo);
    }

    public Ferramenta GetFerramentaAtual()
    {
        return ferramentaAtual;
    }
}
