
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthMovementPart1 : MonoBehaviour
{
    //this object rigidbody component
    private Rigidbody m_Rb;

    //setting up sun's property
    Vector3 m_SunPos = Vector3.zero;
    float m_SunMass = 50000.0f;

    //setting up earth's property
    float m_EarthMass = 100.0f;

    //setting up initial force
    Vector3 m_InitForce = new Vector3(0.0f, 0.0f, 10.0f);

    //each frame velocity
    Vector3 m_Velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        //get rigidbody component
        m_Rb = GetComponent<Rigidbody>();

        //apply initial force
        m_Rb.AddForce(m_InitForce, ForceMode.VelocityChange);
    }

    private void FixedUpdate()
    {
        Vector3 gravitationForce = _GetGravitationForce();
        Vector3 appliedForce = gravitationForce / gravitationForce.magnitude;
        m_Rb.AddForce(appliedForce, ForceMode.VelocityChange);
    }

    //get the distance between sun and earth
    private float _GetDistance()
    {
        return Vector3.Distance(m_SunPos, transform.position);
    }

    //get normalized vector between sun and earth
    private Vector3 _GetNormalized()
    {
        Vector3 movementDirection = m_SunPos - transform.position;

        return movementDirection / movementDirection.magnitude;
    }

    private Vector3 _GetGravitationForce()
    {
        //get the radius as a distance between earth and sun
        float radius = _GetDistance();

        //constant G value
        const float G = 6.67f;

        //get force amount = (G * m1 * m2) / r^2
        float forceAmount = (G * m_SunMass * m_EarthMass) / (radius * radius);

        //return gravitation force
        return forceAmount * _GetNormalized();
    }
}

