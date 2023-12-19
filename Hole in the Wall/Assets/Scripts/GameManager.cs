using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;
    private GameObject mainCam;
    private GameObject fallCam;
    private GameObject audienceStrafe;
    private GameObject characterStrafe;
    private GameObject startBlank;
    private Camera mainCameraComponent;
    private Camera fallCameraComponent;
    private Camera audienceCameraComponent;
    private Camera characterCameraComponent;
    private Camera startBlankCameraComponent;
    private GameObject audiencePlayer;
    private AudioSource audienceClap;
    
    private bool paused;
    private bool dead;
    private bool startSceneAudience;
    private bool startSceneCharacter;
    private bool startSceneBlank;

    public bool debugToggle;

    // Start is called before the first frame update
    void Start()
    {
        debugToggle = true;

        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();

        audiencePlayer = GameObject.Find("Audience Clap");
        audienceClap = audiencePlayer.GetComponent<AudioSource>();

        mainCam = GameObject.Find("Main Cam");
        mainCameraComponent = mainCam.GetComponent<Camera>();

        fallCam = GameObject.Find("Fall Cam");
        fallCameraComponent = fallCam.GetComponent<Camera>();

        audienceStrafe = GameObject.Find("Audience Strafe Camera");
        audienceCameraComponent = audienceStrafe.GetComponent<Camera>();

        characterStrafe = GameObject.Find("Character Strafe Camera");
        characterCameraComponent = characterStrafe.GetComponent<Camera>();

        startBlank = GameObject.Find("Start Blank");
        startBlankCameraComponent = startBlank.GetComponent<Camera>();
        
        mainCameraComponent.enabled = true;
        fallCameraComponent.enabled = false;
        audienceCameraComponent.enabled = false;
        characterCameraComponent.enabled = false;
        startBlankCameraComponent.enabled = false;

        StartCoroutine(playerController.StartScene());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dead = player.GetComponent<PlayerController>().dead;
        startSceneAudience = playerController.startSceneAudience;
        startSceneCharacter = playerController.startSceneCharacter;
        startSceneBlank = playerController.startSceneBlank;
        
        if (debugToggle == false)
        {
            if (startSceneBlank)
            {
                mainCameraComponent.enabled = false;
                fallCameraComponent.enabled = false;
                audienceCameraComponent.enabled = false;
                characterCameraComponent.enabled = false;
                startBlankCameraComponent.enabled = true;
            }
            else if (startSceneAudience)
            {
                mainCameraComponent.enabled = false;
                fallCameraComponent.enabled = false;
                audienceCameraComponent.enabled = true;
                characterCameraComponent.enabled = false;
                startBlankCameraComponent.enabled = false;
            }
            else if (startSceneCharacter)
            {
                mainCameraComponent.enabled = false;
                fallCameraComponent.enabled = false;
                audienceCameraComponent.enabled = false;
                characterCameraComponent.enabled = true;
                startBlankCameraComponent.enabled = false;
            }
            else if (dead && !startSceneAudience)
            {
                mainCameraComponent.enabled = false;
                fallCameraComponent.enabled = true;
                audienceCameraComponent.enabled = false;
                characterCameraComponent.enabled = false;
                startBlankCameraComponent.enabled = false;
            }
            else if (!dead && !startSceneAudience)
            {
                mainCameraComponent.enabled = true;
                fallCameraComponent.enabled = false;
                audienceCameraComponent.enabled = false;
                characterCameraComponent.enabled = false;
                startBlankCameraComponent.enabled = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        else if (Input.GetKeyDown(KeyCode.P) && !paused)
        {
            Time.timeScale = 0;
            paused = true;
            audienceClap.Pause();
        }
        else if (Input.GetKeyDown(KeyCode.P) && paused)
        {
            Time.timeScale = 1;
            paused = false;
            audienceClap.UnPause();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Hole in the Wall");
        }
    }
}
