using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
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

    float G = 6.67f;
    Vector3 startVelocity = new Vector3(0f, 10f, 0f);
    
    public Vector3 calculateForce(){
        sunPosition = sun.transform.position;
        earthPosition = earth.transform.position;

        float radius = Vector3.Distance(sunPosition, earthPosition);
        //Debug.Log("Distance" + distance);

        float radiusSquare = radius * radius;
        //Debug.Log("distanceSquare" + distanceSquare);

        float force = (G * sunRigidBoby.mass * earthRigidBody.mass)/radiusSquare;
        //Debug.Log("Force" + force);

        Vector3 heading = sunPosition - earthPosition;
        //Debug.Log("Heading" + heading);

        Vector3 forceWithDirection = (force * (heading/heading.magnitude)); //check
        //Debug.Log("forceWithDirection" + forceWithDirection);
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
        float time = 0.001f;
        originalPosition = earth.transform.position;
        acceleration = (calculateForce() / earthRigidBody.mass);
        earth.transform.position += (velocity*time + 0.5f * acceleration * time * time);
        newPosition = earth.transform.position;
        velocity = (newPosition - originalPosition) / time;
        print(velocity);
    }
}
