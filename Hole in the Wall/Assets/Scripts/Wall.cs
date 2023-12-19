using UnityEngine;

public class Wall : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;
    private GameObject audienceStrafeObject;
    private GameObject characterStrafeObject;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();

        audienceStrafeObject = GameObject.Find("Audience Strafe");
        characterStrafeObject = GameObject.Find("Character Strafe");
    }

    private readonly float speed = 1f; 

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward);

        if (playerController.startSceneAudience)
        {
            audienceStrafeObject.transform.Translate(speed * 2 * Time.deltaTime * -Vector3.forward);
        }
        else if (playerController.startSceneCharacter)
        {
            characterStrafeObject.transform.Translate(speed / 3 * Time.deltaTime * Vector3.left);
        }
    }
}