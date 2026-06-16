using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public TextMeshProUGUI inventoryText;

    private void Update()
    {
        inventoryText.text =
            $"Iron: {inventory.iron}\n" +
            $"Copper: {inventory.copper}\n" +
            $"Coal: {inventory.coal}";
    }
}