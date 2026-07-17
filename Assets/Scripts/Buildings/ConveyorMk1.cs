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

    private GameObject carriedItem;

    public int powerConsumption = 2;

    public bool HasResource()
    {
        return carriedItem != null;
    }


    private void Start()
    {
        Debug.Log(name);

        Debug.Log("Start = " + startPoint);
        Debug.Log("End = " + endPoint);

        if (storage == null)
            storage = FindAnyObjectByType<StorageBox>();
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
        Debug.Log("Receive llamado");

        if (carriedItem != null)
        {
            Debug.Log("Conveyor ocupado");
            return false;
        }

        carriedItem = item;

        Debug.Log("Item recibido");

        carriedItem.transform.position = startPoint.position;

        carriedItem.transform.SetParent(transform);

        return true;
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

            Debug.Log(carriedItem.transform.position);

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
        StorageBox storage =
            FindAnyObjectByType<StorageBox>();

        if (storage == null)
            return;

        Item item =
            carriedItem.GetComponent<Item>();

        storage.AddResource(item.ResourceType);

        Destroy(carriedItem);

        carriedItem = null;
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
}