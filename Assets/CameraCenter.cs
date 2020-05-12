using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCenter : MonoBehaviour
{
    [SerializeField] Camera[] m_Cameras = new Camera[3];
    Vector3 m_MousePosition;

    float m_HorizontalAngle = 0;
    float m_VerticalAngle = 0;
    int m_SelectedIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        SelectCamera(m_SelectedIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            NextCamera();
            m_MousePosition = Input.mousePosition;
        }

        if (m_SelectedIndex == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                
            }
            else if (Input.GetMouseButton(0))
            {
                

            }

            Vector3 mouseDeltaPosition = m_MousePosition - Input.mousePosition;

            m_HorizontalAngle = Mathf.Clamp(m_HorizontalAngle - mouseDeltaPosition.x * 0.1f, -30f, 30f);
            //m_HorizontalAngle -= mouseDeltaPosition.x * 0.1f;
            m_VerticalAngle = Mathf.Clamp(m_VerticalAngle + mouseDeltaPosition.y * 0.1f, -20f, 50f);

            m_Cameras[m_SelectedIndex].transform.localEulerAngles = new Vector3(m_VerticalAngle + 3, m_HorizontalAngle + 90, 0f);

            m_MousePosition = Input.mousePosition;
        }
    }

    void NextCamera()
    {
        m_SelectedIndex++;
        if (m_SelectedIndex >= m_Cameras.Length)
        {
            m_SelectedIndex = 0;
        }
        SelectCamera(m_SelectedIndex);
    }

    void SelectCamera(int index)
    {
        for (int i = 0; i < m_Cameras.Length; i++)
        {
            m_Cameras[i].enabled = i == index;
        }
    }
}
