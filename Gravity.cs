using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Gravity : MonoBehaviour
{
    [SerializeField]
    private GameObject m_sun;
    private Vector3 sunPos;
    private Vector3 earthPos;
    [SerializeField]
    private GameObject m_earth;

    private Rigidbody sun_rb;
    private Rigidbody earth_rb;

    Vector3 startVelocity = new Vector3(0f, 10f, 0f);

    Vector3 originalPosition;
    Vector3 newPosition;
    Vector3 velocity;
    Vector3 acceleration;

    [Range(-10.00f, 10.00f)]
    public const float g = 6.67f; 

    private Vector3 Force()
    {
        sunPos = m_sun.transform.position;
        earthPos = m_earth.transform.position;

        float distanceBetween = Vector3.Distance(sunPos, earthPos);
        float power2DistanceBetween = distanceBetween * distanceBetween;

        float F = g * sun_rb.mass * earth_rb.mass / power2DistanceBetween;
        Vector3 movingDirection = (sunPos - earthPos);

        Vector3 FDircetion = (F * (movingDirection / movingDirection.magnitude));
        return FDircetion;
    }

    // Start is called before the first frame update
    void Start()
    {
        sun_rb = m_sun.GetComponent<Rigidbody>();
        earth_rb = m_earth.GetComponent<Rigidbody>();
        earth_rb.AddForce(startVelocity, ForceMode.VelocityChange);
    }

    private void FixedUpdate()
    {
        float time = 0.001f;

        originalPosition = m_earth.transform.position;

        acceleration = (Force() / earth_rb.mass);

        m_earth.transform.position += (velocity * time + 0.5f * acceleration * time * time);
        newPosition = m_earth.transform.position;

        velocity = (newPosition - originalPosition) / time;
        Debug.Log(velocity);
    }
}

