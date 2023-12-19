using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private bool spawnToggle; // Change to false if you don't want walls to spawn
    public GameObject[] wallPrefabs;
    private int wallIndex;
    public float spawnTime = 10; // Wall spawning interval
    private readonly int startTime = 9; // Wall spawning start time
    private GameObject player;
    private bool dead;

    // Start is called before the first frame update
    void Start()
    {
        spawnToggle = false;

        player = GameObject.Find("Player");
        
        wallIndex = 0;
        if (spawnToggle)
        {
            InvokeRepeating(nameof(SpawnWall), startTime, spawnTime);
        }
    }

    void FixedUpdate()
    {
        dead = player.GetComponent<PlayerController>().dead;
    }

    void SpawnWall()
    {
        if (!dead)
        {
            if (wallIndex != wallPrefabs.Length - 1) // If the previous wall is not the last one, select the next wall and spawn it
            {
                wallIndex += 1;
                Instantiate(wallPrefabs[wallIndex], new(0,3.33f,0),
                wallPrefabs[wallIndex].transform.rotation);
            }

            else if (wallIndex == wallPrefabs.Length - 1) // If the previous wall is the last one, select the first wall and spawn it
            {
                wallIndex = 0;
                Instantiate(wallPrefabs[wallIndex], new(0,3.3f,0),
                wallPrefabs[wallIndex].transform.rotation);
            }
            else
            {
                Debug.Log("Error: Something went wrong");
            }
        }
    }
}
