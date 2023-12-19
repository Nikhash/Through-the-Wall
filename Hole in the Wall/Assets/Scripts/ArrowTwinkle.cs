using System.Collections;
using UnityEngine;

public class ArrowTwinkle : MonoBehaviour
{

    public Material ArrowNormal;
    public Material ArrowHighlight;
    private GameObject arrowBack;
    private GameObject arrowRight;
    private GameObject arrowFront;
    private MeshRenderer meshRenderer;
    private bool move = true;

    // Start is called before the first frame update
    void Start()
    {
        arrowBack = GameObject.Find("Back Trigger");
        arrowRight = GameObject.Find("Right Trigger");
        arrowFront = GameObject.Find("Front Trigger");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (move)
        {
        transform.Translate(6.5f * Vector3.right);
        move = false;
        StartCoroutine(ArrowMoveBuffer());
        }

        // Right Trigger
        if (arrowRight.transform.position.z > 12)
        {
            transform.position = new(-12.5f, 5, -11);
        }
        // Back Trigger
        else if (arrowBack.transform.position.x > 11)
        {
            transform.position = new(-6.5f, 5, -17f);
        }
        // Front Trigger
        else if (arrowFront.transform.position.x > 11)
        {
            transform.position = new(-6.5f, 5, 13.2f);
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            meshRenderer = other.gameObject.GetComponent<MeshRenderer>();
            meshRenderer.material = ArrowHighlight;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            meshRenderer = other.gameObject.GetComponent<MeshRenderer>();
            meshRenderer.material = ArrowNormal;
        }
    }

    IEnumerator ArrowMoveBuffer()
    {
        yield return new WaitForSeconds(0.75f);
        move = true;
    }
}
