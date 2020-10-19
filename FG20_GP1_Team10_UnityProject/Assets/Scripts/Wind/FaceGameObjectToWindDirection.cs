using System;
using UnityEngine;

public class FaceGameObjectToWindDirection : MonoBehaviour
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

	private void Awake()
	{
		WindManager.instance.windCallback += RotateToWindDirection;
	}

	private void RotateToWindDirection(Vector2 windDirection)
	{
		if (windDirection.sqrMagnitude == 0) // no input
			return;
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