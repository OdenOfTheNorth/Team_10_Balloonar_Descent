using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedByFallingObjects : MonoBehaviour
{
    private float minimumVelocityToBreak=0f;

    private List<Renderer> renderers;
    private List<Collider2D> colliders;

    private ParticleSystem destructionParticles = null;

    private void Awake()
    {
        destructionParticles = transform.parent.GetComponentInChildren<ParticleSystem>();
        renderers = new List<Renderer>();
        foreach (Renderer ren in GetComponentsInChildren<Renderer>())
        {
            if (ren.enabled)
            {
                renderers.Add(ren);
            }
        }
        
        colliders = new List<Collider2D>();
        foreach (Collider2D coll in GetComponentsInChildren<Collider2D>())
        {
            if (coll.enabled)
            {
                colliders.Add(coll);
            }
        }
    }

    private void OnEnable()
    {
        foreach (Renderer ren in renderers)
        {
            ren.enabled = true;
        }
        foreach (Collider2D coll in colliders)
        {
            coll.enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (destructionParticles != null)
        {
            Vector2 VFXpos = collision.GetContact(0).point;
            destructionParticles.transform.position = new Vector3(VFXpos.x, VFXpos.y, transform.position.z);
            destructionParticles.Play();
        }
        
        Debug.Log(collision);
        if (Mathf.Abs(collision.relativeVelocity.y) >= minimumVelocityToBreak)
        {
            BreakObject();
        }
    }

    private void BreakObject()
    {
        foreach (Renderer ren in renderers)
        {
            ren.enabled = false;
        }
        foreach (Collider2D coll in colliders)
        {
            coll.enabled = false;
        }
    }

}
