using UnityEngine;
using System.Collections;

public class debryScript : MonoBehaviour {

    public GameObject newDebry;
    private GameManager GM;
    private Rigidbody2D rigid;


    public Transform target;

    public int debryInt;
    public float maxX, maxY,speed;
    // Use this for initialization
	void Start () {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        rigid.velocity = new Vector2(Random.Range(-speed, speed), Random.Range(-speed, speed));
        rigid.AddTorque(Random.Range(-1f, 1f) * 360f);
        GM = GameObject.Find("GM").GetComponent<GameManager>();
        GM.SpawnedDebry++;
	}
	
	// Update is called once per frame
	void Update () {
        var pos = transform.position;
        if (pos.y > maxY)
        {
            pos.y = -maxY;
        }
        if (pos.y < -maxY)
        {
            pos.y = maxY;
        }
        if (pos.x > maxX)
        {
            pos.x = -maxX;
        }
        if (pos.x < -maxX)
        {
            pos.x = maxX;
        }
        transform.position = pos;

        if (pos.x > -1 && pos.x < 1 && pos.y > -1 && pos.y < 1 && GM.isDead == true)
        {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "bullet")
        {
            GM.PlayHit();
            GM.SpawnedDebry--;
            GM.Score += 10;
            for (int i = 0; i < debryInt; i++)
            {
                Instantiate(newDebry, transform.position, Quaternion.identity);
            }
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Player")
        {
            GM.PlayHit();
            GM.SpawnedDebry--;
            for (int i = 0; i < debryInt; i++)
            {
                Instantiate(newDebry, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
