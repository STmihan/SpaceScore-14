using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject GFX;
    public GameObject PointToRotate;
    public float MaxForce;
    [Space]
    public float SpeedRotation;

    public float ForceSpeed { get; set; }
    
    
    private bool isCapture { get; set; }
    private bool isTeleporting;
    public bool isDead { get; set; }

    public float Force;
    
    public float maxTimer;
    private float curTimer;
    private void Start()
    {
        isCapture = true;
        curTimer = maxTimer;
        isDead = false;
    }


    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Mathf.Clamp(Force, 0, MaxForce);
            Force += Time.fixedDeltaTime * ForceSpeed;
        }
        if (isCapture)
        {
            Capture();
        }
        if (!isCapture)
        {
            if (curTimer > 0)
                curTimer -= Time.fixedDeltaTime;
            else 
                Death();
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Mathf.Clamp(Force, 0, MaxForce);
            Force -= Time.fixedDeltaTime * ForceSpeed;
            GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * Force,ForceMode2D.Impulse);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Star"))
        {
            PointToRotate.transform.position = other.transform.position;
            isCapture = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Star"))
        {
            PointToRotate.transform.position = transform.position;
            isCapture = false;
        }
    }

    void Capture()
    {
        transform.RotateAround(PointToRotate.transform.position, Vector3.forward, Time.deltaTime*SpeedRotation);
        Vector2 dir = PointToRotate.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 180f;
        transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("InnerStar"))
        {
            Death();
        }
    }

    void Death()
    {
        Time.timeScale = 0;
        Debug.Log("Game over");
        isDead = true;
    }
}
