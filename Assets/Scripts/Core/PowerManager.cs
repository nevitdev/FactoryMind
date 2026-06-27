using UnityEngine;

public class PowerManager : MonoBehaviour
{
    public static PowerManager Instance;

    public bool hasPower = true;

    private void Awake()
    {
        Instance = this;
    }
}