using UnityEngine;
// Produce recursos desde un ResourceNode
public class MinerMk1 : MonoBehaviour
{
    public ResourceNode targetNode;
    public StorageBox storage;

    public float productionInterval = 3f;

    private float timer;

    private void Start()
    {
        if (storage == null)
            storage = GameStorage.Instance.storage;

        if (targetNode == null)
            targetNode = FindClosestNode();

        if (targetNode != null)
        {
            Debug.Log(
                gameObject.name +
                " encontró nodo: " +
                targetNode.resourceName
            );
        }
    }


    private ResourceNode FindClosestNode()
    {
        ResourceNode[] nodes =
            FindObjectsByType<ResourceNode>(
                FindObjectsSortMode.None
            );

        ResourceNode closestNode = null;
        float closestDistance = Mathf.Infinity;

        foreach (ResourceNode node in nodes)
        {
            float distance =
                Vector3.Distance(
                    transform.position,
                    node.transform.position
                );

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestNode = node;
            }
        }

        return closestNode;
    }

    private void Update()
    {
        if (!PowerManager.Instance.hasPower)
            return;

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

        if (!storage.HasSpace(targetNode.resourceName))
        {
            return;
        }

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