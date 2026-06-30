using UnityEngine;

//script de prueba NO ES REALISTA

public class ConveyorMk1 : MonoBehaviour
{
    public StorageBox storage;

    public float transferInterval = 2f;

    private float timer;


    public string carriedResource = "";

    public int powerConsumption = 2;

    public bool HasResource()
    {
        return carriedResource != "";
    }

    private void Start()
    {
        if (storage == null)
        {
            storage = FindFirstObjectByType<StorageBox>();
        }
    }

    public void Deliver(StorageBox storage)
    {
        if (!HasResource())
            return;

        storage.AddResource(carriedResource);

        carriedResource = "";
    }

    public bool Receive(string resource)
    {
        if (HasResource())
            return false;

        carriedResource = resource;

        return true;
    }


    private void Update()
    {

        if (!PowerManager.Instance.HasEnoughPower(powerConsumption))
            return;

        if (!PowerManager.Instance.hasPower)
            return;

        if (PowerManager.Instance.AvailablePower() < powerConsumption)
        {
            return;
        }

        PowerManager.Instance.ConsumePower(powerConsumption);

        if (storage == null)
            return;

        timer += Time.deltaTime;

        if (timer >= transferInterval)
        {
            timer = 0f;

            storage.AddResource("Iron");

            Debug.Log(
                "Conveyor transfirió 1 Iron"
            );
        }
        Debug.Log(
    "Conveyor transportando: " +
    carriedResource
);
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