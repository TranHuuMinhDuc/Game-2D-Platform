using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingBackGround : MonoBehaviour
{
    private float startPos, length;
    public GameObject cam;
    public float loopingEffect;
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = cam.transform.position.x * loopingEffect;
        float movement = cam.transform.position.x * (1 - loopingEffect);
        transform.position = new Vector3 (startPos + distance, transform.position.y, transform.position.z);

        if(movement > startPos + length)
        {
            startPos += length;
        }
        else if(movement < startPos - length)
        {
            startPos -= length;
        }
        
    }
}
