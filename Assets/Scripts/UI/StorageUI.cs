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
            "Iron: " + storage.Iron + "\n" +
            "Copper: " + storage.Copper + "\n" +
            "Coal: " + storage.Coal + "\n" +
            "Iron Ingot: " + storage.IronIngot + "\n" +
            "Copper Ingot: " + storage.CopperIngot;
    }
}