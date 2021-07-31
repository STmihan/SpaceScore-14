using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    GameObject player;
    Joint2D join;
    float timerConnect = 2f;
    PlayerController playerController;

    void Start()
    {
        join = GetComponent<Joint2D>();
    }
     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && timerConnect>=2f && join.connectedBody==null)
        {
            player = collision.gameObject;          
            join.connectedBody = player.GetComponent<Rigidbody2D>();
        }        
    }
    void Update()
    {
        if (timerConnect<2f) timerConnect+= Time.deltaTime;
        
        if (Input.GetMouseButtonDown(1) && join.connectedBody!= null) 
        {
            join.connectedBody=null;
            timerConnect = 0f;
        }
        if (join.connectedBody != null)
        {
            player.GetComponent<PlayerController>().Force();
            Vector3 dir = player.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg ;
            player.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), 10);

        }
        

    }

}
