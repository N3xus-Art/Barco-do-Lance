using UnityEngine;

public class DebugInventoryTesterHandler : MonoBehaviour
{
    public PlayerInventoryHandler playerInventory;
    public ToolDataHandler scissorsData;

    void showInventory()
    {
        Debug.Log("\nLista de ferramentas:" + string.Join(", ", playerInventory.OwnedTools) +
                  "\nDinheiro:" + playerInventory.Money +
                  "\nFerramenta equipada: " + playerInventory.EquippedTool
                  );
    }

    void Update()
    {
        // T - Compra uma Tesoura
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (playerInventory.TryBuyTool(scissorsData, out var tool))
                Debug.Log("Bought " + scissorsData.toolName);
            else
                Debug.Log("Não conseguiu comprar o Martelo");
            showInventory();
        }

        // U - Use Equipped Tool
        if (Input.GetKeyDown(KeyCode.U))
        {
            bool success = playerInventory.TryUseEquipped();
            Debug.Log(success ? "Usou a ferramenta equipada" : "Sem ferramentas para usar!");

        }

        // E - Equipa a proxima ferramenta
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerInventory.EquipNext();
            Debug.Log("Proxima ferramenta equipada: " + playerInventory.EquippedTool);
        }
    }
}
