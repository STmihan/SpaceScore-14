using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    public float force;    
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();        
    }

    void FixedUpdate()
    {
        Move();
    }
    public void Force()
    {
        rb.AddRelativeForce(transform.up * force, ForceMode2D.Force);
    }
    public void Impulse()
    {
        rb.AddForce(transform.up * force * 10, ForceMode2D.Impulse);
    }

    void Move() 
    {
        float move = Input.GetAxis("Horizontal");
        if (rb.velocity.x < 0.8f && rb.velocity.y < 0.8f)
        {            
            rb.AddTorque(move);
            if (Input.GetMouseButtonDown(0)) Impulse();
        }
    }
}
