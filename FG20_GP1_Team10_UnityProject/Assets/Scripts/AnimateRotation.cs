using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateRotation : MonoBehaviour
{
    private GameObject objectToRotate;

    Vector3 eulerRotation;

    private float rotationSpeed = 100f;

    private void Awake()
    {
        objectToRotate = this.gameObject;
        eulerRotation = objectToRotate.transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        float maxRotation = rotationSpeed * Time.deltaTime;
        eulerRotation = objectToRotate.transform.rotation.eulerAngles + new Vector3(0, 90, 0);
        objectToRotate.transform.rotation = Quaternion.RotateTowards(objectToRotate.transform.rotation, Quaternion.Euler(eulerRotation), maxRotation);
    }
}
