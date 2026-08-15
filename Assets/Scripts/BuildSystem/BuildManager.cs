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
    [SerializeField] private GameObject constructorPrefab;

    [Header("UI")]

    [SerializeField] private BuildUI buildUI;

    [Header("Building Costs")]

    [SerializeField] private int storageCost = 3;
    [SerializeField] private int minerCost = 5;
    [SerializeField] private int smelterCost = 10;
    [SerializeField] private int conveyorCost = 2;
    [SerializeField] private int generatorCost = 15;
    [SerializeField] private int constructorCost = 12;

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

        ghost.gameObject.SetActive(false);

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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            buildMode = false;
            ghost.gameObject.SetActive(false);
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

        if (Input.GetKeyDown(KeyCode.Alpha6))
            SelectBuilding(constructorPrefab,"Constructor Mk1",constructorCost);
    }

    //Selecciona un edificio para construir.
    private void SelectBuilding(
        GameObject prefab,
        string buildingName,
        int cost)
    {
        selectedBuilding = prefab;

        ghost.SetBuilding(prefab);

        buildUI.UpdateSelectedBuilding(
            buildingName,
            cost
        );
    }

    //Activa o desactiva el modo construcción.
    private void ToggleBuildMode()
    {
        buildMode = !buildMode;

        ghost.gameObject.SetActive(buildMode);

        Debug.Log($"Build Mode: {buildMode}");
    }

    //Intenta colocar el edificio seleccionado.
    private void PlaceBuilding()
    {
        Debug.Log(
    $"Intentando construir {selectedBuilding.name} | " +
    $"Posición: {ghost.transform.position} | " +
    $"Puede construir: {ghost.CanBuild()}"
);
        // 1. Verificar posición
        if (!ghost.CanBuild())
        {
            Debug.Log("No se puede construir aquí.");
            return;
        }

        // 2. Obtener costo
        int cost = GetBuildCost();

        // 3. Verificar recursos
        if (!Storage.RemoveIron(cost))
        {
            Debug.Log("No hay suficiente Iron.");
            return;
        }

        // 4. Construir
        GameObject building = Instantiate(
            selectedBuilding,
            ghost.transform.position,
            ghost.transform.rotation
        );

        Debug.Log(
            "Construido: " + building.name
        );

        // 5. Actualizar conexiones
        ConveyorMk1[] conveyors =
            FindObjectsByType<ConveyorMk1>();

        foreach (ConveyorMk1 conveyor in conveyors)
        {
            conveyor.RefreshConnections();
        }

        SmelterMk1[] smelters =
            FindObjectsByType<SmelterMk1>();

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

        if (selectedBuilding == constructorPrefab)
            return constructorCost;

        return 0;
    }
    
}