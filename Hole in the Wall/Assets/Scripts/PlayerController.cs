using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Input variables
    private float rotationInput;
    private float leftArmInput;
    private float leftLegInput;
    private float rightArmInput;
    private float rightLegInput;
    private float secondaryInput;

    // Primary limbs variables
    private GameObject leftShoulder;
    private GameObject leftLeg;
    private GameObject rightShoulder;
    private GameObject rightLeg;

    // Secondary limbs variables
    private GameObject leftElbow;
    private GameObject leftLegLower;
    private GameObject rightElbow;
    private GameObject rightLegLower;
    
    // Class variables
    private Rigidbody playerRb;
    public GameObject mainCam;
    private AudioSource soundPlayer;
    private GameObject deathSoundSource;
    private AudioSource deathSoundPlayer;
    public AudioClip[] deathSounds;
    public AudioClip[] music;
    private GameObject spawnManager;
    public GameManager gameManager;

    // Settings toggles
    private readonly bool triggersLogToggle = true;
    private readonly bool secondaryToggle = true;

    // Boolean variables
    public bool dead = false;
    private bool hit = false;
    private bool passed;
    public bool startSceneAudience = false;
    public bool startSceneCharacter = false;
    public bool startSceneBlank = false;
    private bool debugToggle;


    // Other variables
    private readonly float rotationSpeed = 0.005f;
    private readonly float hitForce = 200;
    private int triggers;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.Find("Main Cam");
        soundPlayer = mainCam.GetComponent<AudioSource>();
        deathSoundSource = GameObject.Find("Death Sound Source");
        deathSoundPlayer = deathSoundSource.GetComponent<AudioSource>();
        StartCoroutine(PlayMusic());

        playerRb = GetComponent<Rigidbody>();
        spawnManager = GameObject.Find("GameManager");
        gameManager = spawnManager.GetComponent<GameManager>();

        leftShoulder = GameObject.Find("Shoulder_L (collider)");
        leftLeg = GameObject.Find("UpperLeg_L (collider)");
        rightShoulder = GameObject.Find("Shoulder_R (collider)");
        rightLeg = GameObject.Find("UpperLeg_R (collider)");

        leftElbow = GameObject.Find("Elbow_L (Collider)");
        leftLegLower = GameObject.Find("LowerLeg_L (collider)");
        rightElbow = GameObject.Find("Elbow_R (collider)");
        rightLegLower = GameObject.Find("LowerLeg_R (collider)");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        debugToggle = gameManager.debugToggle;

        rotationInput = Input.GetAxis("Rotation Input");
        secondaryInput = Input.GetAxis("Secondary Input");
        leftArmInput = Input.GetAxis("Left Arm");
        leftLegInput = Input.GetAxis("Left Leg");
        rightArmInput = Input.GetAxis("Right Arm");
        rightLegInput = Input.GetAxis("Right Leg");

        // This determines character movement
        if (leftArmInput == 1)
        {
            if (secondaryInput == 1 & secondaryToggle)
            {
                Debug.Log("Left Elbow (Secondary)\n");
                leftElbow.transform.Rotate(-rotationInput * rotationSpeed * Time.deltaTime * Vector3.forward);
            }
            else
            {
                leftShoulder.transform.Rotate(-rotationInput * rotationSpeed * Time.deltaTime * Vector3.forward);
            }
        }

        if (leftLegInput == 1)
        {
            if (secondaryInput == 1 & secondaryToggle)
            {
                Debug.Log("Left Lower Leg (Secondary)\n");
                leftLegLower.transform.Rotate(-rotationInput * rotationSpeed * Time.deltaTime * Vector3.forward);
            }
            else
            {
                leftLeg.transform.Rotate(-rotationInput * rotationSpeed * Time.deltaTime * Vector3.forward);
            }
        }

        if (rightArmInput == 1)
        {
            if (secondaryInput == 1 & secondaryToggle)
            {
                Debug.Log("Right Elbow (Secondary)\n");
                rightElbow.transform.Rotate(-rotationInput * rotationSpeed * Time.deltaTime * Vector3.forward);
            }
            else
            {
                rightShoulder.transform.Rotate(-rotationInput * rotationSpeed * Time.deltaTime * Vector3.forward);
            }
        }

        if (rightLegInput == 1)
        {
            if (secondaryInput == 1 & secondaryToggle)
            {
                Debug.Log("Right Lower Leg (Secondary)\n");
                rightLegLower.transform.Rotate(-rotationInput * rotationSpeed * Time.deltaTime * Vector3.forward);
            }
            else
            {
                rightLeg.transform.Rotate(-rotationInput * rotationSpeed * Time.deltaTime * Vector3.forward);
            }
        }

        if (triggers > 9)
        {
            passed = true;
        }

        else if (triggers < 10)
        {
            passed = false;
        }

        if (dead & !hit)
        {
            playerRb.constraints = RigidbodyConstraints.None;
            playerRb.AddForce(Vector3.one * hitForce, ForceMode.Impulse);
            PlayDeathSound();
            hit = true;
            StartCoroutine(Reload(5));
        }

        if (dead)
        {
            soundPlayer.volume *= 0.99f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall Checkpoint"))
        {
            triggers += 1;

            if (triggersLogToggle)
            {
                Debug.Log(triggers + "triggers");
            }
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            Collider targetCollider = other.gameObject.GetComponent<BoxCollider>(); // Gets the wall's box collider on collision
            if (passed)
            {
                Debug.Log("Passed");
                targetCollider.enabled = false;
            }

            else if (!passed)
            {
                Debug.Log("Failed");
                dead = true;
                targetCollider.enabled = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Wall Checkpoint"))
        {
            triggers -= 1;

            if (triggersLogToggle)
            {
                Debug.Log(triggers + "triggers");
            }

            if (triggers! > 9)
            {
                passed = false;
            }
        }
    }

    void PlayDeathSound()
    {
        index = Random.Range(0, 2);
        deathSoundPlayer.clip = deathSounds[index];
        deathSoundPlayer.Play();
    }

    IEnumerator PlayMusic()
    {
        yield return new WaitForSeconds(3);
        soundPlayer.clip = music[0];
        soundPlayer.Play();
        yield return new WaitForSeconds(music[0].length);
        soundPlayer.clip = music[1];
        soundPlayer.Play();
    }

    IEnumerator Reload(float time)
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Hole in the Wall");
    }

    public IEnumerator StartScene()
    {
        startSceneBlank = true;
        yield return new WaitForSeconds(4);
        startSceneBlank = false;
        startSceneAudience = true;
        yield return new WaitForSeconds(3);
        startSceneAudience = false;
        startSceneCharacter = true;
        yield return new WaitForSeconds(2);
        startSceneCharacter = false;
    }
}
