using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int iron;
    public int copper;
    public int coal;

    public void AddResource(ResourceType type)
    {
        switch (type)
        {
            case ResourceType.IronOre:
                iron++;
                break;

            case ResourceType.CopperOre:
                copper++;
                break;

            case ResourceType.Coal:
                coal++;
                break;
        }

        Debug.Log(
            $"Iron: {iron} | Copper: {copper} | Coal: {coal}"
        );
    }
}