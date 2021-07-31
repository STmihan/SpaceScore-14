using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStar : MonoBehaviour
{
    public Sprite[] Sprites;
    private SpriteRenderer SpriteRenderer;
    
    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer.sprite = Sprites[Random.Range(0, Sprites.Length)];
    }
}
