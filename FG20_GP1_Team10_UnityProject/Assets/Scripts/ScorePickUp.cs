using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickUp : MonoBehaviour
{
    public LayerMask hitlayer;

    [Tooltip("1 to 3, higher number = more points")]
    [SerializeField] private int collectableNumber=1;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        int layer = (int) Mathf.Log(hitlayer.value, 2);
        
        if (collider.gameObject.layer == layer)
        {
            ScoreKeeper _scoreKeeper = ScoreKeeper.Get();
            _scoreKeeper.CollectableTaken(collectableNumber);

            GetComponentInChildren<MeshRenderer>().enabled = false;

            //Pick-up SFX
            SoundManager.instance.PlayPickup();

            Destroy(gameObject, 2f);
        }
    }
}
