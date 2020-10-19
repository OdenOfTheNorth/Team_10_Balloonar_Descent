using UnityEngine;

[DefaultExecutionOrder(-100)]
public class WindManager : MonoBehaviour
{
    public delegate void WindReceiverDelegate(Vector2 wind);
    public WindReceiverDelegate windCallback;

    [Range(0.0f, 10.0f)]
    public float startWindStrength = 1.0f;
    [System.NonSerialized]
    public float currentWindStrength = 0.0f;
    
    [Header("Visual")]
    [System.NonSerialized]
    public Vector2 currentWindDirection = Vector2.zero;
    private Vector2 currentVisualWindDirection = Vector2.zero;
    public float visualWindChangeSpeed = 5.0f;
    
    [Header("Auditory")]
    [System.NonSerialized]
    public Vector2 currentAudioWindDirection = Vector2.zero;
    [System.NonSerialized]
    public float currentAudioWindMagnitude = 0.0f;
    public float auditoryWindChangeSpeed = 5.0f;
    
    private GameObject parent = null;
    
    private static WindManager actualInstance;
    public static WindManager instance
    {
        get
        {
            if (actualInstance != null) 
                return actualInstance;
            else if(!applicationIsQuitting) // So nothing can try to access the singleton if it was destroyed after the singleton and create a new one
            {
                actualInstance = FindObjectOfType<WindManager>();
                if (actualInstance != null) 
                    return actualInstance;
                else
                {
                    Debug.LogWarning("No Windmanager found in scene. Creating one but it might be expensive. " +
                                     "Please add a windmanager anywhere in the scene");
                    GameObject parent = new GameObject();
                    actualInstance = parent.AddComponent<WindManager>();
                    parent.name = "WindManager";
                    return actualInstance;
                }
            }
            return null; // application is closing 
        }
    }

    private int shaderWindDirectionPropertyID = 0;
    private void Awake()
    {
        applicationIsQuitting = false;
        if (actualInstance != null && actualInstance != this)
        {
            Destroy(this);
        }
        else
        {
            actualInstance = this;
        }
        shaderWindDirectionPropertyID = Shader.PropertyToID("_WindDirection");
    }

    private void Start()
    {
        currentWindStrength = startWindStrength;
    }

    private void FixedUpdate()
    {
        windCallback?.Invoke(currentWindDirection * currentWindStrength); // Ternary operator to check if delegate has any subscribers
    }

    private void Update()
    {
        UpdateAudioVisualWind();
    }

    private static bool applicationIsQuitting = false;
    
    private void OnDestroy()
    {
        applicationIsQuitting = true;
        if (parent != null)
        {
            Destroy(parent); // If script had to create itself because it didn't exist in scene from start
        }
    }

    public void ResetWindStrength()
    {
        currentWindStrength = startWindStrength;
    }

    private void UpdateAudioVisualWind()
    {
        currentVisualWindDirection = Vector2.Lerp(currentVisualWindDirection, currentWindDirection, Time.deltaTime * visualWindChangeSpeed);
        Shader.SetGlobalVector(shaderWindDirectionPropertyID, new Vector4(currentVisualWindDirection.x, currentVisualWindDirection.y, 0, 1));

        currentAudioWindDirection = Vector2.Lerp(currentAudioWindDirection, currentWindDirection,
            Time.deltaTime * auditoryWindChangeSpeed);
        currentAudioWindMagnitude = currentAudioWindDirection.magnitude;
    }
}
