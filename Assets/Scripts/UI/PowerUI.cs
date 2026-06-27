using TMPro;
using UnityEngine;

public class PowerUI : MonoBehaviour
{
    public TextMeshProUGUI powerText;

    private void Update()
    {
        powerText.text =
            "POWER\n\n" +
            "Production: " + PowerManager.Instance.totalPower + " MW\n" +
            "Consumption: " + PowerManager.Instance.usedPower + " MW\n" +
            "Available: " + PowerManager.Instance.AvailablePower() + " MW";
    }
}