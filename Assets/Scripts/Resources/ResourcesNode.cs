using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public ResourceType resourceType;
    public int amount = 10;

    public bool Harvest()
    {
        if (amount <= 0)
            return false;

        amount--;

        Debug.Log(resourceType + " restante: " + amount);

        if (amount <= 0)
        {
            Debug.Log(resourceType + " destruido");
            Destroy(gameObject);
        }

        return true;
    }
}