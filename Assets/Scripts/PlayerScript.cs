using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    private Rigidbody2D rigid;
    public float angleForce, shotFrequency, lastShotTime, shotSpeed, moveSpeed, maxY, maxX;
    public Transform shotLoc;
    public GameObject bulletPrefab;
    public Vector3 minBounds, maxBounds;
    public bool isMoving;
    private Animator anim;


	// Use this for initialization
	void Start () {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
        Rotation();
        Attack();    
	}

    void Movement()
    {
        var pos = transform.position;
        float boost = Input.GetAxis("Boost");

        
        if (boost != 0 || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            rigid.AddForce(transform.up * moveSpeed, ForceMode2D.Force);
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

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
   
    void Rotation()
    {
        
        float angle = Input.GetAxis("Horizontal");
        rigid.AddTorque(angleForce * -angle, ForceMode2D.Force);

    }

    void Attack()
    {
        float attack = Input.GetAxis("Fire");
        if ((attack != 0 || Input.GetKeyDown(KeyCode.Space)) && Time.time - lastShotTime > shotFrequency)
        {
            Vector2 target = new Vector2(transform.position.x, transform.position.y);
            Vector2 mypos = new Vector2(shotLoc.position.x, shotLoc.position.y);
            Vector2 dir = target - mypos;
            dir.Normalize();

            GameObject bullet = (GameObject)Instantiate(bulletPrefab, shotLoc.position, Quaternion.identity);
            bullet.gameObject.GetComponent<Rigidbody2D>().velocity = shotSpeed * -dir;
            lastShotTime = Time.time;
        }
    }

    void Respawn()
    {
        Debug.Log("respawning");
        var animat = gameObject.GetComponent<Animator>();
        animat.SetBool("isDead", false);
        gameObject.SetActive(true);
        var pos = gameObject.transform.position;
        pos.x = 0;
        pos.y = 0;
        var rot = gameObject.transform.rotation;
        rot.z = 180;
        gameObject.transform.rotation = rot;
        gameObject.transform.position = pos;

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "enemy")
        {
            var animat = gameObject.GetComponent<Animator>();
            animat.SetBool("isDead", true);
            WaitForAnim();
            gameObject.SetActive(false);
            Invoke("Respawn", 1.5f);

        }
    }

    private IEnumerator WaitForAnim()
    {
        do
        {
            yield return null;
        }
        while (anim.GetBool("isDead"));
    }
}
