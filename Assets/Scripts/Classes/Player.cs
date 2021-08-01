using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject GFX;
    public GameObject PointToRotate;
    public GameObject NextPoint;
    public Transform Max;
    [Space]
    public float SpeedRotation;
    [Space]
    public LineRenderer LineRenderer;
    
    
    private bool isCapture { get; set; }
    private bool isTeleporting;
    public bool isDead { get; set; }
    
    public float LineSpeed { get; set; }
    
    public float maxTimer;
    private float curTimer;
    private void Start()
    {
        LineRenderer.enabled = false;
        isCapture = true;
        curTimer = maxTimer;
        isDead = false;
    }


    private void FixedUpdate()
    {
        LineRenderer.SetPosition(0, GFX.transform.localPosition);
        if (Input.GetKey(KeyCode.Space))
        {
            if (!isTeleporting) LineRenderer.enabled = true;
            NextPoint.transform.position = Vector2.MoveTowards(
            NextPoint.transform.position,
            Max.position,
            LineSpeed*Time.fixedDeltaTime);
        }
        else
        {
            LineRenderer.enabled = true;
            NextPoint.transform.position = Vector2.MoveTowards(
            NextPoint.transform.position,
            transform.position,
            LineSpeed * Time.fixedDeltaTime);
        }
        LineRenderer.SetPosition(1, NextPoint.transform.localPosition);
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
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Teleporting());
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

    IEnumerator Teleporting()
    {
        GFX.GetComponent<SpriteRenderer>().enabled = false;
        transform.position = NextPoint.transform.position;
        NextPoint.transform.position = transform.position;
        LineRenderer.enabled = false;
        isTeleporting = true;
        yield return new WaitForSeconds(0.5f);
        GFX.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.25f);
        isTeleporting = false;

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