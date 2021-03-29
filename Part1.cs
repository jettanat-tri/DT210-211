using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part1 : MonoBehaviour
{
    [SerializeField] private GameObject sun;
    [SerializeField] private GameObject earth;

    private Vector3 sunPos, earthPos;
    private Rigidbody sunRigid, earthRigid;
    private Vector3 velocity, acceleration;
   
    private float _G = 6.67f * Mathf.Pow(10, -11);
    Vector3 startVelocity = new Vector3(0f, 10f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        earthRigid = earth.GetComponent<Rigidbody>();
        sunRigid = sun.GetComponent<Rigidbody>();
        earthRigid.AddForce(startVelocity, ForceMode.VelocityChange);
    }

    void Update()
    {
           
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 force = CalForce();
        Vector3 forceVector = force / Mathf.Sqrt((force.x * force.x) + (force.y * force.y) + (force.z * force.z));
        earthRigid.AddForce(forceVector, ForceMode.VelocityChange);
    }

    private Vector3 CalForce()
    {
        sunPos = sun.transform.position;
        earthPos = earth.transform.position;

        float distance = Vector3.Distance(sunPos, earthPos);
        float distanceSqr = distance * distance;
        float force = (_G * sunRigid.mass * earthRigid.mass) / distanceSqr;

        Vector3 direction = sunPos - earthPos;
        Vector3 forceWithDir = (force * (direction / direction.magnitude));
        return forceWithDir;
    }

}
