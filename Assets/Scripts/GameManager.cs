using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private int playerLives;
    public int PlayerLives { get; set; }

    private int score;
    public int Score { get; set; }

    private int spawnedDebries;
    public int SpawnedDebries { get { return spawnedDebries; } set { spawnedDebries = value; } }

    public Transform[] spawnLocs;
    public GameObject DebryStarterPrefab;
    public float maxX, maxY, currTime, nextSpawn, lastTime, maxDebry,currDebry;
	// Use this for initialization
	void Start () {
        Score = 0;
        PlayerLives = 3;
        SpawnedDebries = 0;
	}
	
	// Update is called once per frame
	void Update () {
        currDebry = SpawnedDebries;
        currTime = Time.time;
        if (lastTime + nextSpawn < currTime)
        {
            if (currDebry < maxDebry)
            {
                SpawnDebry();
                lastTime = Time.time;
            }
        }
	}

    void SpawnDebry()
    {
        int loc = Random.Range(0, spawnLocs.Length);
        Instantiate(DebryStarterPrefab, spawnLocs[loc].position, Quaternion.identity);
        SpawnedDebries++;
    }
}
