using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupFeedback : MonoBehaviour
{
    [SerializeField]
    ParticleSystem _ps;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 8)
        {
            _ps.gameObject.SetActive(true);
        }
    }
}
