using UnityEngine;

//script de prueba NO ES REALISTA

public class ConveyorMk1 : MonoBehaviour
{
    public StorageBox storage;

    public float transferInterval = 2f;

    private float timer;

    public int powerUsage = 5;

    private void Start()
    {
        if (storage == null)
        {
            storage = FindFirstObjectByType<StorageBox>();
        }
    }

    private void Update()
    {
        if (!PowerManager.Instance.hasPower)
            return;

        if (PowerManager.Instance.AvailablePower() < powerUsage)
        {
            return;
        }

        PowerManager.Instance.ConsumePower(powerUsage);

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
    }
}