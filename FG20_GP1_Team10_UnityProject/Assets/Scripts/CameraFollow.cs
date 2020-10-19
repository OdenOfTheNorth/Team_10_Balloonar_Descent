using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Player and Camera transforms
    [SerializeField] private Transform playerTransform = null;
    private Transform camTransform = null;
    // Smooth damp variable
    private Vector3 velocity = Vector3.zero;
    public float SmoothTime = 1f;
    //
    public Vector3 offset = new Vector3();
    
    [Header("Camera Shake")]
    public static float trauma = 0.0f;
    public float traumaReduction = 0.5f;
    public Vector3 maxEulerShakeRotation = new Vector3(10, 10, 10);
    public float perlinSeed = 12345677.132415f;
    private static float damageGivingMaxTrauma = 40.0f;
        
    private void Awake()
    {
        camTransform = transform;
    }

    private void LateUpdate()
    {
        if (trauma > 0)
        {
            ShakeScreen();
        }
        
        if (playerTransform != null)
        {
            camTransform.position = Vector3.SmoothDamp(transform.position,new Vector3(0, playerTransform.position.y + offset.y, offset.z),ref velocity, SmoothTime);
        }
    }
    
    private void Update()
    {
        if (trauma > 0)
        {
            trauma -= traumaReduction * Time.deltaTime;
        }
    }

    public static void AddTrauma(float damage)
    {
        trauma = Mathf.Clamp01( damage / damageGivingMaxTrauma);
    }

    private void ShakeScreen()
    {
        float perlinSeedTime = perlinSeed + Time.time; 
        float yaw = Mathf.Pow(trauma, 3) * maxEulerShakeRotation.x * Mathf.PerlinNoise(perlinSeedTime, perlinSeedTime);
        float pitch = Mathf.Pow(trauma, 3) * maxEulerShakeRotation.y * Mathf.PerlinNoise(perlinSeedTime + 1, perlinSeedTime + 1);
        float roll = Mathf.Pow(trauma, 3) * maxEulerShakeRotation.z * Mathf.PerlinNoise(perlinSeedTime + 2, perlinSeedTime + 2);
        
        transform.rotation = Quaternion.Euler(yaw, pitch, roll);
    }
}
