using UnityEngine;

public class GhostBuilding : MonoBehaviour
{
    private Camera cam;

    [Header("Ground")]
    [SerializeField]
    private LayerMask groundMask;

    [Header("Ghost")]
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
            ghostRenderer.material.color = validColor;
        }
        else
        {
            ghostRenderer.material.color = invalidColor;
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
        Collider[] colliders =
            GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
        {
            Collider[] hits = Physics.OverlapBox(
                collider.bounds.center,
                collider.bounds.extents,
                transform.rotation
            );

            foreach (Collider hit in hits)
            {
                if (hit.transform.IsChildOf(transform))
                    continue;

                if (hit.CompareTag("Building"))
                    return false;
            }
        }

        return true;
    }
    public void SetBuilding(GameObject prefab)
    {
        // Eliminar la apariencia anterior
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Crear la apariencia del nuevo edificio
        GameObject visual = Instantiate(
            prefab,
            transform.position,
            transform.rotation,
            transform
        );

        // Desactivar scripts del edificio real
        MonoBehaviour[] scripts =
            visual.GetComponentsInChildren<MonoBehaviour>();

        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = false;
        }

        // Buscar Renderer
        ghostRenderer =
            visual.GetComponentInChildren<Renderer>();

        if (ghostRenderer == null)
        {
            Debug.LogWarning(
                "El prefab no tiene Renderer: " +
                prefab.name);
        }
    }
}