using UnityEngine;
using UnityEngine.UI;

public class FaceWindDirection : MonoBehaviour
{
    private Transform ownTransform;
    private Image image;

    private Sprite arrowSprite = default;

    private void Awake()
    {
        image = GetComponent<Image>();
        arrowSprite = image.sprite;
        ownTransform = transform;
    }

    private void Update()
    {
        Vector2 windDirection = WindManager.instance.currentWindDirection;
        if(windDirection.sqrMagnitude > 0)
        {
            ownTransform.right = windDirection;
            if (image.enabled == false)
            {
                image.enabled = true;
            }
        }
        else
        {
            image.enabled = false;
        }
       
    }
}
