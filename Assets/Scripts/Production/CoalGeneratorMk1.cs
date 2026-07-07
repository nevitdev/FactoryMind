using UnityEngine;

//Genera energía consumiendo carbón.

public class CoalGeneratorMk1 : MonoBehaviour
{
    [SerializeField]
    private float fuelInterval = 5f;

    [SerializeField]
    private int generatedPower = 10;

    private float timer;

    private StorageBox Storage => GameStorage.Instance.Storage;

    private void Update()
    {
        if (Storage == null)
            return;

        timer += Time.deltaTime;

        if (timer < fuelInterval)
            return;

        timer = 0f;

        if (Storage.RemoveCoal(1))
        {
            PowerManager.Instance.SetTotalPower(generatedPower);

            Debug.Log("Generator activo");
        }
        else
        {
            PowerManager.Instance.SetTotalPower(0);

            Debug.Log("Sin carbón");
        }
    }

    private void OnEnable()
    {
        if (BuildingManager.Instance != null)
            BuildingManager.Instance.generators.Add(this);
    }

    private void OnDisable()
    {
        if (BuildingManager.Instance != null)
            BuildingManager.Instance.generators.Remove(this);
    }

}