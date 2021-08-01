using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Stars : MonoBehaviour
{
    PlayerController playerController;
    GameObject player;
    Joint2D join;
    float timerConnect = Mathf.Clamp(1f,0f,1f);
    float angle;

    void Start()
    {
        join = GetComponent<Joint2D>();
    }
     void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && timerConnect>=1f && join.connectedBody==null )
        {
            player = collision.gameObject;
            playerController = player.GetComponent<PlayerController>();
            join.connectedBody = player.GetComponent<Rigidbody2D>();

        }        
    }
    void FixedUpdate()
    {
        if (timerConnect<2f) timerConnect+= Time.deltaTime;        
        if (join.connectedBody != null)
        {
            playerController.Force();
            Vector3 dir = player.transform.position - transform.position;
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;          
            Debug.Log(angle);
            Slerp();
            
        }

        ExitConnect();
    }
    void ExitConnect() 
    {
        if (Input.GetMouseButtonDown(1) && join.connectedBody != null)
        {
            join.connectedBody = null;
            timerConnect = 0f;
            playerController.Impulse();

            Slerp();
            
        }
    }
    void Slerp()
    {
        player.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle),Time.deltaTime+1);
    }
}
