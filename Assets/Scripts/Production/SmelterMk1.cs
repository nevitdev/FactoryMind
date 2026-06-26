using UnityEngine;

public class SmelterMk1 : MonoBehaviour
{
    public StorageBox storage;

    public float productionInterval = 5f;

    private float timer;

    public RecipeType recipe = RecipeType.Iron;

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

            if (recipe == RecipeType.Iron)
            {
                if (storage.iron > 0)
                {
                    storage.RemoveIron(1);

                    storage.AddIronIngot();

                    Debug.Log(
                        "Smelter produjo 1 Iron Ingot"
                    );
                }
            }

            if (recipe == RecipeType.Copper)
            {
                if (storage.copper > 0)
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
}