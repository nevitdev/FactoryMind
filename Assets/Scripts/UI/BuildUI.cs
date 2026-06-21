using TMPro;
using UnityEngine;

public class BuildUI : MonoBehaviour
{
    public TextMeshProUGUI buildText;

    public void UpdateSelectedBuilding(
        string buildingName)
    {
        buildText.text =
            "BUILD MODE\n\n" +
            "Selected:\n" +
            buildingName;
    }
}