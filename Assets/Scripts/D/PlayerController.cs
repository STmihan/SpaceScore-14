using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    public float force;    
    public Joint2D join;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        join = GetComponent<Joint2D>();
    }


    void Update()
    {     

        if (Input.GetMouseButtonDown(0)) rb.AddForce(transform.up * force*10, ForceMode2D.Impulse);    //удалить
     

    }
    public void Force()
    {
        rb.AddForce(transform.up * force, ForceMode2D.Force);
    }
 
}
