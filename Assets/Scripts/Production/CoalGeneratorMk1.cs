using UnityEngine;

public class CoalGeneratorMk1 : MonoBehaviour
{
    public float fuelInterval = 5f;

    private float timer;

    private void Update()
    {
        StorageBox storage = GameStorage.Instance.storage;

        if (storage == null)
            return;

        timer += Time.deltaTime;

        if (timer >= fuelInterval)
        {
            timer = 0f;

            if (storage.coal > 0)
            {
                storage.coal--;

                PowerManager.Instance.hasPower = true;

                Debug.Log("Generator activo");
            }
            else
            {
                PowerManager.Instance.hasPower = false;

                Debug.Log("Sin carbón");
            }
        }
    }
}