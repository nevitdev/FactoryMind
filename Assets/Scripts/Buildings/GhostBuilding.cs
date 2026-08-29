using UnityEngine;

public class GhostBuilding : MonoBehaviour
{
    private Camera cam;

    private Bounds buildingBounds;
    private Renderer[] ghostRenderers;

    [SerializeField]
    private LayerMask groundMask;

    [SerializeField]
    private Renderer ghostRenderer;

    [SerializeField]
    private Color validColor = Color.green;

    [SerializeField]
    private Color invalidColor = Color.red;


    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        FollowMouse();

        if (Input.GetKeyDown(KeyCode.R))
        {
            Rotate();
        }

        if (CanBuild())
        {
            SetGhostColor(validColor);
        }
        else
        {
            SetGhostColor(invalidColor);
        }
    }

    private void Rotate()
    {
        transform.Rotate(0f, 90f, 0f);
    }

    private void FollowMouse()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(
            ray,
            out RaycastHit hit,
            500f,
            groundMask))
        {
            Vector3 position = hit.point;

            position.x = Mathf.Round(position.x);
            position.z = Mathf.Round(position.z);

            transform.position = position;
        }
    }

    public bool CanBuild()
    {
        Vector3 center =
            transform.position +
            (buildingBounds.center - transform.position);

        Vector3 halfExtents = buildingBounds.extents;

        Collider[] hits = Physics.OverlapBox(
            center,
            halfExtents,
            transform.rotation
        );

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Building"))
            {
                return false;
            }
        }

        return true;
    }

    public void SetBuilding(GameObject prefab)
    {
        // Eliminar la representación anterior
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Crear representación visual del edificio seleccionado
        GameObject visual = Instantiate(
            prefab,
            transform.position,
            transform.rotation,
            transform
        );

        visual.name = "GhostVisual";

        ghostRenderers =
            visual.GetComponentsInChildren<Renderer>();

        // Desactivar scripts del edificio
        foreach (MonoBehaviour component in
                 visual.GetComponentsInChildren<MonoBehaviour>())
        {
            component.enabled = false;
        }

        // Desactivar Rigidbody
        foreach (Rigidbody rb in
                 visual.GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = true;
        }

        // Desactivar colliders del Ghost
        foreach (Collider collider in
                 visual.GetComponentsInChildren<Collider>())
        {
            collider.enabled = false;
        }

        // Obtener el tamaño real del edificio
        Collider[] colliders =
            prefab.GetComponentsInChildren<Collider>();

        if (colliders.Length > 0)
        {
            Bounds bounds = colliders[0].bounds;

            foreach (Collider collider in colliders)
            {
                bounds.Encapsulate(collider.bounds);
            }

            buildingBounds = bounds;
        }
    }

    private void SetGhostColor(Color color)
    {
        if (ghostRenderers == null)
            return;

        foreach (Renderer renderer in ghostRenderers)
        {
            renderer.material.color = color;
        }
    }
}