using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNoPhys : MonoBehaviour
{
    public GameObject GFX;
    public Transform PointToRotate;
    public GameObject NextPoint;
    public Transform Max;
    public float SpeedRotation;

    [Space] [Header("Line")] 
    public LineRenderer LineRenderer;
    public float LineSpeed;
    public bool isCapture;

    private Rigidbody2D _rigidbody2D;
    private Camera _camera;

    private void Start()
    {
        isCapture = false;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
    }

    private void FixedUpdate()
    {
        LineRenderer.SetPosition(0, GFX.transform.localPosition);
        if (Input.GetKey(KeyCode.Space))
        {
            LineRenderer.enabled = true;
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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            // transform.position = NextPoint.transform.position;
            // NextPoint.transform.position = transform.position;
            LineRenderer.enabled = false;
            isCapture = false;
            PointToRotate = transform;
            _camera.transform.position = new Vector3(
                0,
                GFX.transform.position.y + 6f,
                -10f
            );
        }
        if (isCapture)
        {
            Capture();
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, Max.transform.position, 2f*Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Star"))
        {
            PointToRotate = other.transform;
            isCapture = true;
        }
    }

    void Capture()
    {
        transform.RotateAround(PointToRotate.transform.position, Vector3.forward, Time.deltaTime*SpeedRotation);
        Vector2 dir = PointToRotate.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 180f;
        transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));
    }
}
