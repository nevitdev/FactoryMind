using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    public static ItemFactory Instance;

    [Header("Prefabs")]
    [SerializeField] private GameObject ironOrePrefab;
    [SerializeField] private GameObject copperOrePrefab;
    [SerializeField] private GameObject coalPrefab;
    [SerializeField] private GameObject ironIngotPrefab;
    [SerializeField] private GameObject copperIngotPrefab;
    [SerializeField] private GameObject ironPlatePrefab;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject Spawn(ResourceType type, Vector3 position)
    {
        GameObject prefab = GetPrefab(type);

        if (prefab == null)
        {
            Debug.LogError("Prefab NULL para: " + type);
            return null;
        }

        Debug.Log("Instanciando: " + prefab.name);

        GameObject item =
            Instantiate(prefab, position, Quaternion.identity);

        Debug.Log("Creado: " + item.name);

        return item;
    }

    private GameObject GetPrefab(ResourceType type)
    {
        switch (type)
        {
            case ResourceType.IronOre:
                return ironOrePrefab;

            case ResourceType.CopperOre:
                return copperOrePrefab;

            case ResourceType.Coal:
                return coalPrefab;

            case ResourceType.IronIngot:
                return ironIngotPrefab;

            case ResourceType.CopperIngot:
                return copperIngotPrefab;

            case ResourceType.IronPlate:
                return ironPlatePrefab;
        }

        return null;
    }
}