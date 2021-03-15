using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Gravitation : MonoBehaviour
{
    public static List<Gravitation> s_allObjects = new List<Gravitation>();
    private static int s_massMultiplier = 24;

    private float _G = 6.67408f;

    private Rigidbody _rb;
    
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
        for (int i = 0; i < s_allObjects.Count; i++)
        {
            if (s_allObjects[i] != this)
            {
                Vector3 dir = s_allObjects[i].gameObject.transform.position - this.transform.position;
                dir.Normalize();

                Vector3 force = dir * Mathf.Sqrt(_G * s_allObjects[i]._rb.mass / Vector3.Distance(this.transform.position, s_allObjects[i].gameObject.transform.position));

                _rb.AddForce(force);
                _rb.AddForce(new Vector3(-force.z, force.y, force.x));

            }
        }
    }

    // This function is being added to stimulate the circular orbit of the planet
    public void TangentForce()
    {
        for (int i = 0; i < s_allObjects.Count; i++)
        {
            s_allObjects[i]._rb.velocity = new Vector3(- s_allObjects[i]._rb.velocity.z, s_allObjects[i]._rb.velocity.y, s_allObjects[i]._rb.velocity.x);
        }
    }
}
