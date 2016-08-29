using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    private int playerLives;
    public int PlayerLives { get { return playerLives; } set { playerLives = value; } }

    public GameObject[] lives;

    public Text scoreText;
    private int score;

    private int spawnedDebries;

    public Transform[] spawnLocs;
    public GameObject DebryStarterPrefab;
    public float maxX, maxY, currTime, nextSpawn, lastTime, maxDebry,currDebry,currScore;
	// Use this for initialization
	void Start () {
        score = 0;
        PlayerLives = 3;
        spawnedDebries = 0;
	}
	
	// Update is called once per frame
	void Update () {
        currScore = score;
        currDebry = spawnedDebries;
        currTime = Time.time;
        if (lastTime + nextSpawn < currTime)
        {
            if (currDebry < maxDebry)
            {
                SpawnDebry();
                lastTime = Time.time;
            }
        }
        updateLives();
        scoreText.text = "Score: " + currScore;

        if (playerLives == 0)
        {
            StartCoroutine(loadStart("end"));
        }
     }

    void SpawnDebry()
    {
        int loc = Random.Range(0, spawnLocs.Length);
        Instantiate(DebryStarterPrefab, spawnLocs[loc].position, Quaternion.identity);
    }

    public void updateLives()
    {
        if (PlayerLives == 3)
        {
            lives[2].SetActive(true);
            lives[1].SetActive(true);
            lives[0].SetActive(true);
        }
        if (PlayerLives == 2)
        {
            lives[2].SetActive(false);
            lives[1].SetActive(true);
            lives[0].SetActive(true);
        }
        if (PlayerLives == 1)
        {
            lives[1].SetActive(false);
            lives[0].SetActive(true);
        }

    }

    public void AddScore(int value)
    {
        score += value;
    }

    public void AddDebry(int value)
    {
        spawnedDebries += value;
    }

    public void RemoveDebry(int value)
    {
        spawnedDebries -= value;
    }

    private IEnumerator loadStart(string level)
    {
        float fadeTime = GameObject.Find("GM").GetComponent<FadeOut>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        Application.LoadLevel(level);
    }

}
