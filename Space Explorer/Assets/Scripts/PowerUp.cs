using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    bool isGrabbable = true;
    public GameObject particles;
    float timer = 0;
    public float duration = 10f;
    public GameObject particleBurst;
    

    // Update is called once per frame
    void Update()
    {
        if(!isGrabbable)
        {
            if(timer < duration)
                timer += Time.deltaTime;
            else
            {
                // Make visible
                gameObject.GetComponent<MeshRenderer>().enabled = true;
                gameObject.GetComponent<Collider>().enabled = true;
                particles.SetActive(true);
                particleBurst.SetActive(false);
                timer = 0;

                isGrabbable = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("Space Ship"))
        {
            if (isGrabbable)
            {
                // Make invisible
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                gameObject.GetComponent<Collider>().enabled = false;
                particles.SetActive(false);
                isGrabbable = false;
                particleBurst.SetActive(true);
            }
        }
    }
}
