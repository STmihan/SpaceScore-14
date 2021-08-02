using System;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public float LineSpeed;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().LineSpeed = LineSpeed;
           
        }
    }
}
