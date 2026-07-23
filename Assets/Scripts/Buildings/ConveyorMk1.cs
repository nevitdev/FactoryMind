using UnityEngine;

//script de prueba NO ES REALISTA

public class ConveyorMk1 : MonoBehaviour
{
    public StorageBox storage;

    public float transferInterval = 2f;

    private float timer;
    [Header("Transport")]

    [SerializeField]
    private Transform startPoint;

    [SerializeField]
    private Transform endPoint;

    [SerializeField]
    private float speed = 2f;

    [Header("Connections")]
    [SerializeField] private ConveyorMk1 nextConveyor;

    [SerializeField] private StorageBox targetStorage;

    private GameObject carriedItem;

    public int powerConsumption = 2;

    public bool HasResource()
    {
        return carriedItem != null;
    }

    public void RefreshConnections()
    {
        nextConveyor = null;
        targetStorage = null;

        FindConnections();
    }

    private void Start()
    {
        RefreshConnections();

        if (storage == null)
        {
            storage = FindAnyObjectByType<StorageBox>();
        }        
    }

    public void Deliver(StorageBox storage)
    {
        if (!HasResource())
            return;

        Item item = carriedItem.GetComponent<Item>();

        switch (item.ResourceType)
        {
            case ResourceType.IronOre:
                storage.AddResource(ResourceType.IronOre);
                break;

            case ResourceType.CopperOre:
                storage.AddResource(ResourceType.CopperOre);
                break;

            case ResourceType.Coal:
                storage.AddResource(ResourceType.Coal);
                break;
        }

        Destroy(carriedItem);

        carriedItem = null;
    }

    public bool Receive(GameObject item)
    {
        Debug.Log(name + " recibió un item");

        if (carriedItem != null)
            return false;

        carriedItem = item;

        carriedItem.transform.SetParent(transform);

        carriedItem.transform.position = startPoint.position;

        return true;
        Debug.Log(carriedItem.transform.position);
        carriedItem.transform.position = startPoint.position;
    }


    private void Update()
    {

        if (!PowerManager.Instance.HasEnoughPower(powerConsumption))
            return;

        if (!PowerManager.Instance.HasPower)
            return;

        if (PowerManager.Instance.AvailablePower < powerConsumption)
        {
            return;
        }

        PowerManager.Instance.ConsumePower(powerConsumption);

        if (storage == null)
            return;

        timer += Time.deltaTime;


        if (carriedItem != null)
        {
            Debug.Log("Moviendo item");

            carriedItem.transform.position =
                Vector3.MoveTowards(
                    carriedItem.transform.position,
                    endPoint.position,
                    speed * Time.deltaTime
                );

            //Debug.Log(carriedItem.transform.position);

            if (Vector3.Distance(
                carriedItem.transform.position,
                endPoint.position) < 0.05f)
            {
                Debug.Log("Llegó al final");

                DeliverItem();
            }
        }
    }

    private void DeliverItem()
    {
        if (nextConveyor != null)
        {
            if (nextConveyor.Receive(carriedItem))
            {
                carriedItem = null;
            }

            return;
        }

        if (targetStorage != null)
        {
            Item item = carriedItem.GetComponent<Item>();

            targetStorage.AddResource(item.ResourceType);

            Destroy(carriedItem);

            carriedItem = null;
        }
        Debug.Log(nextConveyor);
    }

    private void OnEnable()
    {
        if (BuildingManager.Instance != null)
            BuildingManager.Instance.conveyors.Add(this);
    }

    private void OnDisable()
    {
        if (BuildingManager.Instance != null)
            BuildingManager.Instance.conveyors.Remove(this);
    }


    private void FindConnections()
    {
        nextConveyor = null;
        targetStorage = null;

        Collider[] hits = Physics.OverlapSphere(endPoint.position, 1f);

        foreach (Collider hit in hits)
        {
            ConveyorMk1 conveyor = hit.GetComponent<ConveyorMk1>();

            if (conveyor != null && conveyor != this)
            {
                nextConveyor = conveyor;

                Debug.Log(name + " conectado a " + conveyor.name);

                return;
            }

            StorageBox storage = hit.GetComponent<StorageBox>();

            if (storage != null)
            {
                targetStorage = storage;

                Debug.Log(name + " conectado al Storage");

                return;
            }
        }

        Debug.Log(name + " no encontró conexiones.");

        Debug.DrawLine(
    endPoint.position,
    endPoint.position + Vector3.up,
    Color.green,
    10f
);
    }
}