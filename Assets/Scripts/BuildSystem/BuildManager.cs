using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public GameObject storagePrefab;
    public GameObject minerPrefab;
    public GameObject smelterPrefab;

    private GameObject selectedBuilding;
    private bool buildMode;

    public BuildUI buildUI;

    private void Start()
    {
        selectedBuilding = minerPrefab;

        buildUI.UpdateSelectedBuilding(
            "Miner Mk1"
        );
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedBuilding = storagePrefab;

            buildUI.UpdateSelectedBuilding(
                "Storage"
            );
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedBuilding = minerPrefab;

            buildUI.UpdateSelectedBuilding(
                "Miner Mk1"
            );
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedBuilding = smelterPrefab;

            buildUI.UpdateSelectedBuilding(
                "Smelter Mk1"
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
            Instantiate(
                selectedBuilding,
                hit.point,
                Quaternion.identity
            );
        }
    }
}