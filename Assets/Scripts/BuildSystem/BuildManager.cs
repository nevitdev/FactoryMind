using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public GameObject minerPrefab;

    private bool buildMode;

    private void Update()
    {
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
                minerPrefab,
                hit.point,
                Quaternion.identity
            );
        }
    }
}