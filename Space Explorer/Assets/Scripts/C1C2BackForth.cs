using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Back and forth motion for cube one.
public class C1C2BackForth : MonoBehaviour
{
    public float speed = 1f;
    public float zCenter = 23f;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x , transform.position.y , zCenter + Mathf.PingPong(Time.time * speed, 2));
    }
}
