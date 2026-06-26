using UnityEngine;
// Convierte minerales en lingotes
public class SmelterMk1 : MonoBehaviour
{
    private StorageBox Storage
    {
        get
        {
            return GameStorage.Instance.storage;
        }
    }
    public float productionInterval = 5f;

    private float timer;

    public RecipeType recipe = RecipeType.Iron;

    private void Start()
    {
        if (Storage == null)
        {
            Storage =
                GameStorage.Instance.storage;
        }
    }

    private void Update()
    {
        if (Storage == null)
            return;

        timer += Time.deltaTime;

        if (timer >= productionInterval)
        {
            timer = 0f;

            if (recipe == RecipeType.Iron)
            {
                if (Storage.iron > 0)
                {
                    Storage.RemoveIron(1);

                    Storage.AddIronIngot();

                    Debug.Log(
                        "Smelter produjo 1 Iron Ingot"
                    );
                }
            }

            if (recipe == RecipeType.Copper)
            {
                if (Storage.copper > 0)
                {
                    Storage.copper--;

                    Storage.copperIngot++;

                    Debug.Log(
                        "Smelter produjo 1 Copper Ingot"
                    );
                }
            }
        }
    }
}