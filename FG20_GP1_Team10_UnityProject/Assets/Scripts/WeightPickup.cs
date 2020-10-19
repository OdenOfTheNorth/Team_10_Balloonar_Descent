using System;
using System.Collections.Generic;
using UnityEngine;

public class WeightPickup : MonoBehaviour
{
    private WindReceiver playerWindReceiver = null;
    private BalloonDescent playerBalloonDescent = null;
    private Joint2D[] weightAttachment = null;
    private float currentWeightPreviousGravityScale = 0.0f;
    
    private bool isHoldingWeight = false;
    
    [Range(0.0f, 1.0f)]
    public float weightWindReductionMultiplier = 0.5f;

    [Range(1.0f, 10.0f)] 
    public float playerDownwardsMultiplierWhileCarryingWeight = 1.5f;

    private Rigidbody2D rigidbodyAttached;
    private List<GameObject> ToBeSorted = new List<GameObject>();
    
    private void Awake()
    {
        playerWindReceiver = transform.parent.parent.GetComponentInChildren<WindReceiver>(); // TODO: Make less shit :)
        playerBalloonDescent = GetComponentInParent<BalloonDescent>();
        weightAttachment = GetComponents<Joint2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ToBeSorted.Add(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        ToBeSorted.Remove(other.gameObject);
    }

    public void TogglePickup()
    {
        if (isHoldingWeight)
        {
            DetachWeight(rigidbodyAttached);
        }
        else if (ToBeSorted.Count > 0)
        {
            Rigidbody2D wight = GetClosesRock().GetComponent<Rigidbody2D>();
            AttachWeight(wight);
        }
    }

    private void AttachWeight(Rigidbody2D objectToAttach)
    {
        Debug.Log("object to attach",objectToAttach );
        rigidbodyAttached = objectToAttach;
        weightAttachment[0].connectedBody = objectToAttach;
        weightAttachment[1].connectedBody = objectToAttach;
        currentWeightPreviousGravityScale = objectToAttach.gravityScale;
        objectToAttach.gravityScale = 0;
        objectToAttach.GetComponent<Collider2D>().enabled = false;
        playerWindReceiver.windReceivedMultiplier *= weightWindReductionMultiplier;
        playerBalloonDescent.currentDescentSpeed *= playerDownwardsMultiplierWhileCarryingWeight;
        isHoldingWeight = true;
    }

    private GameObject GetClosesRock()
    {
        float shortes = float.MaxValue;
        GameObject closetObject = default;
        foreach (GameObject Pos in ToBeSorted)
        {
            float distanse = Vector2.Distance(transform.position, Pos.transform.position);
            if (distanse < shortes)
            {
                shortes = distanse;
                closetObject = Pos;
            }
        }

        return closetObject;
    }
    
    private void DetachWeight(Rigidbody2D objectToDetach)
    {
        Debug.Log("Detach Weight", objectToDetach);
        objectToDetach.gravityScale = currentWeightPreviousGravityScale;
        objectToDetach.GetComponent<Collider2D>().enabled = true;
        weightAttachment[0].connectedBody = null;
        weightAttachment[1].connectedBody = null;
        playerWindReceiver.windReceivedMultiplier /= weightWindReductionMultiplier;
        playerBalloonDescent.currentDescentSpeed /= playerDownwardsMultiplierWhileCarryingWeight;
        isHoldingWeight = false;
    }

    private void FixedUpdate()
    {
        Quaternion worldRotation = transform.parent.parent.rotation;
        gameObject.transform.rotation = worldRotation;
    }
}