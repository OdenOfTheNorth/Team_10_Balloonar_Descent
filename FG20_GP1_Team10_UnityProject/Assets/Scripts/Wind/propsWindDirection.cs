using System;
using UnityEngine;

public class propsWindDirection : MonoBehaviour
{
	private enum FacingDirection
	{
		[Tooltip("Green arrow of object Transform")]
		Up,
		[Tooltip("Red Arrow of object Transform")]
		Right,
		[Tooltip("Opposite of green Arrow of object Transform")]
		Down,
		[Tooltip("Opposite of red Arrow of object Transform")]
		Left
        
	}
    
	[SerializeField]
	private FacingDirection facingDirection = FacingDirection.Up;

    [SerializeField] GameObject windTarget;
    [SerializeField] ParticleSystem wind;

	private void OnEnable()
	{
		WindManager.instance.windCallback += RotateToWindDirection;
	}

	private void OnDisable()
	{
		if (WindManager.instance)
		{
			WindManager.instance.windCallback -= RotateToWindDirection;
		}
	}

	private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, windTarget.transform.position, 100f * Time.deltaTime);
    }

    private void RotateToWindDirection(Vector2 windDirection)
	{
		if (windDirection.sqrMagnitude == 0) // no input
        {
            wind.Stop();
            
            return;
        }

        else if(!wind.isEmitting)
        { wind.Play(); }
			
		switch (facingDirection)
		{
			case FacingDirection.Up:
				transform.up = windDirection;
				break;
			case FacingDirection.Right:
				transform.right = windDirection;
				break;
			case FacingDirection.Down:
				transform.up = -windDirection;
				break;
			case FacingDirection.Left:
				transform.right = -windDirection;
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}
}