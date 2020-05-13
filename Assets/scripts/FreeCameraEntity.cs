using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCameraEntity : MonoBehaviour
{
    float VELOCITY =3f;
    Vector3 m_MousePosition;
    Vector3 movement = Vector3.zero;
    float m_HorizontalAngle = 0;
    float m_VerticalAngle = 0;
    bool adjust = true;
    public bool hook_adjust { get { return !adjust; } }

    // Start is called before the first frame update
    void Start()
    {
        m_MousePosition = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
            adjust = !adjust;
        if (adjust)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {

                //Move Forward
                this.transform.Translate(VELOCITY * Time.deltaTime, 0, 0);
                movement += Vector3.right;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {

                //Move backward
                this.transform.Translate(-VELOCITY * Time.deltaTime, 0, 0);
                movement -= Vector3.right;
            }


            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //Parallel left

                this.transform.Translate(0, 0, VELOCITY * Time.deltaTime);
                movement += Vector3.forward;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                //Parallel right

                this.transform.Translate(0, 0, -VELOCITY * Time.deltaTime);
                movement -= Vector3.forward;
            }



            if (Input.GetKeyDown(KeyCode.E))
            {
                //Vertical up
                VELOCITY = 3f;
                this.transform.Translate(0, VELOCITY * Time.deltaTime, 0);
                movement += Vector3.up;
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                //Vertical down
                VELOCITY = -3f;
                this.transform.Translate(0, -VELOCITY * Time.deltaTime, 0);
                movement += Vector3.up;
            }




            if (Input.GetMouseButtonDown(0))
            {
                m_MousePosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                Vector3 mouseDeltaPosition = m_MousePosition - Input.mousePosition;
                m_HorizontalAngle -= mouseDeltaPosition.x * 0.1f;
                m_VerticalAngle = Mathf.Clamp(m_VerticalAngle - mouseDeltaPosition.y * 0.1f, -89f, 89f);
                this.transform.localEulerAngles = new Vector3(0f, -m_HorizontalAngle, -m_VerticalAngle);
                m_MousePosition = Input.mousePosition;
            }
        }
    }


}
