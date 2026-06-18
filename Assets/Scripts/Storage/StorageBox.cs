using UnityEngine;

public class StorageBox : MonoBehaviour
{
    public int iron;
    public int copper;
    public int coal;

    public void AddResource(string resourceName)
    {
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
}