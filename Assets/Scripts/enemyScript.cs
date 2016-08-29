using UnityEngine;
using System.Collections;

public class enemyScript : MonoBehaviour {

    public Transform ShotLoc;
    public GameObject Arrow;
    public Transform target;

    private GameManager GM;

    public float veloc, rotato, shotSpeed, lastShot, lastTime, currTime, nextTime, maxX,maxY,shootSpeed;
    private Rigidbody2D rigid;

    public GameObject newDebry;
    private Animator anim;
    private AudioSource aud;
	// Use this for initialization
	void Start () {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        GM = GameObject.Find("GM").GetComponent<GameManager>();
        aud = gameObject.GetComponent<AudioSource>();
        anim = gameObject.GetComponent<Animator>();
        GM.shipCount++;
	}
	
	// Update is called once per frame
	void Update () {
        currTime = Time.time;

        if (lastTime + nextTime < currTime)
        {
            rigid.AddTorque(Random.Range(-0.5f, 0.5f) * 360f / Mathf.PI);
            rigid.AddForce((transform.up * veloc)* Random.Range(4,veloc), ForceMode2D.Force);
            lastTime = Time.time;
        }
        if (lastShot + shotSpeed < currTime)
        {
            Vector2 tar = transform.position;
            Vector2 myPos = ShotLoc.position;
            Vector2 dir = tar - myPos;
            dir.Normalize();
            anim.SetBool("isMoving", true);
            GameObject clone = (GameObject)Instantiate(Arrow, ShotLoc.position, ShotLoc.rotation);
            clone.gameObject.GetComponent<Rigidbody2D>().velocity = shootSpeed * -dir;
            aud.Play();
            lastShot = Time.time;
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
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
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "bullet")
        {
            GM.PlayHit();
            GM.Score += 30;
            GM.shipCount--;
            Instantiate(newDebry, transform.position, Quaternion.identity);
            Destroy(col.gameObject);
            Destroy(gameObject);

        }
    }
}
