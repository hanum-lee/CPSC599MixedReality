using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : DefaultTrackableEventHandler
{
    // Start is called before the first frame update
    public GameObject uni;
    void Start()
    {
        
    }

    // Update is called once per frame
    

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        this.transform.Translate(Vector3.forward * Time.deltaTime * 0.05f);

    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();

    }


}
