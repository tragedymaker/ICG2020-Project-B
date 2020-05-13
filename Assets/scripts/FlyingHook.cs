using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingHook : MonoBehaviour
{
    
    const float Move_Speed = 0.8f;
    const float Attach_Distance = 5f;
    const float cable_len = 6f;
    GameObject m_DetectedObject;
    ConfigurableJoint m_JointForObject;
    [SerializeField] GameObject m_JointBody;
    [SerializeField] LineRenderer m_Cable;
    public bool cable { get { return m_Cable.enabled; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (m_JointForObject == null)
        {
            DetectObjects();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AttachOrDetachObject();
        }
        UpdateCable();
    }

    void DetectObjects()
    {
        Ray ray = new Ray(this.transform.position, Vector3.down);
        RaycastHit hiiit;

        if(Physics.Raycast(ray,out hiiit,Attach_Distance))
        {

            if (m_DetectedObject == hiiit.collider.gameObject)
            {
                return;
            }
            RecoverDetectedObject();

            MeshRenderer renderer = hiiit.collider.GetComponent<MeshRenderer>();

            if (renderer != null)
            {
                renderer.material.color = Color.black;
                m_DetectedObject = hiiit.collider.gameObject;
            }
            else
            {
                RecoverDetectedObject();
            }

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AttachOrDetachObject();
        }
           UpdateCable();
    }
    

    void AttachOrDetachObject()
    {
        if (m_JointForObject == null)
        {
            if (m_DetectedObject != null)
            {
                var joint = m_JointBody.AddComponent<ConfigurableJoint>();
                joint.xMotion = ConfigurableJointMotion.Limited;
                joint.yMotion = ConfigurableJointMotion.Limited;
                joint.zMotion = ConfigurableJointMotion.Limited;
                joint.angularXMotion = ConfigurableJointMotion.Free;
                joint.angularYMotion = ConfigurableJointMotion.Free;
                joint.angularZMotion = ConfigurableJointMotion.Free;

                var limited = joint.linearLimit;
                limited.limit =cable_len;
                joint.linearLimit = limited;

                joint.autoConfigureConnectedAnchor = false;
                joint.connectedAnchor = new Vector3(0f, 0.5f, 0f);
                joint.anchor = new Vector3(0f, 0f, 0f);

                joint.connectedBody = m_DetectedObject.GetComponent<Rigidbody>();

                m_JointForObject = joint;

                m_DetectedObject.GetComponent<MeshRenderer>().material.color = Color.red;
                m_DetectedObject = null;
                
            }

        }
        else
        {
            m_JointForObject.connectedBody.GetComponent<MeshRenderer>().material.color = Color.white;
            GameObject.Destroy(m_JointForObject);
            m_JointForObject = null;
        }
    }

    void RecoverDetectedObject()
    {
        if (m_DetectedObject != null)
        {
            m_DetectedObject.GetComponent<MeshRenderer>().material.color = Color.white;
            m_DetectedObject = null;
        }
    }

    void UpdateCable()
    {
        m_Cable.enabled = m_JointForObject != null;

        if (m_Cable.enabled)
        {
            m_Cable.SetPosition(0, this.transform.position);

            var connectedBodyTransform = m_JointForObject.connectedBody.transform;
            m_Cable.SetPosition(1, connectedBodyTransform.TransformPoint(m_JointForObject.connectedAnchor));
        }
    }
    
}
