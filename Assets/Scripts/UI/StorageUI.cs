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
            "Coal: " + storage.coal;
    }
}