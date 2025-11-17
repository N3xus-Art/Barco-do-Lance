using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class AquariumHandler : MonoBehaviour, IInteractable
{
    public static AquariumHandler Instance { get; private set; }
    // Área onde os peixes podem nadar dentro do aquário
    [SerializeField] private Transform fishSwimArea;
    // Lista para armazenar os peixes dentro do aquário
    public List<GameObject> storedFish = new List<GameObject>();

    // Garante que apenas uma instância do AquariumHandler exista
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (fishSwimArea != null)
            {
                fishSwimArea.SetParent(this.transform);
            }
        }
    }

    // Implementação da interface IInteractable
    public void Interact()
    {
        // Lógica para transferir peixes da Sambura para o aquário
        Debug.Log("Interagindo com o aquário...");
        PlayerInventoryHandler inventory = PlayerInventoryHandler.Instance;
        if (inventory == null) return;
        // Procura pela Sambura no inventário
        SamburaInstanceHandler sambura = inventory.OwnedTools.OfType<SamburaInstanceHandler>().FirstOrDefault();
        if (sambura == null)
        {
            Debug.Log("Nenhuma Sambura encontrada no inventário.");
            return;
        }
        // Verifica se há peixes na Sambura
        if (sambura.occupiedSpace == 0)
        {
            Debug.Log("A Sambura está vazia.");
            return;
        }
        // Transfere os peixes para o aquário
        Debug.Log($"Transferindo {sambura.PeixesArmazenados.Count} peixes para o aquário.");
        // Usar ToList() para evitar problemas de modificação durante a iteração
        foreach (GameObject fishObject in sambura.PeixesArmazenados.ToList())
        {
            AddFishToAquarium(fishObject);
        }
        // Limpa a Sambura após a transferência
        sambura.LimparSambura();
    }
    // Adiciona um peixe ao aquário e o posiciona aleatoriamente na área de nado
    private void AddFishToAquarium(GameObject fishObject)
    {
        if (fishObject == null) return;
        // Define o aquário como o novo pai do peixe
        fishObject.transform.SetParent(fishSwimArea);
        fishObject.transform.position = GetRandomPositionInBounds();
        fishObject.SetActive(true);
        fishObject.GetComponent<ICapturable>().SetCanInteract(false);
        // Chama o comportamento de retorno do peixe, se aplicável
        ICapturable capturable = fishObject.GetComponent<ICapturable>();
        if (capturable != null)
        {
            capturable.ReturnBehaviour();
        }

        storedFish.Add(fishObject);
    }
    // Obtém uma posição aleatória dentro dos limites do fishSwimArea
    private Vector3 GetRandomPositionInBounds()
    {
        // Obtém o Renderer para acessar os limites
        Renderer areaRenderer = fishSwimArea.GetComponent<Renderer>();
        if (areaRenderer == null)
        {
            Debug.LogWarning("Aquarium 'fishSwimArea' não tem Renderer. Posicionando no centro.");
            return fishSwimArea.position;
        }
        Bounds bounds = areaRenderer.bounds;
        // Gera uma posição aleatória dentro dos limites
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            fishSwimArea.position.z
        );
    }

    public void RemoveFish()
    {

        foreach (GameObject FishObject in storedFish)
        {

            Destroy(FishObject);

        }

        storedFish.Clear();

    }

    public void SpawnFishStored()
    {

        if (storedFish.Count <= 0) { return; }
        else
        {
            foreach(GameObject fishObject in storedFish)
            {

                fishObject.transform.SetParent(fishSwimArea);
                fishObject.transform.position = GetRandomPositionInBounds();
                fishObject.SetActive(true);
                fishObject.GetComponent<ICapturable>().SetCanInteract(false);
            }


        }
    }

    public void SetCanInteract(bool Can) { }
}