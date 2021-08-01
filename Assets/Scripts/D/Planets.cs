using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planets : MonoBehaviour
{
    HashSet<Rigidbody2D> gravityBodies = new HashSet<Rigidbody2D>();
    Rigidbody2D rb;

    public float gravityForce;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        foreach (Rigidbody2D body in gravityBodies)
        {
            Vector3 directionToPlanet = (transform.position - body.transform.position).normalized;
            float distance = (transform.position - body.transform.position).sqrMagnitude;
            float force= gravityForce*rb.mass*body.mass/(distance * distance);
            body.AddForce(directionToPlanet* force);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody != null) gravityBodies.Add(collision.attachedRigidbody);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.attachedRigidbody != null) gravityBodies.Remove(collision.attachedRigidbody);
    }
}
