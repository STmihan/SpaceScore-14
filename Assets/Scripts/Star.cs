using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    private Vector3 scale;

    private void Start()
    {
        scale = transform.localScale;
    }

    private void OnMouseEnter()
    {
        transform.localScale *= 1.1f;
    }

    private void OnMouseExit()
    {
        transform.localScale = scale;
    }
}
