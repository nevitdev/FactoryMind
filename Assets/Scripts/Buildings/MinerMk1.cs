using UnityEngine;

public class MinerMk1 : MonoBehaviour
{
    public ResourceNode targetNode;
    public Inventory inventory;

    public float productionInterval = 3f;

    private float timer;

    private void Update()
    {
        if (targetNode == null)
        {
            Debug.Log("Nodo agotado");
            return;
        }

        timer += Time.deltaTime;

        if (timer >= productionInterval)
        {
            timer = 0f;

            if (targetNode.Harvest())
            {
                inventory.AddResource(
                    targetNode.resourceName
                );

                Debug.Log(
                    "MinerMk1 produjo " +
                    targetNode.resourceName
                );
            }
        }
    }
}