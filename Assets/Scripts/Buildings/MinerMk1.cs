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
        if (!PowerManager.Instance.HasEnoughPower(powerConsumption))
        {
            Debug.Log("Sin energía");
            return;
        }

        if (!PowerManager.Instance.hasPower)
            return;

        if (PowerManager.Instance.AvailablePower < powerConsumption)
        {
            return;
        }


        if (Storage == null)
        {
            Debug.LogError("Storage no asignado");
            return;
        }

        if (targetNode == null)
        {
            return;
        }

        timer += Time.deltaTime;

        if (!Storage.HasSpace(targetNode.resourceName))
        {
            return;
        }

        if (timer >= productionInterval)
        {
            timer = 0f;

            PowerManager.Instance.ConsumePower(powerConsumption);


            if (targetNode.Harvest())
            {
                if (outputConveyor != null)
                {
                    outputConveyor.Receive(
                        targetNode.resourceName
                    );
                }

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