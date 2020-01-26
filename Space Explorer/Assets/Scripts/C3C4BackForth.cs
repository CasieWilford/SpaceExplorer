using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C3C4BackForth : MonoBehaviour
{
    public float speed = 1f;
    float xCenter = 20f;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(xCenter + Mathf.PingPong(Time.time * speed, 2), transform.position.y, transform.position.z);
    }
}
