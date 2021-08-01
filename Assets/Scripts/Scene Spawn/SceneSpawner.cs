using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


public class SceneSpawner : MonoBehaviour
{
    public GameObject Player;
    
    [Space][Header("Stars")]
    public StarBlock StarBlockPrefab;
    public StarBlock StartStarBlock;
    public float RangeBetweenStarBlocks;
    public Star[] Stars;
    public GameObject parent;
    
    private List<StarBlock> PlacedStarBlocks = new List<StarBlock>();
    private List<GameObject> placedStars = new List<GameObject>();

    [Space][Header("Background")]
    public Background BackgroundPrefab;
    public Background StartBackground;
    
    private List<Background> PlacedBG = new List<Background>();

    private void Start()
    {
        PlacedStarBlocks.Add(StartStarBlock);
        PlacedBG.Add(StartBackground);
    }

    private void Update()
    {
        BGSpawn(15f);
        StarSpawn();
    }
    
    public void StarSpawn()
    {
        if (Player.transform.position.y >= PlacedStarBlocks[PlacedStarBlocks.Count - 1].transform.position.y)
        {
            var item = Instantiate(StarBlockPrefab);
            item.transform.position = new Vector2(
                0,
                PlacedStarBlocks[PlacedStarBlocks.Count-1].transform.position.y + RangeBetweenStarBlocks
                );
            PlacedStarBlocks.Add(item);
            var box2d = item.GetComponents<BoxCollider2D>();
            foreach (var VARIABLE in box2d)
            {
                GameObject star = Instantiate(Stars[Random.Range(0, Stars.Length)].gameObject, parent.transform);
                star.transform.position = new Vector2(
                    Random.Range(VARIABLE.bounds.min.x, VARIABLE.bounds.max.x),
                    PlacedStarBlocks[PlacedStarBlocks.Count-1].transform.position.y + Random.Range(VARIABLE.bounds.min.y, VARIABLE.bounds.max.y)
                );
                placedStars.Add(star);
                if (placedStars.Count > 20)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Destroy(placedStars[i]);
                        placedStars.Remove(placedStars[i]);
                    }
                }
            }
            if (PlacedStarBlocks.Count > 2)
            {
                Destroy(PlacedStarBlocks[0].gameObject);
                PlacedStarBlocks.Remove(PlacedStarBlocks[0]);
            }
        }
    }
    
    public void BGSpawn(float BGPlayerPosOffset)
    {
        if (Player.transform.position.y >= PlacedBG[PlacedBG.Count - 1].StartPos.transform.position.y - BGPlayerPosOffset)
        {
            var item = Instantiate(BackgroundPrefab);
            item.transform.position = new Vector2(0,
                PlacedBG[0].StartPos.transform.position.y - item.EndPos.transform.position.y);
            PlacedBG.Add(item);
            if (PlacedBG.Count > 2)
            {
                Destroy(PlacedBG[0].gameObject);
                PlacedBG.Remove(PlacedBG[0]);
            }
        }
    }
}
