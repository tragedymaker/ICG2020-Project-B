using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trolley : MonoBehaviour
{
    ConfigurableJoint m_JointForObject;


    [SerializeField] LineRenderer TrolleyCable;
    [SerializeField] GameObject m_JointBody;
    [SerializeField] GameObject Hook;
    public FreeCameraEntity camera;
    const float TRANSLATE_VEL = 1f;
    const float Move_Speed = 1f;
    float cable_len = 0;
    // Start is called before the first frame update
    void Start()
    {
        AttachOrDetachObject();
    }

    // Update is called once per frame
    void Update()
    {
        if (camera.hook_adjust)
        {
            float position = this.transform.localPosition.y;
            if (Input.GetKey(KeyCode.UpArrow) & (position > -17))
            {
                this.transform.Translate(0, 0, TRANSLATE_VEL * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.DownArrow) & (position < -1))
            {
                this.transform.Translate(0, 0, -TRANSLATE_VEL * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.Z))
            {
                cable_len = Mathf.Max(0, cable_len - Move_Speed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.C))
            {
                cable_len = Mathf.Min(cable_len + Move_Speed * Time.deltaTime, 20);
            }
            var limited = m_JointForObject.linearLimit;
            limited.limit = cable_len;
            m_JointForObject.linearLimit = limited;
            UpdateCable();
        }
        

    }


    void AttachOrDetachObject()
    {
            var joint = m_JointBody.AddComponent<ConfigurableJoint>();
            joint.xMotion = ConfigurableJointMotion.Limited;
            joint.yMotion = ConfigurableJointMotion.Limited;
            joint.zMotion = ConfigurableJointMotion.Limited;
            joint.angularXMotion = ConfigurableJointMotion.Free;
            joint.angularYMotion = ConfigurableJointMotion.Free;
            joint.angularZMotion = ConfigurableJointMotion.Free;



            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = new Vector3(0f,0.015f, 0.2f);
            joint.anchor = new Vector3(0f, 0f, 0f);

            joint.connectedBody = Hook.GetComponent<Rigidbody>();

            m_JointForObject = joint;

    }

    void UpdateCable()
    {
        TrolleyCable.SetPosition(0, this.transform.position);

        var connectedBodyTransform = m_JointForObject.connectedBody.transform;
        TrolleyCable.SetPosition(1, connectedBodyTransform.TransformPoint(m_JointForObject.connectedAnchor));
    }
}