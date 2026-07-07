using UnityEngine;

// Gestiona toda la energía del juego.

public class PowerManager : MonoBehaviour
{
    // Instancia global del PowerManager.
    public static PowerManager Instance { get; private set; }
    [Header("Power")]

    [SerializeField]
    private int totalPower;

    [SerializeField]
    private int usedPower;

    // Energía total producida.
    public int TotalPower => totalPower;

    // Energía consumida durante el ciclo actual.
    public int UsedPower => usedPower;

    // Energía actualmente disponible.
    public int AvailablePower => totalPower - usedPower;

    // Indica si existe al menos una fuente de energía.
    public bool HasPower => totalPower > 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning(
                "Se encontró un PowerManager duplicado. Será destruido."
            );

            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        // Temporal.
        // Más adelante este método será controlado por BuildingManager.
        ResetConsumption();
    }

    //Agrega energía al sistema.
    public void AddPower(int amount)
    {
        if (amount <= 0)
            return;

        totalPower += amount;
    }

    // Consume energía.    
    public void ConsumePower(int amount)
    {
        if (amount <= 0)
            return;

        usedPower += amount;
    }

    // Reinicia el consumo de energía del ciclo actual.
   
    public void ResetConsumption()
    {
        usedPower = 0;
    }

    // Comprueba si existe energía suficiente.
    
    public bool HasEnoughPower(int amount)
    {
        return AvailablePower >= amount;
    }
    // Establece la energía producida por el sistema.
    public void SetTotalPower(int amount)
    {
        totalPower = Mathf.Max(0, amount);
    }

}