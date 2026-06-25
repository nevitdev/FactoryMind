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
                GameStorage.Instance.storage;
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

            if (storage.iron > 0)
            {
                storage.RemoveIron(1);

                storage.AddIronIngot();

                Debug.Log(
                    "Smelter produjo 1 Iron Ingot"
                );
            }
            else if (storage.copper > 0)
            {
                storage.copper--;

                storage.copperIngot++;

                Debug.Log(
                    "Smelter produjo 1 Copper Ingot"
                );
            }
        }
    }
}