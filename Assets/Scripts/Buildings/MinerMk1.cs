using UnityEngine;
// Produce recursos desde un ResourceNode
public class MinerMk1 : MonoBehaviour
{
    public ResourceNode targetNode;

    public float productionInterval = 3f;

    private float timer;

    public ConveyorMk1 outputConveyor;

    public int powerConsumption = 2;

    private StorageBox Storage
    {
        get
        {
            return GameStorage.Instance.Storage;
        }
    }

    private void Start()
    {
        

        if (targetNode == null)
            targetNode = FindClosestNode();

        if (targetNode != null)
        {
            Debug.Log(
                gameObject.name +
                " encontró nodo: " +
                targetNode.resourceType
            );
        }
    }

    private ResourceNode FindClosestNode()
    {
        ResourceNode[] nodes =
        FindObjectsByType<ResourceNode>();

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
        if (!PowerManager.Instance.HasPower)
            return;

        if (!PowerManager.Instance.HasEnoughPower(powerConsumption))
            return;

        if (Storage == null)
            return;

        if (targetNode == null)
            return;

        timer += Time.deltaTime;

        if (timer < productionInterval)
            return;

        timer = 0f;

        PowerManager.Instance.ConsumePower(powerConsumption);

        if (!Storage.HasSpace(targetNode.resourceType))
        {
            Debug.Log("Storage lleno");
            return;
        }

        if (targetNode.Harvest())
        {
            GameObject item =
                ItemFactory.Instance.Spawn(
                    targetNode.resourceType,
                    transform.position + Vector3.up
                );

            if (item == null)
            {
                Debug.LogError("ItemFactory devolvió NULL");
                return;
            }

            Debug.Log("Item creado: " + item.name);

            if (outputConveyor != null)
            {
                outputConveyor.Receive(item);
            }

            Debug.Log("Miner produjo " + targetNode.resourceType);
        }
        else
        {
            Debug.Log("Nodo agotado");
            targetNode = null;
        }
    }

    private void OnEnable()
    {
        if (BuildingManager.Instance != null)
            BuildingManager.Instance.miners.Add(this);
    }

    private void OnDisable()
    {
        if (BuildingManager.Instance != null)
            BuildingManager.Instance.miners.Remove(this);
    }
}