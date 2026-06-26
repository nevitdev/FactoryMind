using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public StorageBox storage;

    private string savePath;

    private void Start()
    {
        savePath =
            Application.persistentDataPath +
            "/save.json";

        if (storage == null)
            storage = GameStorage.Instance.storage;
    }

    public void SaveGame()
    {
        GameData data = new GameData();

        data.iron = storage.iron;
        data.copper = storage.copper;
        data.coal = storage.coal;

        data.ironIngot = storage.ironIngot;
        data.copperIngot = storage.copperIngot;

        string json =
            JsonUtility.ToJson(data, true);

        File.WriteAllText(savePath, json);

        Debug.Log("Partida guardada");
    }

    public void LoadGame()
    {
        if (!File.Exists(savePath))
            return;

        string json =
            File.ReadAllText(savePath);

        GameData data =
            JsonUtility.FromJson<GameData>(json);

        storage.iron = data.iron;
        storage.copper = data.copper;
        storage.coal = data.coal;

        storage.ironIngot = data.ironIngot;
        storage.copperIngot = data.copperIngot;

        Debug.Log("Partida cargada");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
            SaveGame();

        if (Input.GetKeyDown(KeyCode.F9))
            LoadGame();
    }
}