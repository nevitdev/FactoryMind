using UnityEngine;

public class StorageBox : MonoBehaviour
{
    public int iron;
    public int copper;
    public int coal;

    public int ironIngot;

    public int maxCapacity = 10;

    public bool HasSpace(string resourceName)
    {
        switch (resourceName)
        {
            case "Iron":
                return iron < maxCapacity;

            case "Copper":
                return copper < maxCapacity;

            case "Coal":
                return coal < maxCapacity;

            default:
                return false;
        }
    }

    public void AddResource(string resourceName)
    {
        if (!HasSpace(resourceName))
        {
            Debug.Log(resourceName + " lleno");
            return;
        }

        switch (resourceName)
        {
            case "Iron":
                iron++;
                break;

            case "Copper":
                copper++;
                break;

            case "Coal":
                coal++;
                break;
        }

        Debug.Log(
            $"Storage -> Iron:{iron} Copper:{copper} Coal:{coal}"
        );
    }

    public bool RemoveIron(int amount)
    {
        if (iron < amount)
            return false;

        iron -= amount;
        return true;
    }

    public void AddIronIngot()
    {
        ironIngot++;

        Debug.Log(
            "Iron Ingots: " +
            ironIngot
        );
    }
}