using UnityEngine;
// Gestiona la construcción de edificios

public class BuildManager : MonoBehaviour
{

    [Header("Prefabs")]
    public GameObject storagePrefab;
    public GameObject minerPrefab;
    public GameObject smelterPrefab;
    public GameObject conveyorPrefab;

    [Header("UI")]
    public BuildUI buildUI;

    [Header("Building Costs")]
    public int storageCost = 3;
    public int minerCost = 5;
    public int smelterCost = 10;
    public int conveyorCost = 2;

    

    private GameObject selectedBuilding;
    private bool buildMode;
    public StorageBox storage;
    private StorageBox Storage => GameStorage.Instance.storage;

  


    private void Start()
    {
        selectedBuilding = minerPrefab;

        buildUI.UpdateSelectedBuilding(
            "Miner Mk1",
            minerCost
        );

        if (storage == null)
        {
            storage =
                GameStorage.Instance.storage;
        }
        Debug.Log(
    "Storage usado: " +
    storage.name
);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedBuilding = storagePrefab;

            buildUI.UpdateSelectedBuilding(
                "Storage",
                storageCost
            );
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedBuilding = minerPrefab;

            buildUI.UpdateSelectedBuilding(
                "Miner Mk1",
                minerCost
            );
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedBuilding = smelterPrefab;

            buildUI.UpdateSelectedBuilding(
                "Smelter Mk1",
                smelterCost
            );
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedBuilding = conveyorPrefab;

            buildUI.UpdateSelectedBuilding(
                "Conveyor Mk1",
                conveyorCost

            );
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            buildMode = !buildMode;

            Debug.Log(
                "Build Mode: " +
                buildMode
            );
        }

        if (buildMode &&
            Input.GetMouseButtonDown(0))
        {
            PlaceBuilding();
        }
    }

    private void PlaceBuilding()
    {
        Ray ray =
            Camera.main.ScreenPointToRay(
                Input.mousePosition
            );

        if (Physics.Raycast(
            ray,
            out RaycastHit hit))
        {
            int cost = GetBuildCost();
            Debug.Log(
    "Iron actual: " +
    storage.iron
);

            Debug.Log(
                "Costo edificio: " +
                cost
            );

            Debug.Log(
                "Edificio seleccionado: " +
                selectedBuilding.name
            );

            if (!storage.RemoveIron(cost))
            {
                Debug.Log(
                    "No hay suficiente Iron"
                );

                return;
            }

            Instantiate(
                selectedBuilding,
                hit.point,
                Quaternion.identity
            );
        }
    }
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

        return 0;
    }
}