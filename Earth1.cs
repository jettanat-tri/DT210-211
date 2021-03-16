using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth1 : MonoBehaviour
{
    public GameObject sun;
    public GameObject earth;
    Rigidbody sunRigidBoby;
    Rigidbody earthRigidBody;

    [SerializeField]
    Vector3 sunPosition;
    [SerializeField]
    Vector3 earthPosition;
    [SerializeField]
    Vector3 velocity;
    [SerializeField]
    Vector3 acceleration;

    Vector3 originalPosition;
    Vector3 newPosition;
    Vector3 ForceVector;
    Vector3 Force;

    float G = 6.67f * Mathf.Pow(10, -11);
    Vector3 startVelocity = new Vector3(0f, 10f, 0f);
    
    public Vector3 calculateForce(){
        sunPosition = sun.transform.position;
        earthPosition = earth.transform.position;

        float radius = Vector3.Distance(sunPosition, earthPosition);
        float radiusSquare = radius * radius;
        float force = (G * sunRigidBoby.mass * earthRigidBody.mass)/radiusSquare;
        Vector3 heading = sunPosition - earthPosition;

        Vector3 forceWithDirection = (force * (heading/heading.magnitude));
        return forceWithDirection;
    }

    void Start()
    {
        earthRigidBody = earth.GetComponent<Rigidbody>();
        sunRigidBoby = sun.GetComponent<Rigidbody>();
        earthRigidBody.AddForce(startVelocity, ForceMode.VelocityChange);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Force = calculateForce();
        ForceVector = Force / Mathf.Sqrt((Force.x * Force.x) + (Force.y * Force.y) + (Force.z * Force.z));

        earthRigidBody.AddForce(ForceVector, ForceMode.VelocityChange);
    }
}
