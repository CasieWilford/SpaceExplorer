using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 20f;
    public float rotationSpeed = 20f;

    public GameObject particles;
    public GameObject frontParticles;
    public GameObject shootParticles;

    public GameObject bodyPart1;
    public GameObject bodyPart2;
    public GameObject bodyPart3;
    public GameObject bodyPart4;
    public GameObject bodyPart5;

    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;

    public GameObject titleScreen;

    public GameObject bulletHolder;

    public GameObject completeGame;

    public Material greenColor;
    public Material originalColor;
    public Material redColor;

    bool isLarge = false;
    public float enlarge = 1.5f;
    public float durationOfEnlargment = 5f;

    bool speedUp = false;
    public float durationOfSpeed = 5f;
    public GameObject speedTrail;

    public static int deathValue = 0;
    public Text score;

    float timerEnlarge = 0;
    float timerSpeed = 0;

    bool winEnlarge = false;
    bool winSpeed = false;
    bool winShoot = false;


    // Start is called before the first frame update
    void Start()
    {
        particles.SetActive(false);
        frontParticles.SetActive(false);
        shootParticles.SetActive(false);

        speedTrail.SetActive(false);

        completeGame.SetActive(false);

        titleScreen.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            titleScreen.SetActive(false);
        }

        // Moves ship forward with back particles.
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, speed));
            particles.SetActive(true);
        }
        else
            particles.SetActive(false);

        // Moves ship backwards with front particles.
        if (Input.GetKey(KeyCode.S))
        {
            gameObject.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, -speed));
            frontParticles.SetActive(true);
        }
        else
            frontParticles.SetActive(false);

        // Turns Space Ship Right.
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.GetComponent<Rigidbody>().AddRelativeTorque(new Vector3(0, rotationSpeed, 0));

        }

        // Turn Space Ship Left.
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.GetComponent<Rigidbody>().AddRelativeTorque(new Vector3(0, -rotationSpeed, 0));

        }

        // Exits Program.
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (isLarge)
        {
            if(timerEnlarge < durationOfEnlargment)
                timerEnlarge += Time.deltaTime;
            else
            {
                isLarge = false;
                timerEnlarge = 0;

                gameObject.GetComponent<MeshRenderer>().material = originalColor;
                bodyPart1.GetComponentInChildren<MeshRenderer>().material = originalColor;
                bodyPart2.GetComponentInChildren<MeshRenderer>().material = originalColor;
                bodyPart3.GetComponentInChildren<MeshRenderer>().material = originalColor;
                bodyPart4.GetComponentInChildren<MeshRenderer>().material = originalColor;
                bodyPart5.GetComponentInChildren<MeshRenderer>().material = originalColor;

                // Get current scale
                Vector3 currScale = gameObject.transform.localScale;

                // Return scale back to normal size
                gameObject.transform.localScale = new Vector3(currScale.x / enlarge, currScale.y, currScale.z / enlarge);
            }
        }

        if (speedUp)
        {
            if (timerSpeed < durationOfSpeed)
                timerSpeed += Time.deltaTime;
            else
            {
                speedTrail.SetActive(false);
                speedUp = false;
                timerSpeed = 0;

                speed = 20f;
            }
        }

        // Display Death Score.
        score.text = "Deaths: " + deathValue;

        // Win Screen.
        if (winEnlarge == true && winSpeed == true && winShoot == true && enemy1 == null && enemy2 == null && enemy3 == null && enemy4 == null)
        {
            completeGame.SetActive(true);

        }
    }
    

    // Resets player back to (0, 0, 0) after hitting a Kill Volume.
    private void OnCollisionEnter(Collision collision)
    {
        Quaternion resetRotation = new Quaternion();
        resetRotation.x = 0;
        resetRotation.y = 0;
        resetRotation.z = 0;

        if (collision.gameObject.name.Equals("Kill Volume") || collision.transform.tag == "enemy")
        {
            gameObject.transform.position = Vector3.zero;
            gameObject.transform.rotation = resetRotation;

            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            // If enlarge powerup is on, return back to regular size.
            if (isLarge)
            {
                Vector3 currScale = gameObject.transform.localScale;
                gameObject.transform.localScale = new Vector3(currScale.x / enlarge, currScale.y, currScale.z / enlarge);
            }

            // Turns off power ups.
            isLarge = false;
            speedUp = false;

            // Returns speed to normal and turn off trail.
            speed = 20f;
            speedTrail.SetActive(false);

            gameObject.GetComponent<MeshRenderer>().material = originalColor;
            bodyPart1.GetComponentInChildren<MeshRenderer>().material = originalColor;
            bodyPart2.GetComponentInChildren<MeshRenderer>().material = originalColor;
            bodyPart3.GetComponentInChildren<MeshRenderer>().material = originalColor;
            bodyPart4.GetComponentInChildren<MeshRenderer>().material = originalColor;
            bodyPart5.GetComponentInChildren<MeshRenderer>().material = originalColor;

            shootParticles.SetActive(false);
            particles.SetActive(true);

            // Adds to death score.
            ++deathValue;

            winEnlarge = false;
            winSpeed = false;
            winShoot = false;
        }

        if (collision.gameObject.name.Equals("EnlargePowerUp"))
        {
            gameObject.GetComponent<MeshRenderer>().material = greenColor;
            bodyPart1.GetComponentInChildren<MeshRenderer>().material = greenColor;
            bodyPart2.GetComponentInChildren<MeshRenderer>().material = greenColor;
            bodyPart3.GetComponentInChildren<MeshRenderer>().material = greenColor;
            bodyPart4.GetComponentInChildren<MeshRenderer>().material = greenColor;
            bodyPart5.GetComponentInChildren<MeshRenderer>().material = greenColor;
            // Get current scale
            Vector3 currScale = gameObject.transform.localScale;

            // Enlarge the scale by a factor of 50%
            gameObject.transform.localScale = new Vector3(currScale.x * enlarge, currScale.y, currScale.z * enlarge);

            // Set true to variable
            isLarge = true;

            winEnlarge = true;
        }

        if (collision.gameObject.name.Equals("SpeedPowerUp"))
        {
            speed = 40f;

            speedTrail.SetActive(true);
            speedUp = true;

            winSpeed = true;
        }

        if (collision.gameObject.name.Equals("BulletPowerUp"))
        {
            bulletHolder.GetComponent<ShootingMechanic>().setShootEnabled();

            winShoot = true;

            bodyPart4.GetComponentInChildren<MeshRenderer>().material = redColor;
            bodyPart5.GetComponentInChildren<MeshRenderer>().material = redColor;

            shootParticles.SetActive(true);
        }
    }
}
