using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnRandomObject : MonoBehaviour
{
    #region SpawnedObject component
    
    // This component is automatically added to gameObjects that are spawned by this spawner
    // It's an inner class to prevent it from appearing in the "Add Component" menu in the unity editor
    public class SpawnedObject : MonoBehaviour
    {
        public bool isVisible = true;
        private bool isSpawnerActive = true;
        private MeshRenderer ren;

        private void Awake()
        {
	        ren = GetComponentInChildren<MeshRenderer>();
	        
        }

        public void SpawnerActiveStatChanged(bool spawnerActive)
        {
            isSpawnerActive = spawnerActive;
            UpdateOwnActiveState();
        }

        private void Update()
        {
	        isVisible = ren.isVisible;
	        UpdateOwnActiveState();
        }

        /*
        private void OnBecameVisible()
        {
            isVisible = true;
        }

        private void OnBecameInvisible()
        {
            isVisible = false;
            UpdateOwnActiveState();
            
	        Debug.Log("Became invisible");
        }
        */

        private void UpdateOwnActiveState()
        {
            if (!(isVisible || isSpawnerActive))
            {
                gameObject.SetActive(false);
            }
        }
    }

    #endregion
    
    [SerializeField] private WeightedSpawnTableEntry[] spawnTable = null;

    private SpawnedObject mostRecentObject = null;

    private int spawnValueCap = 0;

    private void Awake()
    {
        spawnValueCap = 0;
        foreach (WeightedSpawnTableEntry entry in spawnTable)
        {
            spawnValueCap += entry.spawnWeight;
        }
    }

    private void OnEnable()
    {
        SpawnObject();
    }

    private void OnDisable()
    {
        if (mostRecentObject)
        {
            mostRecentObject.SpawnerActiveStatChanged(false);
        }
    }

    private void SpawnObject()
    {
        int spawnValue = Random.Range(0, spawnValueCap + 1);

        GameObject prefabToSpawn = null;

        foreach (WeightedSpawnTableEntry entry in spawnTable)
        {
            spawnValue -= entry.spawnWeight;

            if (spawnValue <= 0)
            {
                prefabToSpawn = entry.prefabToSpawn;
                break;
            }
        }

        if (prefabToSpawn)
        {
            GameObject prefabInstance =
                ObjectPoolManager.GetPooledObject(prefabToSpawn, transform.position, Quaternion.identity);
            mostRecentObject = prefabInstance.GetComponent<SpawnedObject>();
            if (!mostRecentObject)
            {
                mostRecentObject = prefabInstance.AddComponent<SpawnedObject>();
            }

            mostRecentObject.isVisible = true; // TODO (minor) check if this is actually needed
            mostRecentObject.SpawnerActiveStatChanged(true);
        }
        else
        {
            mostRecentObject = null;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Color oldColor = Gizmos.color;
        Gizmos.color = Color.red;
        
        Gizmos.DrawSphere(transform.position, 0.5f);

        Gizmos.color = oldColor;
    }
#endif
}
