using UnityEngine;

public class DebugInventoryTester : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public ToolData hammerData;
    public ToolData scissorsData;

    void Update()
    {
        // B - Buy Hammer
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (playerInventory.TryBuyTool(hammerData, out var tool))
                Debug.Log("Bought " + tool);
            else
                Debug.Log("Could not buy Hammer");
        }

        // N - Buy Scissors
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (playerInventory.TryBuyTool(scissorsData, out var tool))
                Debug.Log("Bought " + tool);
            else
                Debug.Log("Could not buy Scissors");
        }

        // U - Use Equipped Tool
        if (Input.GetKeyDown(KeyCode.U))
        {
            bool success = playerInventory.TryUseEquipped();
            Debug.Log(success ? "Used equipped tool" : "No tool to use!");
        }

        // S - Scrap Equipped Tool (if broken)
        if (Input.GetKeyDown(KeyCode.S))
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

        // E - Equip Next Tool
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerInventory.EquipNext();
            Debug.Log("Equipped next tool: " + playerInventory.EquippedTool);
        }
    }
}
