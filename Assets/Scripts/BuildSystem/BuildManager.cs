using UnityEngine;

// Gestiona el modo de construcción del jugador.

public class BuildManager : MonoBehaviour
{
    
    [Header("Prefabs")]

    [SerializeField] private GameObject storagePrefab;
    [SerializeField] private GameObject minerPrefab;
    [SerializeField] private GameObject smelterPrefab;
    [SerializeField] private GameObject conveyorPrefab;
    [SerializeField] private GameObject generatorPrefab;

    [Header("UI")]

    [SerializeField] private BuildUI buildUI;

    [Header("Building Costs")]

    [SerializeField] private int storageCost = 3;
    [SerializeField] private int minerCost = 5;
    [SerializeField] private int smelterCost = 10;
    [SerializeField] private int conveyorCost = 2;
    [SerializeField] private int generatorCost = 15;

    [SerializeField] private GhostBuilding ghost;

    //Devuelve el Storage principal del juego.
    private StorageBox Storage => GameStorage.Instance.Storage;

    private GameObject selectedBuilding;

    private bool buildMode;
    
    private void Start()
    {
        if (GameStorage.Instance == null)
        {
            Debug.LogError("GameStorage no existe en la escena.");
            enabled = false;
            return;
        }

        if (Storage == null)
        {
            Debug.LogError("No existe un Storage asignado.");
            enabled = false;
            return;
        }

        SelectBuilding(
            minerPrefab,
            "Miner Mk1",
            minerCost
        );

        Debug.Log(
            $"Storage utilizado: {Storage.name}"
        );
    }

    private void Update()
    {
        HandleBuildingSelection();

        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleBuildMode();
        }

        if (buildMode &&
            Input.GetMouseButtonDown(0))
        {
            PlaceBuilding();
        }
    }
    
    //Gestiona las teclas de selección de edificios.
    private void HandleBuildingSelection()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SelectBuilding(storagePrefab, "Storage", storageCost);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            SelectBuilding(minerPrefab, "Miner Mk1", minerCost);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            SelectBuilding(smelterPrefab, "Smelter Mk1", smelterCost);

        if (Input.GetKeyDown(KeyCode.Alpha4))
            SelectBuilding(conveyorPrefab, "Conveyor Mk1", conveyorCost);

        if (Input.GetKeyDown(KeyCode.Alpha5))
            SelectBuilding(generatorPrefab, "Coal Generator", generatorCost);
    }

    //Selecciona un edificio para construir.
    private void SelectBuilding(
        GameObject prefab,
        string buildingName,
        int cost)
    {
        selectedBuilding = prefab;

        buildUI.UpdateSelectedBuilding(
            buildingName,
            cost
        );
    }

    //Activa o desactiva el modo construcción.
    private void ToggleBuildMode()
    {
        buildMode = !buildMode;

        Debug.Log(
            $"Build Mode: {buildMode}"
        );
    }

    //Intenta colocar el edificio seleccionado.
    private void PlaceBuilding()
    {
        // Verificar si el Ghost está en una posición válida
        if (!ghost.CanBuild())
            return;

        //  Verificar recursos
        int cost = GetBuildCost();

        if (!Storage.RemoveIron(cost))
        {
            Debug.Log("No hay suficiente Iron.");
            return;
        }

        //  Construir el edificio
        GameObject building = Instantiate(
            selectedBuilding,
            ghost.transform.position,
            ghost.transform.rotation
        );

        //  Actualizar conexiones
        ConveyorMk1[] conveyors = FindObjectsByType<ConveyorMk1>();

        foreach (ConveyorMk1 conveyor in conveyors)
        {
            conveyor.RefreshConnections();
        }

        SmelterMk1[] smelters = FindObjectsByType<SmelterMk1>();

        foreach (SmelterMk1 smelter in smelters)
        {
            smelter.RefreshConnections();
        }
    }

    // Devuelve el costo del edificio seleccionado.
    private int GetBuildCost()
    {
        if (selectedBuilding == storagePrefab)
            return storageCost;

        if (selectedBuilding == minerPrefab)
            return minerCost;

        if (selectedBuilding == smelterPrefab)
            return smelterCost;

        if (selectedBuilding == conveyorPrefab)
            return conveyorCost;

        if (selectedBuilding == generatorPrefab)
            return generatorCost;

        return 0;
    }
    
}