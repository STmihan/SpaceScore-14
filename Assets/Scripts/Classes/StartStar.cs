using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class StartStar : MonoBehaviour
{
    public GameObject[] Sprites;

    void Start()
    {
        Instantiate(Sprites[Random.Range(0, Sprites.Length)], gameObject.transform.position, quaternion.identity, this.gameObject.transform);
    }
}
