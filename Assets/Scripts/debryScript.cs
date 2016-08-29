using UnityEngine;
using System.Collections;

public class debryScript : MonoBehaviour {

    public GameObject newDebry;
    public GameManager GM;
    private Rigidbody2D rigid;
    public int debryInt;
    public float maxX, maxY,speed;
    // Use this for initialization
	void Start () {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        rigid.velocity = new Vector2(Random.Range(-speed, speed), Random.Range(-speed, speed));
        rigid.AddTorque(Random.Range(-1f, 1f) * 360f);

        GM.AddDebry(1);
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
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "bullet")
        {
            GM.RemoveDebry(1);
            GM.AddScore(10);
            for (int i = 0; i < debryInt; i++)
            {
                Instantiate(newDebry, transform.position, Quaternion.identity);
            }
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Player")
        {
            GM.RemoveDebry(1);
            for (int i = 0; i < debryInt; i++)
            {
                Instantiate(newDebry, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
