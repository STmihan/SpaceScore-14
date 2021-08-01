using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float BlackHoleSpeed;
    public Transform UP;
    void FixedUpdate()
    {
        float speed = Time.fixedDeltaTime*BlackHoleSpeed*Time.time;
        transform.position = Vector3.MoveTowards(transform.position, UP.position , speed/1000f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetComponent<Player>())
        {
            other.GetComponent<Player>().Death();
            
        }
        Destroy(other.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(other.gameObject);
    }
}
