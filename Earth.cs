using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{
    [SerializeField]
    private GameObject sun;
    private Vector3 sunPos;
    private Vector3 earthPos;
    [SerializeField]
    private GameObject earth;

    private Rigidbody sunRigid;
    private Rigidbody earthRigid;

    Vector3 startVelocity = new Vector3(0f, 10f, 0f);

    Vector3 originalPosition;
    Vector3 newPosition;
    Vector3 velocity;
    Vector3 acceleration;

    private Vector3 Force()
    {
        //Position of both Sun and Earth
        sunPos = sun.transform.position;
        earthPos = earth.transform.position;
        //Find the distance between 2 objects (R)
        float distanceBetween = Vector3.Distance(sunPos, earthPos);
        //Find the distance between 2 objects with ^2
        float power2DistanceBetween = distanceBetween * distanceBetween;
        //Debug.Log("Distance = " + distanceBetween);
        //Gravity constant
        float g = 6.67f;
        //Debug.Log("G constant = " + g);
        float F = g * sunRigid.mass * earthRigid.mass / power2DistanceBetween;
        //Debug.Log("F = " + F);
        //The Direction of moving Sun and Earth
        Vector3 movingDirection = (sunPos - earthPos);

        Vector3 FDircetion = (F * (movingDirection / movingDirection.magnitude));
        return FDircetion;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Get both of RigidBody of 2 Objects
        sunRigid = sun.GetComponent<Rigidbody>();
        earthRigid = earth.GetComponent<Rigidbody>();
        earthRigid.AddForce(startVelocity, ForceMode.VelocityChange);
    }

    private void FixedUpdate()
    {
        float time = 0.001f;
        originalPosition = earth.transform.position;
        acceleration = (Force() / earthRigid.mass);
        earth.transform.position += (velocity * time + 0.5f * acceleration * time * time);
        newPosition = earth.transform.position;
        velocity = (newPosition - originalPosition) / time;
        Debug.Log(velocity);
    }
}
