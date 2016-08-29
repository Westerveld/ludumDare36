using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    private int playerLives;
    public int PlayerLives { get { return playerLives; } set { playerLives = value; } }

    public GameObject[] lives;

    public Text scoreText;
    public int Score;

    public int SpawnedDebry;

    public bool isDead;
    public AudioSource audi;
    public AudioSource aud;

    public Transform[] spawnLocs;
    public GameObject DebryStarterPrefab, ShipPrefab;
    public float maxX, maxY, currTime, nextSpawn, lastTime, maxDebry,currDebry,currScore, shipSpawn,shipCount, nextIncrease, allowedShips;
	// Use this for initialization
	void Start () {
        Score = 0;
        PlayerLives = 3;
        SpawnedDebry = 0;
        StartCoroutine(FadeIn());
        PlayerPrefs.SetInt("Score", 0);
	}
	
	// Update is called once per frame
	void Update () {
        currTime = Time.time;
        if (lastTime + nextSpawn < currTime)
        {
            if (SpawnedDebry < maxDebry)
            {
                SpawnDebry();
                lastTime = Time.time;
            }
        }
        updateLives();
        scoreText.text = "Score: " + Score;

        if (playerLives == 0)
        {
            PlayerPrefs.SetInt("Score", Score);
            StartCoroutine(loadStart("end"));
        }

        if (shipCount < allowedShips)
        {
            SpawnShip();
        }
     }

    void SpawnDebry()
    {
        int loc = Random.Range(0, spawnLocs.Length);
        Instantiate(DebryStarterPrefab, spawnLocs[loc].position, Quaternion.identity);
    }

    void SpawnShip()
    {
        int loc = Random.Range(0, spawnLocs.Length);
        Instantiate(ShipPrefab, spawnLocs[loc].position, Quaternion.identity);
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

    private IEnumerator loadStart(string level)
    {
        while (audi.volume > 0.1f)
        {
            audi.volume -= 0.1f;
            yield return new WaitForSeconds(0.05f);
        }
        float fadeTime = GameObject.Find("GM").GetComponent<FadeOut>().BeginFade(1);

        yield return new WaitForSeconds(fadeTime);
        
        Application.LoadLevel(level);
    }

    private IEnumerator FadeIn()
    {
        while (audi.volume < 0.7f)
        {
            audi.volume += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void PlayHit()
    {
        aud.Play();
    }
}
