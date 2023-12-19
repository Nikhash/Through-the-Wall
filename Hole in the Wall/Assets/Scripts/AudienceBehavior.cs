using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudienceBehavior : MonoBehaviour
{
    private float defaultPositionX;
    private float defaultPositionY;
    private float defaultPositionZ;
    private float dividerNumber;

    // Start is called before the first frame update
    void Start()
    {
        dividerNumber = Random.Range(10f, 15f);
        defaultPositionX = transform.localPosition.x;
        defaultPositionY = transform.localPosition.y-1;
        defaultPositionZ = transform.localPosition.z;
        InvokeRepeating("Call_AudienceJump", Random.Range(10f, 50f)/ dividerNumber, Random.Range(10f, 50f)/ dividerNumber);
    }

    void Call_AudienceJump()
    {
        StartCoroutine(AudienceJump());
        dividerNumber = Random.Range(10f, 15f);
    }

    IEnumerator AudienceJump()
    {
        transform.position = new(defaultPositionX, defaultPositionY + 0.1f, defaultPositionZ);
        yield return new WaitForSeconds(0.1f);
        transform.position = new(defaultPositionX, defaultPositionY, defaultPositionZ);
    }
}
