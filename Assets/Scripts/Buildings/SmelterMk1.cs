using UnityEngine;

//Convierte minerales almacenados en lingotes.


public class SmelterMk1 : MonoBehaviour
{

    [Header("Production")]

    [SerializeField]
    private float productionInterval = 5f;

    [SerializeField]
    private RecipeType recipe = RecipeType.Iron;

    [Header("Power")]

    [SerializeField]
    private int powerConsumption = 2;

 

    //Acceso al Storage principal.

    private StorageBox Storage => GameStorage.Instance.Storage;

    

    private float timer;

    

    private void Update()
    {
        if (Storage == null)
            return;

        if (!PowerManager.Instance.HasPower)
            return;

        timer += Time.deltaTime;

        if (timer < productionInterval)
            return;

        timer = 0f;

        Produce();
    }

    private void OnEnable()
    {
        if (BuildingManager.Instance != null)
            BuildingManager.Instance.smelters.Add(this);
    }

    private void OnDisable()
    {
        if (BuildingManager.Instance != null)
            BuildingManager.Instance.smelters.Remove(this);
    }

    //Produce un lingote según la receta seleccionada.

    private void Produce()
    {
        if (!PowerManager.Instance.HasEnoughPower(powerConsumption))
            return;

        switch (recipe)
        {
            case RecipeType.Iron:
                ProduceIron();
                break;

            case RecipeType.Copper:
                ProduceCopper();
                break;
        }
    }

    private void ProduceIron()
    {
        if (!Storage.RemoveIron(1))
            return;

        PowerManager.Instance.ConsumePower(powerConsumption);

        Storage.AddIronIngot();

        Debug.Log("Smelter produjo 1 Iron Ingot");
    }

    private void ProduceCopper()
    {
        if (Storage.Copper <= 0)
            return;

        PowerManager.Instance.ConsumePower(powerConsumption);

        Storage.RemoveCopper(1);
        Storage.AddCopperIngot();

        Debug.Log("Smelter produjo 1 Copper Ingot");
    }

}