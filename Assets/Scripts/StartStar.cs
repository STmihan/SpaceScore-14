using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStar : MonoBehaviour
{
    public GameObject[] Sprites;

    void Start()
    {
        Instantiate(Sprites[Random.Range(0, Sprites.Length)]);
    }
}
