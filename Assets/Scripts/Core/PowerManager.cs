using UnityEngine;

public class PowerManager : MonoBehaviour
{
    public static PowerManager Instance;

    public int totalPower;
    public int usedPower;

    public bool hasPower;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        ResetConsumption();

        hasPower = totalPower > 0;
    }

    public void AddPower(int amount)
    {
        totalPower += amount;
    }

    public void ConsumePower(int amount)
    {
        usedPower += amount;
    }

    public void ResetConsumption()
    {
        usedPower = 0;
    }

    public int AvailablePower()
    {
        return totalPower - usedPower;
    }
}