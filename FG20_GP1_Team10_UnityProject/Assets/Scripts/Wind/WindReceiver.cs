using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WindReceiver : MonoBehaviour
{
    [Tooltip("The amount of force applied to the object in the direction of the wind (also multiplied by the windstrength). Seperated in X & Y Axis")]
    public Vector2 windReceivedMultiplier = new Vector2(10.0f, 10.0f);

    private new Rigidbody2D rigidbody;

    private bool isVisible = false;
    
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        WindManager.instance.windCallback += ReceiveWind;
    }

    private void OnDisable()
    {
        if (WindManager.instance != null) 
            WindManager.instance.windCallback -= ReceiveWind;
    }

    private void ReceiveWind(Vector2 wind)
    {
        if (!isVisible)
            return;
        Vector2 velocity = rigidbody.velocity;
        velocity.x += wind.x * (windReceivedMultiplier.x * Time.fixedDeltaTime);
        velocity.y += wind.y * (windReceivedMultiplier.y * Time.fixedDeltaTime);

        rigidbody.velocity = velocity;
    }
    
    private void OnBecameVisible()
    {
        isVisible = true;
    }

    private void OnBecameInvisible()
    {
        isVisible = false;
    }
}
