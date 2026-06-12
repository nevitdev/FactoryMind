using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width = 20;
    public int height = 20;
    public float cellSize = 1f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        float yOffset = 0.05f;

        for (int x = 0; x <= width; x++)
        {
            Gizmos.DrawLine(
                new Vector3(x * cellSize, yOffset, 0),
                new Vector3(x * cellSize, yOffset, height * cellSize)
            );
        }

        for (int z = 0; z <= height; z++)
        {
            Gizmos.DrawLine(
                new Vector3(0, yOffset, z * cellSize),
                new Vector3(width * cellSize, yOffset, z * cellSize)
            );
        }
    }
}