using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingMechanic : MonoBehaviour
{
    bool shootBullet = false;
    float timerShoot = 0f;
    float durationOfShoot = 7f;

    public GameObject Bullet;
    float bulletSpeed = 1100f;

    public Material originalColor;

    public GameObject bodyPart1;
    public GameObject bodyPart2;

    public GameObject shootingParticles;
    public GameObject backParticles;

    public void setShootEnabled()
    {
        shootBullet = true;
    }

    void Fire()
    {
        // Shoots bullet.
        GameObject tempBullet = Instantiate(Bullet, transform.position, transform.rotation) as GameObject;
        Rigidbody tempRigidBodyBullet = tempBullet.GetComponent<Rigidbody>();

        tempRigidBodyBullet.AddForce(tempRigidBodyBullet.transform.forward * bulletSpeed);
        Destroy(tempBullet, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (shootBullet)
        {
            if (timerShoot < durationOfShoot)
            {
                timerShoot += Time.deltaTime;

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Fire();
                }
            }
            else
            {
                shootBullet = false;
                timerShoot = 0f;

                bodyPart1.GetComponentInChildren<MeshRenderer>().material = originalColor;
                bodyPart2.GetComponentInChildren<MeshRenderer>().material = originalColor;

                shootingParticles.SetActive(false);
                backParticles.SetActive(true);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("Kill Volume"))
        {
            shootBullet = false;
        }
    }
}
