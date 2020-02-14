using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : DefaultTrackableEventHandler
{
    // Start is called before the first frame update

    public Animator anima;
    public bool aniplaying;
    private GameObject target;
    private Rigidbody rigid;
    private int position;
    Vector3 originPosition;
    bool found = false;


    void Start()
    {
        anima = GetComponent<Animator>();
        target = GameObject.Find("Target");
        rigid = GetComponent<Rigidbody>();
        position = 0;
        
        Vector3 originPosition = transform.position;

    }

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        found = true;
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        found = false;

    }


    // Update is called once per frame
    void Update()
    {
        
        if(position == 0)
        {
            Debug.Log(gameObject.transform.position.z);
            if (gameObject.transform.position.z > -0.25)
            {
                aniplaying = true;
                gameObject.transform.position = (Vector3.MoveTowards(gameObject.transform.position,new Vector3(originPosition.x,originPosition.y,-2 ), Time.deltaTime * 0.05f));

                //anima.Play("WALK00_F", -1,0.0f);
            }
            else
            {
                aniplaying = false;
                Debug.Log("stoped");
                //anima.Play("POSE_02", -1, 0.0f);
                position = 1;

            }
            anima.SetBool("IsWalking", aniplaying);
        }
        else if(position == 1)
        {
            if(gameObject.transform.rotation.y > 90)
            {
                gameObject.transform.Rotate(Vector3.left * Time.deltaTime);
            }
            else
            {
                if (gameObject.transform.position.x < 0.2)
                {
                    aniplaying = true;
                    gameObject.transform.Translate(Vector3.left * Time.deltaTime * 0.05f);
                }
                else
                {
                    aniplaying = false;
                    //anima.Play("POSE_02", -1, .0f);
                    position = 2;
                }
                anima.SetBool("isWalkingL", aniplaying);
            }

        }
        else if (position == 2)
        {
            
            
            
            
            if (Vector3.Distance(gameObject.transform.position, originPosition) <= 0.001f)
            {

                anima.SetBool("IsWalking", false);
                //gameObject.transform.position = target.transform.position;
                //target.SetActive(false);
                anima.Play("DAMAGED01", -1, .0f);
                position = 3;

            }
            else
            {
                gameObject.transform.position = (Vector3.MoveTowards(gameObject.transform.position, originPosition, Time.deltaTime * 0.05f));
                anima.SetBool("IsWalking", true);
            }
            
            
        }
        
        
        
        
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Target")
        {
            Debug.Log("cube hit");
            anima.SetBool("IsWalking", false);
            anima.Play("DAMAGED01", -1, .0f);
        }
    }



}
