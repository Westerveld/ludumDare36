using UnityEngine;
using System.Collections;

public class enemyBullet : MonoBehaviour {

    public float startTime, endTime, currTime, maxX, maxY;

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

    
}
