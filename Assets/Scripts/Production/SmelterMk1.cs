using UnityEngine;

public class SmelterMk1 : MonoBehaviour
{
    public StorageBox storage;

    public float productionInterval = 5f;

    private float timer;

    private void Start()
    {
        if (storage == null)
        {
            storage =
                FindFirstObjectByType<StorageBox>();
        }
    }

    private void Update()
    {
        if (storage == null)
            return;

        timer += Time.deltaTime;

        if (timer >= productionInterval)
        {
            timer = 0f;

            if (storage.RemoveIron(1))
            {
                storage.AddIronIngot();

                Debug.Log(
                    "Smelter produjo 1 Iron Ingot"
                );
            }
        }
    }
}