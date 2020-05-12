using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HookMotion : MonoBehaviour
{
    const float ATTACH_DISTANCE = 0.3f;
    const float MOVE_SPEED = 1f;

    GameObject m_DetectedObject;
    ConfigurableJoint m_JointForObject;
    Material originDetectedObjectMaterial;
    LineRenderer m_Cable2;

    [SerializeField] GameObject trolley;
    [SerializeField] GameObject hook;
    [SerializeField] LineRenderer m_Cable1;
    [SerializeField] Material detectedMaterial;
    [SerializeField] Material attachedMaterial;
    [SerializeField] Material boardMaterial;
    [SerializeField] Text task1Text;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            if (this.transform.localPosition.y < 0)
            {
                this.transform.Translate(0, MOVE_SPEED * Time.deltaTime, 0);
            }
            else
            {
                Vector3 position = new Vector3(0, 0, 0);
                this.transform.localPosition = position;
            }

        }
        else if (Input.GetKey(KeyCode.E))
        {
            if (m_DetectedObject != null) return;
            if (m_JointForObject != null)
            {
                if (this.transform.localPosition.y < -54)
                {
                    return;
                }
            }

            if (this.transform.localPosition.y > -60)
            {
                this.transform.Translate(0, -MOVE_SPEED * Time.deltaTime, 0);
            }
            else
            {
                Vector3 position = new Vector3(0, -60, 0);
                this.transform.localPosition = position;
            }
        }

        if (m_JointForObject == null)
        {
            DetectObjects();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AttachOrDetachObject();
        }

        UpdateCable();
        UpdateCable2();
    }

    void AttachOrDetachObject()
    {
        if (m_JointForObject == null)
        {
            if (m_DetectedObject != null)
            {
                m_JointForObject = hook.GetComponent<ConfigurableJoint>();
                m_JointForObject.connectedBody = m_DetectedObject.GetComponent<Rigidbody>();

                MeshRenderer[] renderer = m_JointForObject.connectedBody.GetComponentsInChildren<MeshRenderer>();
                if (renderer != null)
                {
                    foreach (MeshRenderer meshRenderer in renderer)
                    {
                        if (meshRenderer.material.color != boardMaterial.color)
                        {
                            
                            meshRenderer.material = attachedMaterial;
                        }
                    }
                }

                task1Text.color = new Color(0.128204f, 0.6320754f, 0.2127042f);

                m_DetectedObject = null;
            }
        }
        else
        {
            if (m_JointForObject != null)
            {
                MeshRenderer[] renderer = m_JointForObject.connectedBody.GetComponentsInChildren<MeshRenderer>();
                if (renderer != null)
                {
                    foreach (MeshRenderer meshRenderer in renderer)
                    {
                        if (meshRenderer.material.color != boardMaterial.color)
                        {

                            meshRenderer.material = originDetectedObjectMaterial;
                        }
                    }
                }
                m_JointForObject.connectedBody = null;
                m_JointForObject = null;
            }
            //m_JointForObject.connectedBody.GetComponent<MeshRenderer>().material.color = Color.white;
            
        }
    }

    void DetectObjects()
    {
        Ray ray = new Ray(hook.transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, ATTACH_DISTANCE))
        {
            Debug.Log(hit.collider.gameObject.name);
            if ((hit.collider.gameObject.name == "ground" || hit.collider.gameObject.name == "Area") ||
                m_DetectedObject == hit.collider.gameObject)
            {
                return;
            }

            RecoverDetectedObject();

            MeshRenderer[] renderer = hit.collider.GetComponentsInChildren<MeshRenderer>();

            if (renderer != null)
            {
                foreach (MeshRenderer meshRenderer in renderer)
                {
                    if (meshRenderer.material.color != boardMaterial.color)
                    {
                        originDetectedObjectMaterial = meshRenderer.material;
                        meshRenderer.material = detectedMaterial;
                    }
                }

                m_DetectedObject = hit.collider.gameObject;
            }
        }
        else
        {
            RecoverDetectedObject();
        }
    }

    void RecoverDetectedObject()
    {
        if (m_DetectedObject != null)
        {
            MeshRenderer[] renderer = m_DetectedObject.GetComponentsInChildren<MeshRenderer>();

            if (renderer != null)
            {
                foreach (MeshRenderer meshRenderer in renderer)
                {
                    if (meshRenderer.material.color != boardMaterial.color)
                    {
                        meshRenderer.material = originDetectedObjectMaterial;
                    }
                }
            }
            originDetectedObjectMaterial = null;
            m_DetectedObject = null;
        }
    }

    void UpdateCable()
    {
        Vector3 hookPos = this.transform.localPosition;
        m_Cable1.SetPosition(0, new Vector3(-20f, 27f + -hookPos.y, 0));
        m_Cable1.SetPosition(3, new Vector3(-19.1f, 26.5f + -hookPos.y, 0));
        m_Cable1.SetPosition(4, new Vector3(-18.75f, 26.5f + -hookPos.y, 0));
        m_Cable1.SetPosition(7, new Vector3(-17.95f, 27f + -hookPos.y, 0));
    }

    void UpdateCable2()
    {
        if (m_JointForObject != null)
        {
            m_Cable2 = m_JointForObject.connectedBody.GetComponentInChildren<LineRenderer>();
            float lineLength = Mathf.Abs(m_JointForObject.connectedBody.transform.position.y - hook.transform.position.y)-0.2f;
            Vector3 boxPos = m_JointForObject.connectedBody.transform.localPosition;

            m_Cable2.SetPosition(3, new Vector3(0f, 0.87f+lineLength, 0f));
            m_Cable2.SetPosition(9, new Vector3(0f, 0.87f + lineLength, 0f));
        }
        else
        {
            if (m_Cable2 != null)
            {
                m_Cable2.SetPosition(3, new Vector3(0f, 0.87f, 0f));
                m_Cable2.SetPosition(9, new Vector3(0f, 0.87f, 0f));
            }
            
        }
    }

}
