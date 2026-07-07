using UnityEngine;
using System.IO;

//Guarda y carga el progreso del jugador.

public class SaveManager : MonoBehaviour
{
    [Header("References")]

    [SerializeField]
    private StorageBox storage;

    private string savePath;

    private void Start()
    {
        savePath = Path.Combine(
            Application.persistentDataPath,
            "save.json"
        );

        if (storage == null)
        {
            storage = GameStorage.Instance.Storage;
        }
    }

    //Guarda el inventario actual.

    public void SaveGame()
    {
        GameData data = new GameData();

        data.iron = storage.Iron;
        data.copper = storage.Copper;
        data.coal = storage.Coal;

        data.ironIngot = storage.IronIngot;
        data.copperIngot = storage.CopperIngot;

        string json =
            JsonUtility.ToJson(data, true);

        File.WriteAllText(savePath, json);

        Debug.Log("Partida guardada correctamente.");
    }

    //Carga una partida guardada.

    public void LoadGame()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("No existe ninguna partida guardada.");
            return;
        }

        string json =
            File.ReadAllText(savePath);

        GameData data =
            JsonUtility.FromJson<GameData>(json);

        storage.LoadData(
            data.iron,
            data.copper,
            data.coal,
            data.ironIngot,
            data.copperIngot
        );

        Debug.Log("Partida cargada correctamente.");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.F9))
        {
            LoadGame();
        }
    }
}