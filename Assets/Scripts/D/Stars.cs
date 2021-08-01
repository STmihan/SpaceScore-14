using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Stars : MonoBehaviour
{
    GameObject player;
    Joint2D join;
    float timerConnect = 1f;
   

    void Start()
    {
        join = GetComponent<Joint2D>();
    }
     void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && timerConnect>=1f && join.connectedBody==null )
        {
            player = collision.gameObject;          
            join.connectedBody = player.GetComponent<Rigidbody2D>();
        }        
    }
    void FixedUpdate()
    {
        if (timerConnect<2f) timerConnect+= Time.deltaTime;
        ExitConnect();


        if (join.connectedBody != null)
        {
            Vector3 dir = player.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg ;
            player.transform.RotateAround(transform.position, Vector3.forward, 360f * Time.fixedDeltaTime);
        }
        

    }
    void ExitConnect() 
    {
        if (Input.GetMouseButtonDown(1) && join.connectedBody != null)
        {
            join.connectedBody = null;
            timerConnect = 0f;
        }
    }
}
