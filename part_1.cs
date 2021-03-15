using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Lab1 : MonoBehaviour
{
    [Range(-10.0f, 10.0f)] public float G = 6.67f;

    public GameObject sun;
    public GameObject earth;

    private Rigidbody sunRB;
    private Rigidbody earthRB;

    //## PART 2 VALUE##-------------------------
    //private Vector3 initialVelocity = new Vector3(0.0f, 0.0f, 15.0f);
    //Sun Mass = 50000
    //Earth Mass = 100
    //Original pos: earth X: 4.33

    //## PART 1 VALUE##-------------------------
    private Vector3 initialVelocity = new Vector3(0, 0, 50.0f);
    //Sun Mass = 100000
    //Earth Mass = 0.001
    //Original pos: earth X: 13.5

    private Vector3 originalPos, prevPosition, newPosition;
    private Vector3 velocity;
    private Vector3 acceleration;
    Vector3 movingDir;
    [SerializeField] private float distance_e2s;

    private float force;
    private Vector3 dir;
    //[Range(0, 10.0f)] [SerializeField] float timeScale;

    // Start is called before the first frame update
    void Start()
    {
        sunRB = sun.GetComponent<Rigidbody>();
        earthRB = earth.GetComponent<Rigidbody>();

        originalPos = earth.transform.position;
        earthRB.AddForce(initialVelocity, ForceMode.VelocityChange);
    }

    private Vector3 calForce()
    {

        //distance between Earth and the Sun 
        distance_e2s = Vector3.Distance(sun.transform.position, earth.transform.position);
        force = (G * sunRB.mass * earthRB.mass) / (distance_e2s * distance_e2s);

        Vector3 sPos = sun.GetComponent<Transform>().position;
        Vector3 ePos = earth.GetComponent<Transform>().position;
        
        movingDir = (sPos - ePos).normalized;
        dir = (force * (movingDir / movingDir.magnitude));

        return dir;
    }

    void FixedUpdate()
    {
        float time = 0.001f;
        //Time.timeScale = timeScale;
        prevPosition = earth.transform.position;
        acceleration = (calForce()*0.1f / earthRB.mass);
        //Part 1 -------------------------------------
        earthRB.AddForce(calForce() * 0.1f, ForceMode.VelocityChange);
        //Part 2 -------------------------------------
        //earth.transform.position += (velocity * time + 0.5f * acceleration * time * time);

        newPosition = earth.transform.position;
        velocity = (newPosition - prevPosition) / time;
        //Part 1 print
        //Debug.Log("Velocity: " + velocity);
        //Part 2 print
        Debug.Log("Velocity: " + earthRB.velocity);
    }
}
