using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject GFX;
    public GameObject PointToRotate;
    public NextPoint NextPoint;
    public Transform Max;
    [Space]
    public float SpeedRotation;
    [Space]
    public LineRenderer LineRenderer;
    public AudioSource lineSound;
    public AudioSource TPsound;
    
    private bool isCapture { get; set; }
    private bool isTeleporting;
    public bool isDead { get; set; }
    
    public float LineSpeed { get; set; }
    
    public float maxTimer;
    private float curTimer;

    
    private bool b; // Для поворота
    private void Start()
    {
        LineRenderer.enabled = false;
        isCapture = true;
        curTimer = maxTimer;
        isDead = false;
        lineSound.Play();
    }

    private void FixedUpdate()
    {
        LineRenderer.SetPosition(0, GFX.transform.localPosition);
        if (Input.GetKey(KeyCode.Space) && !isTeleporting)
        {
            NextPoint.Point.enabled = true;
            LineRenderer.enabled = true;
            NextPoint.gameObject.transform.position = Vector2.MoveTowards(
            NextPoint.gameObject.transform.position,
            Max.position,
            LineSpeed*Time.fixedDeltaTime);
        }
        else
        {
            LineRenderer.enabled = true;
            NextPoint.gameObject.transform.position = Vector2.MoveTowards(
            NextPoint.gameObject.transform.position,
            transform.position,
            LineSpeed * Time.fixedDeltaTime);
        }
        LineRenderer.SetPosition(1, NextPoint.gameObject.transform.localPosition);
        if (isCapture)
        {
            Capture(b);
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
            NextPoint.Point.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            b = !b;
        }

        if (LineRenderer.GetPosition(1) == LineRenderer.GetPosition(0))
        {
            NextPoint.Point.GetComponent<SpriteRenderer>().enabled = false;
            lineSound.mute = true;
        }
        else
        {
            NextPoint.Point.GetComponent<SpriteRenderer>().enabled = true;
            lineSound.mute = false;
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

    void Capture(bool ChangeRotateDirection)
    {
        if (ChangeRotateDirection)
        {
            transform.RotateAround(PointToRotate.transform.position, Vector3.forward, Time.deltaTime * SpeedRotation);
            Vector2 dir = PointToRotate.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 180f;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        else
        {
            transform.RotateAround(PointToRotate.transform.position, Vector3.forward, Time.deltaTime * -SpeedRotation);
            Vector2 dir = PointToRotate.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    IEnumerator Teleporting()
    {
        TPsound.PlayOneShot(TPsound.clip);
        LineRenderer.enabled = false;
        isTeleporting = true;
        NextPoint.Target.SetActive(true);
        GFX.GetComponent<SpriteRenderer>().enabled = false;
        transform.position = NextPoint.gameObject.transform.position;
        NextPoint.gameObject.transform.position = transform.position;
        yield return new WaitForSecondsRealtime(.3f);
        NextPoint.Target.GetComponent<Animator>().SetTrigger("Trigger");
        yield return new WaitForSecondsRealtime(.5f);
        NextPoint.Target.SetActive(false);
        GFX.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSecondsRealtime(.5f);
        isTeleporting = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("InnerStar"))
        {
            Death();
        }
    }

    public void Death()
    {
        Debug.Log("Game over");
        isDead = true;
    }
}
