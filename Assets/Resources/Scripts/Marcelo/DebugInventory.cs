using UnityEngine;

public class DebugInventoryTester : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public ToolData pliersData;
    public ToolData scissorsData;
    public ToolData knifeData;

    void showInventory()
    {
        Debug.Log("\nLista de ferramentas:" + string.Join(", ", playerInventory.OwnedTools) +
                  "\nDinheiro:" + playerInventory.Money +
                  "\nFerramenta equipada: " + playerInventory.EquippedTool
                  );
    }

    void Update()
    {
        // P - Compra um Alicate
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (playerInventory.TryBuyTool(pliersData, out var tool))
                Debug.Log("Comprou " + pliersData.toolName);
            else
                Debug.Log("Não conseguiu comprar o Alicate");
            showInventory();
        }

        // T - Compra uma Tesoura
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (playerInventory.TryBuyTool(scissorsData, out var tool))
                Debug.Log("Comprou " + scissorsData.toolName);
            else
                Debug.Log("Não conseguiu comprar a Tesoura");
            showInventory();
        }

        // F - Compra uma Faca
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (playerInventory.TryBuyTool(knifeData, out var tool))
                Debug.Log("Comprou " + knifeData.toolName);
            else
                Debug.Log("Não conseguiu comprar a Faca");
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

        // M - Upgrade ferramenta equipada
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("Tentando Upgrade ferramenta equipada");
            playerInventory.UpgradeEquippedTool();
            showInventory();
        }
    }
}
