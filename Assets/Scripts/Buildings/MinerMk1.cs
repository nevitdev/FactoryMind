using UnityEngine;

public class MinerMk1 : MonoBehaviour
{
    public ResourceNode targetNode;
    public StorageBox storage;

    public float productionInterval = 3f;

    private float timer;

    private void Update()
    {
        if (storage == null)
        {
            Debug.LogError("Storage no asignado");
            return;
        }

        if (targetNode == null)
        {
            return;
        }

        timer += Time.deltaTime;

        if (timer >= productionInterval)
        {
            timer = 0f;

            if (targetNode.Harvest())
            {
                storage.AddResource(
                    targetNode.resourceName
                );

                Debug.Log(
                    "MinerMk1 produjo " +
                    targetNode.resourceName
                );
            }
            else
            {
                Debug.Log("Nodo agotado");
                targetNode = null;
            }
        }
    }
}