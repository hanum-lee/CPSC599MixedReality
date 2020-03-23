using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator anima;
    public bool aniplaying;
    //private GameObject target;
    //private Rigidbody rigid;
    private int destIndex = 0;
    bool found = false;

    public List<Transform> Destinations;

    public DefaultTrackableEventHandler ScenarioImageTracker;
    private Transform myTransform;
    float moveSpeed = 0.1f;

    void Start()
    {
        //anima = getcomponent<animator>();
        //target = gameobject.find("target");
        //rigid = getcomponent<rigidbody>();
        //destIndex = 0;
        myTransform = transform;
        //Vector3 originPosition = transform.position;
        ScenarioImageTracker.OnTargetFound.AddListener(TurnMoveToDestinationsOn);
        ScenarioImageTracker.OnTargetLost.AddListener(TurnMoveToDestinationsOff);
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Found: " + found);
        Debug.Log("Position: " + destIndex);
        Debug.Log("Count: " + Destinations.Count);
        Debug.Log("Transform Position: " + transform.position);
        if (found && destIndex < Destinations.Count)
        {
            var target = Destinations[destIndex];
            Debug.Log("Target Position: " + target.position);
            float step = moveSpeed * Time.deltaTime;

            myTransform.position = Vector3.MoveTowards(myTransform.up, target.position, step);
            //float distanceToPlane = Vector3.Dot(myTransform.up, target.position - myTransform.position);
            //Vector3 pointOnPlane = target.position - (myTransform.up * distanceToPlane);

            //myTransform.LookAt(pointOnPlane, myTransform.up);
            aniplaying = true;
        }
        


        /*if(position == 0)
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
        
        
        */
        
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


    public void TurnMoveToDestinationsOn()
    {
        found = true;
    }

    public void TurnMoveToDestinationsOff()
    {
        found = false;
    }
}

