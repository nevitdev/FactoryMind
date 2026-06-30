using UnityEngine;

// Gestiona el Storage principal del juego mediante un Singleton.
// Permite que cualquier sistema (Miner, Smelter, BuildManager,
// Conveyor, etc.) acceda al mismo StorageBox.

public class GameStorage : MonoBehaviour
{
    //Instancia global de GameStorage.
    public static GameStorage Instance { get; private set; }

    [Header("References")]

    [SerializeField]
    private StorageBox storage;

    // Devuelve el Storage principal del juego.
    public StorageBox Storage => storage;

    private void Awake()
    {
        // Evita múltiples instancias del Singleton.
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning(
                "Se encontró un GameStorage duplicado. Será destruido."
            );

            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Descomentar cuando el juego tenga varias escenas.
        // DontDestroyOnLoad(gameObject);

        if (storage == null)
        {
            Debug.LogError(
                "GameStorage no tiene un StorageBox asignado en el Inspector."
            );
        }
    }

    
}