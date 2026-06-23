using UnityEngine;
using TMPro;

public class StorageUI : MonoBehaviour
{
    public StorageBox storage;
    public TextMeshProUGUI storageText;

    private void Update()
    {
        storageText.text =
            "STORAGE\n\n" +
            "Iron: " + storage.iron + "\n" +
            "Copper: " + storage.copper + "\n" +
            "Coal: " + storage.coal + "\n"  +
            "Iron Ingot: " + storage.ironIngot + "\n" +
            "Copper Ingot: " + storage.copperIngot + "\n";
    }
}