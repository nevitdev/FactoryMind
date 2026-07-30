using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance;

    public List<MinerMk1> miners = new List<MinerMk1>();
    public List<SmelterMk1> smelters = new List<SmelterMk1>();
    public List<CoalGeneratorMk1> generators = new List<CoalGeneratorMk1>();
    public List<ConveyorMk1> conveyors = new List<ConveyorMk1>();
    public List<ConstructorMk1> constructors = new List<ConstructorMk1>();


    private void Awake()
    {
        Instance = this;
    }

    public int GetTotalBuildings()
    {
        return miners.Count +
               smelters.Count +
               generators.Count +
               conveyors.Count;
    }
    
}