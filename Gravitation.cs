using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Gravitation : MonoBehaviour
{
    public static List<Gravitation> s_allObjects = new List<Gravitation>();
    private static int s_massMultiplier = 24;

    public static bool s_AddForceCal = true;

    private float _G = 6.67408f;

    private Rigidbody _rb;
    private Vector3 oldVelocity;
    
    private void Awake() 
    {
        s_allObjects.Add(this);

        _rb = this.GetComponent<Rigidbody>();
    }

    private void OnDestroy() 
    {
        s_allObjects.Remove(this);
    }

    private void FixedUpdate() 
    {
        if (s_AddForceCal)
            AddForceToPlanet();
        else
            ChangePlanetPos();
    }

    private void ChangePlanetPos()
    {
        for (int i = 0; i < s_allObjects.Count; i++)
        {
            if (s_allObjects[i] != this)
            {
                oldVelocity += 0.5f * GetForce(s_allObjects[i]) * Time.deltaTime;
                _rb.transform.position += 0.5f * oldVelocity * Time.deltaTime;
            }
        }
    }

    private void AddForceToPlanet()
    {
        for (int i = 0; i < s_allObjects.Count; i++)
        {
            if (s_allObjects[i] != this)
            {
                _rb.AddForce(GetForce(s_allObjects[i]));
            }
        }
    }

    private Vector3 GetForce(Gravitation s1)
    {
        Vector3 dir = s1.gameObject.transform.position - this.transform.position;
        dir.Normalize();

        Vector3 force = dir * Mathf.Sqrt(_G * s1._rb.mass / Vector3.Distance(this.transform.position, s1.gameObject.transform.position));
        return force;
    }

    // This function is being added to stimulate the circular orbit of the planet
    public void TangentForce()
    {
        for (int i = 0; i < s_allObjects.Count; i++)
        {
            s_allObjects[i]._rb.velocity = new Vector3(- s_allObjects[i]._rb.velocity.z, s_allObjects[i]._rb.velocity.y, s_allObjects[i]._rb.velocity.x);
            s_allObjects[i].oldVelocity = new Vector3(-s_allObjects[i].oldVelocity.z, s_allObjects[i].oldVelocity.y, s_allObjects[i].oldVelocity.x);
        }
    }
}
