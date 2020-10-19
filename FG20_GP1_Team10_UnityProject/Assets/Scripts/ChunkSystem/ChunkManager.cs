using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ChunkManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> chunkQueue = null;
    [SerializeField] private GameObject[] chunkPrefabs = null;
    [SerializeField, Min(3)] private int activeChunkCount = 3;
    [Tooltip("The base spawn chance of each chunk")]
    [SerializeField] private int baseSpawnChance = 1;
    [Tooltip("How much chunks spawn chance will increase when a different chunk is spawned")]
    [SerializeField] private int spawnChanceIncrement = 1;

    private Chunk currentHeadChunk = null;
    private Chunk currentTailChunk = null;
    private List<Chunk> PlayerPresentChunks = new List<Chunk>();
    private Chunk currentActiveChunk = null;

    private Dictionary<GameObject, int> chunkSpawnChanceDict;
    private int totalSpawnChanceOfAllChunks = 0;

    private static ChunkManager _currentChunkManager = null;
    public static ChunkManager CurrentChunkManager
    {
        get
        {
            if (!_currentChunkManager)
            {
                _currentChunkManager = FindObjectOfType<ChunkManager>();
                Assert.IsNotNull(_currentChunkManager, "No chunk manager found in scene");
            }
            return _currentChunkManager;
        }
    }

    private void Awake()
    {
        if (!_currentChunkManager)
        {
            _currentChunkManager = this;
        }
        else if (_currentChunkManager != this)
        {
            Destroy(gameObject);
            return;
        }
        
        chunkSpawnChanceDict = new Dictionary<GameObject, int>();
        totalSpawnChanceOfAllChunks = 0;
        foreach (GameObject prefab in chunkPrefabs)
        {
            chunkSpawnChanceDict.Add(prefab, baseSpawnChance);
            totalSpawnChanceOfAllChunks += baseSpawnChance;
        }
    }

    private void Start()
    {
        PrepareDeveloperCheats();
        
        Camera mainCamera = Camera.main;
        
        Collider2D cameraCollider = mainCamera.GetComponent<Collider2D>();
        if (!cameraCollider)
        {
            cameraCollider = mainCamera.gameObject.AddComponent<CircleCollider2D>();
        }
        cameraCollider.isTrigger = true;

        Rigidbody2D cameraRigidBody = mainCamera.GetComponent<Rigidbody2D>();
        if (!cameraRigidBody)
        {
            cameraRigidBody = mainCamera.gameObject.AddComponent<Rigidbody2D>();
        }
        cameraRigidBody.bodyType = RigidbodyType2D.Kinematic;
        
        PlaceInitialChunks();
    }

    private void PlaceInitialChunks()
    {
        GameObject middleChunkGameObject = GetNextChunkInstance();
        Chunk middleChunk = middleChunkGameObject.GetComponent<Chunk>();
        middleChunk.destroyOnDisable = true;
        middleChunk.onTriggerEnterCallback += ChunkEntered;
        middleChunk.onTriggerExitCallback += ChunkExited;
        currentHeadChunk = middleChunk;
        currentTailChunk = middleChunk;
        middleChunkGameObject.SetActive(true);
        currentActiveChunk = middleChunk;
        
        for (int i = 0; i < activeChunkCount - 1; i++)
        {
            if (i % 2 == 0)
            {
                PlaceNewChunk();
            }
            else
            {
                GameObject chunkToPlaceGameObject = new GameObject("Dummy chunk " + (1 + i / 2));
                Chunk chunkToPlace = chunkToPlaceGameObject.AddComponent<Chunk>();
                chunkToPlace.destroyOnDisable = true;
                chunkToPlace.onTriggerEnterCallback += ChunkEntered;
                chunkToPlace.onTriggerExitCallback += ChunkExited;
                Vector3 placementOffset = new Vector3(0.0f, chunkToPlace.gameObjectToBottom + currentTailChunk.gameObjectToTop, 0.0f);
                chunkToPlaceGameObject.transform.position = currentTailChunk.transform.position + placementOffset;
                chunkToPlace.nextChunk = currentTailChunk;
                currentTailChunk = chunkToPlace;
                chunkToPlaceGameObject.SetActive(true);
            }
        }
    }

    private void ChunkEntered(Chunk caller)
    {
        PlayerPresentChunks.Add(caller);
    }

    private void ChunkExited(Chunk caller)
    {
        PlayerPresentChunks.Remove(caller);
        if (caller == currentActiveChunk && PlayerPresentChunks.Contains(caller.nextChunk))
        {
            IncrementChunks();
        }
    }
    
    private void IncrementChunks()
    {
        PlaceNewChunk();
        currentActiveChunk = currentActiveChunk.nextChunk;
        RemoveOldChunk();
    }
    
    private void PlaceNewChunk()
    {
        GameObject chunkToPlaceGameObject = GetNextChunkInstance();
        Chunk chunkToPlace = chunkToPlaceGameObject.GetComponent<Chunk>();
        chunkToPlace.onTriggerEnterCallback += ChunkEntered;
        chunkToPlace.onTriggerExitCallback += ChunkExited;
        Vector3 placementOffset = new Vector3(0.0f, -(chunkToPlace.gameObjectToTop + currentHeadChunk.gameObjectToBottom), 0.0f);
        chunkToPlaceGameObject.transform.position = currentHeadChunk.transform.position + placementOffset;
        currentHeadChunk.nextChunk = chunkToPlace;
        currentHeadChunk = chunkToPlace;
        chunkToPlaceGameObject.SetActive(true);
    }
    
    private void RemoveOldChunk()
    {
        currentTailChunk.onTriggerEnterCallback -= ChunkEntered;
        currentTailChunk.onTriggerExitCallback -= ChunkExited;
        GameObject oldTailGameObject = currentTailChunk.gameObject;
        currentTailChunk = currentTailChunk.nextChunk;
        oldTailGameObject.SetActive(false);
    }

    private GameObject GetNextChunkInstance()
    {
        GameObject chunkPrefab = null;
        if (chunkQueue.Count > 0)
        {
            chunkPrefab = chunkQueue[0];
            chunkQueue.RemoveAt(0);
        }
        else
        {
            chunkPrefab = GetPseudoRandomChunkPrefab();
        }
        return ObjectPoolManager.GetPooledObject(chunkPrefab, Vector3.zero, Quaternion.identity, false);
    }

    private GameObject GetPseudoRandomChunkPrefab()
    {
        GameObject prefabToReturn = null;
        int spawnValue = Random.Range(0, totalSpawnChanceOfAllChunks) + 1;

        foreach (GameObject prefab in chunkPrefabs)
        {
            spawnValue -= chunkSpawnChanceDict[prefab];
            if (spawnValue <= 0)
            {
                prefabToReturn = prefab;
                break;
            }
        }

        // fallback in case no prefab to spawn was found, should only be possible when there is only 1 prefab in the array
        if (prefabToReturn == null) 
        {
	        prefabToReturn = chunkPrefabs[0];
        }

        foreach (GameObject prefab in chunkPrefabs)
        {
            if (prefab == prefabToReturn)
            {
                totalSpawnChanceOfAllChunks -= chunkSpawnChanceDict[prefab];
                chunkSpawnChanceDict[prefab] = 0;
            }
            else if (chunkSpawnChanceDict[prefab] == 0)
            {
                chunkSpawnChanceDict[prefab] = baseSpawnChance;
                totalSpawnChanceOfAllChunks += baseSpawnChance;
            }
            else
            {
                chunkSpawnChanceDict[prefab] += spawnChanceIncrement;
                totalSpawnChanceOfAllChunks += spawnChanceIncrement;
            }
        }

        return prefabToReturn;
    }

    #region Developer cheats
    [Header("Developer cheat zone")]
    [SerializeField] private Dropdown chunkSelectionDropdown = null;
    [SerializeField] private Button spawnChunkButton = null;

    private List<GameObject> chunkPrefabsForCheats;
    
    private void PrepareDeveloperCheats()
    {
        if (!chunkSelectionDropdown || !spawnChunkButton)
        {
            Debug.Log("Chunk cheats unavailable due to missing reference either to chunk selection dropdown or spawn chunk button (or both)");
            return;
        }

        chunkPrefabsForCheats = new List<GameObject>();
        List<string> chunkPrefabNames = new List<string>();
        chunkSelectionDropdown.ClearOptions();
        foreach (GameObject chunk in chunkPrefabs)
        {
            if (!chunkPrefabsForCheats.Contains(chunk))
            {
                chunkPrefabsForCheats.Add(chunk);
                chunkPrefabNames.Add(chunk.name);
            }
        }

        foreach (GameObject chunk in chunkQueue)
        {
            if (!chunkPrefabsForCheats.Contains(chunk))
            {
                chunkPrefabsForCheats.Add(chunk);
                chunkPrefabNames.Add(chunk.name);
            }
        }

        chunkSelectionDropdown.AddOptions(chunkPrefabNames);
        spawnChunkButton.onClick.AddListener(AddSelectedChunkToQueue);
        
        chunkSelectionDropdown.gameObject.SetActive(false);
        spawnChunkButton.gameObject.SetActive(false);
    }

	public static void ToggleCheatVisibility(InputAction.CallbackContext context)
	{
		if (CurrentChunkManager)
		{
			CurrentChunkManager.ToggleCheatVisibility();
		}
	}

    private void ToggleCheatVisibility()
    {
        if (!chunkSelectionDropdown || !spawnChunkButton)
        {
            return;
        }

        if (chunkSelectionDropdown.gameObject.activeSelf)
        {
            chunkSelectionDropdown.gameObject.SetActive(false);
            spawnChunkButton.gameObject.SetActive(false);
        }
        else
        {
            chunkSelectionDropdown.gameObject.SetActive(true);
            spawnChunkButton.gameObject.SetActive(true);
        }
    }

    private void AddSelectedChunkToQueue()
    {
        if (chunkSelectionDropdown)
        {
            chunkQueue.Add(chunkPrefabsForCheats[chunkSelectionDropdown.value]);
        }
    }
    #endregion
}
