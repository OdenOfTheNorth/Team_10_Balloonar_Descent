using UnityEngine;

// ObjectPool effectively a linked-list stack specifically for objectpooling gameObjects
public class ObjectPool
{
    #region Poolable component

    // This class is automatically added to objects that are instantiated by the ObjectPool
    // It's an inner class to prevent it from appearing in the "Add Component" menu in the unity editor
    public class Poolable : MonoBehaviour
    {
        public Poolable nextPoolable;

        public delegate void OnObjectRemovalDelegate(Poolable caller);

        public OnObjectRemovalDelegate onDisableCallback;
        private OnObjectRemovalDelegate onDestroyCallback;

        public void SetUpPoolable(OnObjectRemovalDelegate onDisable, OnObjectRemovalDelegate onDestroy)
        {
            onDisableCallback = onDisable;
            onDestroyCallback = onDestroy;
        }

        private void OnDisable()
        {
            onDisableCallback?.Invoke(this);
        }

        private void OnDestroy()
        {
            onDestroyCallback?.Invoke(this);
        }
    }

    #endregion // Poolable component

    private int count = 0;
    private bool hasNext = false;
    private Poolable nextPoolable = null;
    private GameObject sourcePrefab = null;

    public ObjectPool(GameObject prefab)
    {
        sourcePrefab = prefab;
    }

    public GameObject GetPooledObject(Vector3 position, Quaternion rotation, bool active = true)
    {
        GameObject toReturn = null;

        if (hasNext)
        {
            toReturn = Pop();
            toReturn.transform.SetPositionAndRotation(position, rotation);
            toReturn.SetActive(active);
        }
        else
        {
            bool prefabWasActive = sourcePrefab.activeSelf;
            if (!active)
            {
                sourcePrefab.SetActive(false);
            }
            toReturn = GameObject.Instantiate(sourcePrefab, position, rotation);
            Poolable poolableComponent = toReturn.AddComponent<Poolable>();
            poolableComponent.SetUpPoolable(AddObjectToPool, RemoveFromStack);
            if (prefabWasActive)
            {
                sourcePrefab.SetActive(true);
            }
        }

        return toReturn;
    }

    private GameObject Pop()
    {
        GameObject toReturn = nextPoolable.gameObject;
        nextPoolable = nextPoolable.nextPoolable;
        count--;
        hasNext = count > 0;
        return toReturn;
    }

    private void RemoveFromStack(Poolable toRemove)
    {
        if (!hasNext || !toRemove)
        {
            return;
        }

        if (nextPoolable == toRemove)
        {
            nextPoolable = nextPoolable.nextPoolable;
            count--;
            hasNext = count > 0;
            return;
        }

        for (Poolable currentValid = nextPoolable; currentValid.nextPoolable; currentValid = currentValid.nextPoolable)
        {
            if (currentValid.nextPoolable == toRemove)
            {
                currentValid.nextPoolable = currentValid.nextPoolable.nextPoolable;
                count--;
                hasNext = count > 0;
                return;
            }
        }
    }

    private void AddObjectToPool(Poolable objectToAdd)
    {
        objectToAdd.nextPoolable = nextPoolable;
        nextPoolable = objectToAdd;
        count++;
        hasNext = true;
    }

}

