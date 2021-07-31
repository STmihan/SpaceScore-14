using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Line")]
    public LineRenderer LineRenderer;
    public Transform StartLinePos;
    public Transform EndLinePos;

    private void Start()
    {
        LineRenderer.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {

        }
        else
        {

        }
    }
}
