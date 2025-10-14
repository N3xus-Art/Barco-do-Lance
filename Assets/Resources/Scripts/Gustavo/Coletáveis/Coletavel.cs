using UnityEngine;

public class Collectible : MonoBehaviour, IInteractble
{
    public string collectibleID;

    public void Interact()
    {
        // Verifica se o ID não está vazio para evitar erros.
        if (string.IsNullOrEmpty(collectibleID))
        {
            Debug.LogError("O ID do coletável não foi definido no objeto: " + gameObject.name);
            return;
        }

        CollectItem();
    }

    private void CollectItem()
    {
        Debug.Log("Você coletou: " + collectibleID);

        // Salva no PlayerPrefs que este item foi coletado.
        // Usamos '1' para representar 'verdadeiro' (foi pego).
        PlayerPrefs.SetInt(collectibleID, 1);
        PlayerPrefs.Save(); // Garante que a informação seja salva imediatamente.

        Destroy(gameObject);
    }
}