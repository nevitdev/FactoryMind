using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public SelectionUI selectionUI;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectBuilding();
        }
    }

    private void SelectBuilding()
    {
        Ray ray =
            Camera.main.ScreenPointToRay(
                Input.mousePosition
            );

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            BuildingInfo info =
                hit.collider.GetComponent<BuildingInfo>();

            if (info != null)
            {
                selectionUI.Show(info);
            }
            else
            {
                selectionUI.Hide();
            }
        }
        else
        {
            selectionUI.Hide();
        }
    }
}