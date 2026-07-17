using UnityEngine;

// Representa un recurso físico del mundo.
public class Item : MonoBehaviour
{
    [SerializeField]
    private ResourceType resourceType;

    public ResourceType ResourceType => resourceType;



    public float MoveSpeed = 2f;

    public bool IsMoving;
}