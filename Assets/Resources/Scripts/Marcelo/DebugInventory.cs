using UnityEngine;

public class DebugInventoryTester : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public ToolData hammerData;
    public ToolData scissorsData;

    void showInventory()
    {
        Debug.Log("\nLista de ferramentas:" + string.Join(", ", playerInventory.OwnedTools) +
                  "\nDinheiro:" + playerInventory.Money +
                  "\nFerramenta equipada: " + playerInventory.EquippedTool
                  );
    }

    void Update()
    {
        // M - Compra um Martelo
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (playerInventory.TryBuyTool(hammerData, out var tool))
                Debug.Log("Comprou " + hammerData.toolName);
            else
                Debug.Log("Não conseguiu comprar o Martelo");
            showInventory();
        }

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

        // X - Vende a ferramenta quebrada
        if (Input.GetKeyDown(KeyCode.X))
        {
            var equipped = playerInventory.EquippedTool;
            if (equipped != null)
            {
                if (playerInventory.TryScrapBroken(equipped.InstanceId, out int payout))
                    Debug.Log($"Scrapped {equipped.Data.toolName} for {payout} coins");
                else
                    Debug.Log("Equipped tool is not broken yet!");
            }
        }

        // E - Equipa a proxima ferramenta
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerInventory.EquipNext();
            Debug.Log("Proxima ferramenta equipada: " + playerInventory.EquippedTool);
        }
    }
}
