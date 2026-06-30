using UnityEngine;

public class CoalGeneratorMk1 : MonoBehaviour
{
    public float fuelInterval = 5f;

    private float timer;

    private void Update()
    {
        StorageBox storage = GameStorage.Instance.storage;

        if (storage == null)
            return;

        timer += Time.deltaTime;

        if (timer >= fuelInterval)
        {
            timer = 0f;

            if (storage.coal > 0)
            {
                storage.coal--;

                PowerManager.Instance.totalPower = 100;

                Debug.Log("Generator activo");
            }
            else
            {
                PowerManager.Instance.totalPower = 0;

                Debug.Log("Sin carbón");
            }
        }
    }
    private void OnEnable()
    {
        if (BuildingManager.Instance != null)
            BuildingManager.Instance.generators.Add(this);
    }

    private void OnDisable()
    {
        if (BuildingManager.Instance != null)
            BuildingManager.Instance.generators.Remove(this);
    }
}