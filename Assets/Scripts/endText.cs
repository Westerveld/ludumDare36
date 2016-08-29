using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class endText : MonoBehaviour {

    public Text scoreText;
	// Use this for initialization
	void Start () {
        scoreText.text = "Your Score: " + PlayerPrefs.GetInt("Score");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
