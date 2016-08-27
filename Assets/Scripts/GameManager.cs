using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private int playerLives;
    public int PlayerLives { get; set; }

    private int score;
    public int Score { get; set; }

    private int spawnedDebries;
    public int SpawnedDebries { get { return spawnedDebries; } set { spawnedDebries = value; } }

    public GameObject DebryStarterPrefab;
    public float maxX, maxY, currTime, nextSpawn, lastTime;
	// Use this for initialization
	void Start () {
        Score = 0;
        PlayerLives = 3;
        SpawnedDebries = 0;
	}
	
	// Update is called once per frame
	void Update () {
        currTime = Time.time;
        if (lastTime + nextSpawn < currTime)
        {
            if (SpawnedDebries < 4)
            {
                SpawnDebry();
                lastTime = Time.time;
            }
        }
	}

    void SpawnDebry()
    {
        Vector2 spawnLoc = new Vector2(Random.Range(-maxX, maxX), Random.Range(-maxY, maxY));
        Instantiate(DebryStarterPrefab, spawnLoc, Quaternion.identity);
    }
}
