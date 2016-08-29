using UnityEngine;
using System.Collections;

public class startManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartButton()
    {
        StartCoroutine(loadStart("main"));
    }

    public void Hound()
    {
        Application.ExternalEval("window.open('http://www.galactichounds.co.uk','_blank')");
    }

    public void returnButton()
    {
        StartCoroutine(loadStart("start"));
    }

    private IEnumerator loadStart(string level)
    {
        float fadeTime = GameObject.Find("GM").GetComponent<FadeOut>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        Application.LoadLevel(level);
    }
}
