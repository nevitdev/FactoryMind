using TMPro;
using UnityEngine;

public class SelectionUI : MonoBehaviour
{
    public GameObject panel;

    public TextMeshProUGUI infoText;

    public void Show(BuildingInfo info)
    {
        panel.SetActive(true);

        infoText.text =
            info.buildingName +
            "\n\n" +
            info.description;
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
}