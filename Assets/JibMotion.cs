using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JibMotion : MonoBehaviour
{
    private JointMotor joint;
    const float MOVE_SPEED = 30f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            joint.force = 15;
            joint.targetVelocity = -15;
            
        }
        else if (Input.GetKey(KeyCode.D))
        {
            joint.force = joint.targetVelocity = 15;
        }
        else
        {
            joint.force = Mathf.Max(0, joint.force - MOVE_SPEED * Time.deltaTime);
            joint.targetVelocity = 0;
        }

        gameObject.GetComponent<HingeJoint>().motor = joint;
    }
}
