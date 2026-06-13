using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public string resourceName = "Iron";
    public int amount = 10;

    public bool Harvest()
    {
        if (amount <= 0)
            return false;

        amount--;

        return true;
    }
}