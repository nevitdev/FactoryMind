using UnityEngine;

public class RobotController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float interactDistance = 1.5f;

    private Vector3 targetPosition;
    private bool isMoving = false;
    private float lastInteractTime;

    private void Start()
    {
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.W))
                Move(Vector3.forward);

            if (Input.GetKeyDown(KeyCode.S))
                Move(Vector3.back);

            if (Input.GetKeyDown(KeyCode.A))
                Move(Vector3.left);

            if (Input.GetKeyDown(KeyCode.D))
                Move(Vector3.right);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }
        }
    }

    private void Move(Vector3 direction)
    {
        targetPosition += direction;

        transform.rotation =
            Quaternion.LookRotation(direction) *
            Quaternion.Euler(0, 180, 0);

        isMoving = true;
    }

    private void Interact()
    {
        if (Time.time - lastInteractTime < 0.5f)
            return;

        lastInteractTime = Time.time;
        Collider[] nearbyObjects =
            Physics.OverlapSphere(transform.position, interactDistance);

        Inventory inventory = GetComponent<Inventory>();

        foreach (Collider obj in nearbyObjects)
        {
            ResourceNode resource =
                obj.GetComponent<ResourceNode>();

            if (resource != null)
            {
                if (resource.Harvest())
                {
                    inventory.AddResource(resource.resourceName);

                    Debug.Log(
                        "Recolectaste " +
                        resource.resourceName
                    );
                }

                return;
            }
        }

        Debug.Log("No hay recursos cerca");
    }
}