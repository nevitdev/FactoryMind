using UnityEngine;

public class GhostBuilding : MonoBehaviour
{
    private Camera cam;

    [SerializeField]
    private LayerMask groundMask;
    [SerializeField] private Renderer ghostRenderer;
    [SerializeField] private Color validColor = Color.green;
    [SerializeField] private Color invalidColor = Color.red;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        FollowMouse();

        if (CanBuild())
        {
            ghostRenderer.material.color = validColor;
        }
        else
        {
            ghostRenderer.material.color = invalidColor;
        }
    }

    private void FollowMouse()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 500f, groundMask))
        {
            Vector3 position = hit.point;

            position.x = Mathf.Round(position.x);
            position.z = Mathf.Round(position.z);

            transform.position = position;
        }
    }

    public bool CanBuild()
    {
        Collider[] hits = Physics.OverlapBox(
            transform.position,
            Vector3.one * 0.45f);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Building"))
                return false;
        }

        return true;
    }
}