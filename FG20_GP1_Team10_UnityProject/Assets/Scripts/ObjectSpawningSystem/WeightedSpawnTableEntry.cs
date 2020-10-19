using UnityEngine;

[System.Serializable]
public class WeightedSpawnTableEntry
{
    public GameObject prefabToSpawn;
    [Min(1)] public int spawnWeight = 1;
}
