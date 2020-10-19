using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    private static ObjectPoolManager _currentObjectPoolManager = null;

    private static ObjectPoolManager CurrentOjbectPoolManager
    {
        get
        {
            if (!_currentObjectPoolManager)
            {
                _currentObjectPoolManager = FindObjectOfType<ObjectPoolManager>();
                if (!_currentObjectPoolManager)
                {
                    GameObject newObjectPoolManager = new GameObject("ObjectPoolManager");
                    _currentObjectPoolManager = newObjectPoolManager.AddComponent<ObjectPoolManager>();
                    Debug.LogError("No ObjectPoolManager found in scene," 
                                   + " please add an ObjectPoolManager anywhere in the scene when not in play mode.");
                }
            }

            return _currentObjectPoolManager;
        }
    }

    private Dictionary<GameObject, ObjectPool> objectPools;

    private void Awake()
    {
        if (!_currentObjectPoolManager)
        {
            _currentObjectPoolManager = this;
        }
        else if (_currentObjectPoolManager != this)
        {
            Destroy(gameObject);
            return;
        }

        objectPools = new Dictionary<GameObject, ObjectPool>();
    }

    public static GameObject GetPooledObject(GameObject prefab, Vector3 position, Quaternion rotation, bool active = true)
    {
        if (!CurrentOjbectPoolManager.objectPools.ContainsKey(prefab))
        {
            CurrentOjbectPoolManager.objectPools[prefab] = new ObjectPool(prefab);
        }

        return CurrentOjbectPoolManager.objectPools[prefab].GetPooledObject(position, rotation, active);
    }
}

