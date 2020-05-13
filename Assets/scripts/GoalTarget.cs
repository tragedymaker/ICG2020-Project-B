using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTarget : MonoBehaviour
{
    public FlyingHook hook;
    float time = 0f;
    public bool oncar=false;
    public bool PutToCar { get { return oncar; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Time.time);
    }

    void OnTriggerEnter(Collider other)
    {
        time = Time.time;
    }


    void OnTriggerStay(Collider other)
    {
        if (Time.time - time > 3 && !hook.cable)
            oncar = true;
       
    }
    void OnTriggerExit(Collider other)
    {
        oncar = false;
    }
}
