using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeRotation : MonoBehaviour
{
    
    private Rigidbody2D rigidBody;
    private HingeJoint2D joint;
    private JointMotor2D jointMotor;

    public HingeJoint2D topJoint;
    
    
    public float resetSpeed = 500f;
    public float stillForce = 200f;
    public float normalForce = 140f;
     

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        joint = GetComponent<HingeJoint2D>();
        jointMotor = joint.motor;
        
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if(joint.jointAngle <= -3f)
        {
            
            joint.useMotor = true;
            jointMotor.motorSpeed = resetSpeed;
            jointMotor.maxMotorTorque = normalForce;
            joint.motor = jointMotor;

            topJoint.useMotor = false;
           

        }

        if(joint.jointAngle >= -1f && joint.jointAngle <= 1f)
        {
            jointMotor.motorSpeed = 0f;
            jointMotor.maxMotorTorque = stillForce;
            joint.motor = jointMotor;

            if (topJoint.jointAngle >= -1 && topJoint.jointAngle <= 1f)
            {
                topJoint.useMotor = true;
            }
        }

        

        if(joint.jointAngle >= 3f)
        {
            joint.useMotor = true;
            jointMotor.motorSpeed = -resetSpeed;
            jointMotor.maxMotorTorque = normalForce;
            joint.motor = jointMotor;

            topJoint.useMotor = false;
        }
       









    }
}
