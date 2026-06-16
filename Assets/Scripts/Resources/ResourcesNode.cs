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

        Debug.Log(
            resourceName +
            " restante: " +
            amount
        );

        if (amount <= 0)
        {
            Debug.Log(resourceName + " destruido");
            Destroy(gameObject);
        }

        return true;
    }
}