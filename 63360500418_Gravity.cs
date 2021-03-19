using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour{

    [SerializeField]
    private GameObject sunObj;
    private Vector3 sunPos;
    
    [SerializeField]
    private GameObject earthObj;
    private Vector3 earthPos;

    private Rigidbody sunRig;
    private Rigidbody earthRig;

    Vector3 InitialVel = new Vector3(0f, 10f, 0f);

    Vector3 originPos;
    Vector3 newPos;
    Vector3 velocity;
    Vector3 acceleration;

    [Range(-10.00f, 10.00f)]
    public const float g = 6.67f;

    private Vector3 Force()
    {
        sunPos = sunObj.transform.position;
        earthPos = earthObj.transform.position;

        float distance = Vector3.Distance(sunPos, earthPos);
        float distancePow2 = distance * distance;

        float force = g * sunRig.mass * earthRig.mass / distancePow2;
        Vector3 direction = (sunPos - earthPos);

        Vector3 forceDir = (force * (direction / direction.magnitude));
        return forceDir;
    }

    // Start is called before the first frame update
    void Start()
    {
        sunRig = sunObj.GetComponent<Rigidbody>();
        earthRig = earthObj.GetComponent<Rigidbody>();
        earthRig.AddForce(InitialVel, ForceMode.VelocityChange);
    }

    private void FixedUpdate()
    {
        float time = 0.001f;

        originPos = earthObj.transform.position;

        acceleration = (Force() / earthRig.mass);

        earthObj.transform.position += (velocity * time + 0.5f * acceleration * time * time);
        newPos = earthObj.transform.position;

        velocity = (newPos - originPos) / time;
        Debug.Log(velocity);
    }
}

