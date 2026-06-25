using UnityEngine;

public class GameStorage : MonoBehaviour
{
    public static GameStorage Instance;

    public StorageBox storage;

    private void Awake()
    {
        Instance = this;
    }
}