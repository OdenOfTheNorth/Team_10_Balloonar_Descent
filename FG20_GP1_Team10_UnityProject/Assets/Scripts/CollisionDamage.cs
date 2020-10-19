using System;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    public float damageMultiplier = 10f;
    //public LayerMask hitlayer;
    private CharacterHealth PlayerHealth;

    private void Awake()
    {
        PlayerHealth  = GetComponentInParent<CharacterHealth>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //int layer = (int) Mathf.Log(hitlayer.value, 2);
        Vector2 surfaceNormal = collision.GetContact(0).normal;
        float dotNormalBody = Vector2.Dot(surfaceNormal, collision.relativeVelocity.normalized);
        PlayerHealth.TakeDamage(Mathf.Abs(dotNormalBody * damageMultiplier * collision.relativeVelocity.magnitude));
    }
}
