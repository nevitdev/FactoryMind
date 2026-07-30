using UnityEngine;

//Construye nuevos elementos


public class ConstructorMk1 : MonoBehaviour
{

    [Header("Production")]

    [SerializeField]
    private float productionInterval = 4f;

    [SerializeField]
    private ConstructorRecipe recipe;

    [Header("Power")]

    [SerializeField]
    private int powerConsumption = 2;

    [Header("Connections")]
    [SerializeField] private ConveyorMk1 outputConveyor;

    private GameObject currentItem;


    private float timer;

    public void RefreshConnections()
    {
        FindOutputConveyor();
    }
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
            BuildingManager.Instance.constructors.Add(this);
    }

    private void OnDisable()
    {
        if (BuildingManager.Instance != null)
            BuildingManager.Instance.constructors.Remove(this);
    }

    //Produce un lingote según la receta seleccionada.

    private void Produce()
    {
        if (!PowerManager.Instance.HasEnoughPower(powerConsumption))
            return;

        switch (recipe)
        {
            case ConstructorRecipe.IronPlate:
                ProduceIronPlate();
                break;

            case ConstructorRecipe.IronRod:
                ProduceIronRod();
                break;
        }
    }

    private void ProduceIronPlate()
    {
        PowerManager.Instance.ConsumePower(powerConsumption);

        Destroy(currentItem);

        currentItem =
            ItemFactory.Instance.Spawn(
                ResourceType.IronPlate,
                transform.position + Vector3.up
            );

        if (outputConveyor != null)
        {
            outputConveyor.Receive(currentItem);
            currentItem = null;
        }

        Debug.Log("Constructor produjo Iron Plate");
    }

    private void ProduceIronRod()
    {
        Debug.Log("IronRod aún no implementado.");
    }

    public bool Receive(GameObject item)
    {
        if (currentItem != null)
            return false;

        Item itemData = item.GetComponent<Item>();

        if (itemData.ResourceType != ResourceType.IronIngot)
            return false;

        currentItem = item;

        currentItem.transform.position =
            transform.position + Vector3.up * 0.5f;

        currentItem.transform.SetParent(transform);

        Debug.Log("Constructor recibió " + currentItem.name);

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