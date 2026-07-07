using TMPro;
using UnityEngine;

public class PowerUI : MonoBehaviour
{
    public TextMeshProUGUI powerText;

    private void Update()
    {
        powerText.text =
            "POWER\n\n" +
            "Production: " + PowerManager.Instance.TotalPower + " MW\n" +
            "Consumption: " + PowerManager.Instance.UsedPower + " MW\n" +
            "Available: " + PowerManager.Instance.AvailablePower + " MW";
    }
}