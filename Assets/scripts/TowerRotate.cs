using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRotate : MonoBehaviour
{
    public FreeCameraEntity camera;

    const float ANGULAR_VEL=30f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(camera.hook_adjust)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                this.transform.Rotate(0, 0, ANGULAR_VEL * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.transform.Rotate(0, 0, -ANGULAR_VEL * Time.deltaTime);
            }
        }
       
    }
}
