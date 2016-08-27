using UnityEngine;
using System.Collections;

public class bulletScript : MonoBehaviour {

    public float startTime, endTime, currTime;
	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(transform.forward * 1000);
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        currTime = Time.time;
        if (currTime - startTime > endTime)
        {
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "enemy")
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
}
