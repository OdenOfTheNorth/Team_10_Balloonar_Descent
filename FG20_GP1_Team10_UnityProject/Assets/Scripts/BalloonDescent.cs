using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BalloonDescent : MonoBehaviour
{
    [Range(0.0f, 50.0f)]
    public float startDescentSpeed = 1.0f;

    [System.NonSerialized]
    public float currentDescentSpeed = 0.0f;

    private new Rigidbody2D rigidbody;
    private Transform ownTransform;
	
    private void Awake()
    {
        ownTransform = transform;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        currentDescentSpeed = startDescentSpeed;
    }

    private void FixedUpdate()
    {
        if(rigidbody.velocity.y > -currentDescentSpeed)
            rigidbody.velocity += Vector2.down * (currentDescentSpeed * Time.fixedDeltaTime);
    }
}
