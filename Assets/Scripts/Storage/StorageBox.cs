using UnityEngine;

// Almacena todos los recursos del juego.

public class StorageBox : MonoBehaviour
{

    [Header("Raw Resources")]

    [SerializeField] private int iron;
    [SerializeField] private int copper;
    [SerializeField] private int coal;

    [Header("Processed Resources")]

    [SerializeField] private int ironIngot;
    [SerializeField] private int copperIngot;

    [Header("Storage")]

    [SerializeField] private int maxCapacity = 10;


    public int Iron => iron;

    public int Copper => copper;

    public int Coal => coal;

    public int IronIngot => ironIngot;

    public int CopperIngot => copperIngot;

    public int MaxCapacity => maxCapacity;

    //Comprueba si existe espacio para un recurso.

    public bool HasSpace(ResourceType type)
    {
        switch (type)
        {
            case ResourceType.IronOre:
                return iron < maxCapacity;

            case ResourceType.CopperOre:
                return copper < maxCapacity;

            case ResourceType.Coal:
                return coal < maxCapacity;

            case ResourceType.IronIngot:
                return ironIngot < maxCapacity;

            case ResourceType.CopperIngot:
                return copperIngot < maxCapacity;

            default:
                return false;
        }
    }

    //Agrega un recurso al Storage.

    public void AddResource(ResourceType type)
    {
        switch (type)
        {
            case ResourceType.IronOre:
                iron++;
                break;

            case ResourceType.CopperOre:
                copper++;
                break;

            case ResourceType.Coal:
                coal++;
                break;

            case ResourceType.IronIngot:
                ironIngot++;
                break;

            case ResourceType.CopperIngot:
                copperIngot++;
                break;
        }

        PrintInventory();
    }

    //Agrega un Iron Ingot.

    public void AddIronIngot()
    {
        ironIngot++;

        Debug.Log(
            $"Iron Ingots: {ironIngot}"
        );
    }

    //Comprueba si existe suficiente Iron.
  
    public bool HasIron(int amount)
    {
        return iron >= amount;
    }

    //Consume Iron del Storage.

    public bool RemoveIron(int amount)
    {
        if (amount <= 0)
            return false;

        Debug.Log($"Iron disponible: {iron}");
        Debug.Log($"Costo: {amount}");

        if (iron < amount)
            return false;

        iron -= amount;

        Debug.Log($"Iron restante: {iron}");

        return true;
    }

    //Consume Copper del Storage.

    public bool RemoveCopper(int amount)
    {
        if (amount <= 0)
            return false;

        if (copper < amount)
            return false;

        copper -= amount;

        Debug.Log($"Copper restante: {copper}");

        return true;
    }

    //Agrega un Copper Ingot al Storage.

    public void AddCopperIngot()
    {
        copperIngot++;

        Debug.Log(
            $"Copper Ingots: {copperIngot}"
        );
    }

    //Imprime el contenido actual del Storage.

    private void PrintInventory()
    {
        Debug.Log(
            $"Storage | " +
            $"Iron:{iron} " +
            $"Copper:{copper} " +
            $"Coal:{coal} " +
            $"Iron Ingot:{ironIngot} " +
            $"Copper Ingot:{copperIngot}"
        );
    }
    //Consume el carbon
    public bool RemoveCoal(int amount)
    {
        if (amount <= 0)
            return false;

        if (coal < amount)
            return false;

        coal -= amount;

        Debug.Log($"Coal restante: {coal}");

        return true;
    }



    //Restaura el contenido del Storage desde una partida guardada.

    public void LoadData(
        int iron,
        int copper,
        int coal,
        int ironIngot,
        int copperIngot)
    {
        this.iron = iron;
        this.copper = copper;
        this.coal = coal;

        this.ironIngot = ironIngot;
        this.copperIngot = copperIngot;

        PrintInventory();
    }

}