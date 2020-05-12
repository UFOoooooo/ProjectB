using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrolleyMotion : MonoBehaviour
{
    const float MOVE_SPEED = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (this.transform.localPosition.x <65)
            {
                this.transform.Translate(MOVE_SPEED * Time.deltaTime, 0, 0);
            }
            else
            {
                Vector3 position = new Vector3(65, 0, 0);
                this.transform.localPosition = position;
            }
            
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (this.transform.localPosition.x > 0)
            {
                this.transform.Translate(-MOVE_SPEED * Time.deltaTime, 0, 0);
            }
            else
            {
                Vector3 position = new Vector3(0, 0, 0);
                this.transform.localPosition = position;
            }
            
        }
    }
}
