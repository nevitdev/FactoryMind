using TMPro;
using UnityEngine;

public class BuildUI : MonoBehaviour
{
    public TextMeshProUGUI buildText;

    public void UpdateSelectedBuilding(
    string buildingName,
    int cost)
    {
        buildText.text =
            "Selected:\n" +
            buildingName +
            "\n\nCost:\n" +
            cost + " Iron";
    }
}