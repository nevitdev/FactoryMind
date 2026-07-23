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

    [Header("Connections")]
    [SerializeField] private ConveyorMk1 outputConveyor;

    private GameObject currentItem;


    private float timer;


    private void Start()
    {
        FindOutputConveyor();
    }

    private void Update()
    {

        if (!PowerManager.Instance.HasPower)
            return;

        if (currentItem == null)
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
        PowerManager.Instance.ConsumePower(powerConsumption);

        Destroy(currentItem);

        currentItem =
            ItemFactory.Instance.Spawn(
                ResourceType.IronIngot,
                transform.position + Vector3.up
            );

        if (outputConveyor != null)
        {
            Debug.Log(outputConveyor);
            outputConveyor.Receive(currentItem);
            currentItem = null;
        }

        Debug.Log("Smelter produjo Iron Ingot");
    }

    private void ProduceCopper()
    {
        Debug.Log("Copper aún no implementado.");
    }

    public bool Receive(GameObject item)
    {
        if (currentItem != null)
            return false;

        currentItem = item;

        currentItem.transform.position = transform.position + Vector3.up * 0.5f;
        currentItem.transform.SetParent(transform);

        Debug.Log("Smelter recibió " + currentItem.name);

        return true;
    }

    private void FindOutputConveyor()
    {
        Collider[] hits =
            Physics.OverlapSphere(
                transform.position,
                2f);

        foreach (Collider hit in hits)
        {
            ConveyorMk1 conveyor =
                hit.GetComponent<ConveyorMk1>();

            if (conveyor != null)
            {
                outputConveyor = conveyor;

                Debug.Log(
                    "Smelter conectado a " +
                    conveyor.name);

                return;
            }
        }
    }

}