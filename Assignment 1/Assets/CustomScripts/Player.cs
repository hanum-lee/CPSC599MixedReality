using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator anima;
    public bool aniplaying;
    private GameObject target;
    private Rigidbody rigid;
    private int position;

    
    void Start()
    {
        anima = GetComponent<Animator>();
        target = GameObject.Find("Target");
        rigid = GetComponent<Rigidbody>();
        position = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
        if(position == 0)
        {
            if (gameObject.transform.position.z > -2)
            {
                aniplaying = true;
                gameObject.transform.Translate(Vector3.forward * Time.deltaTime);
                //anima.Play("WALK00_F", -1,0.0f);
            }
            else
            {
                aniplaying = false;
                //Debug.Log("stoped");
                //anima.Play("POSE_02", -1, 0.0f);
                position = 1;

            }
            anima.SetBool("IsWalking", aniplaying);
        }
        else if(position == 1)
        {
            if (gameObject.transform.position.x > 2)
            {

            }
            else
            {

            }
        }
        
        
        
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Target")
        {
            Debug.Log("cube hit");
            anima.SetBool("IsWalking", false);
            anima.Play("DAMAGED00", -1, .0f);
        }
    }



}
