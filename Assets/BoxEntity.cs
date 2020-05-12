using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class BoxEntity : MonoBehaviour
{
    public BoxCollider areaCollider;
    [SerializeField] BoxCollider boxCollider;
    [SerializeField] Text task2Text;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (areaCollider.bounds.Intersects(boxCollider.bounds))
        {
            if (areaCollider.bounds.Contains(boxCollider.bounds.max)
                && areaCollider.bounds.Contains(boxCollider.bounds.min))
            {
                task2Text.color = new Color(0.128204f, 0.6320754f, 0.2127042f);
            }
            else
            {
            }
        }
    }
}
