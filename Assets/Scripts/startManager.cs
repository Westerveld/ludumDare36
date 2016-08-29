using UnityEngine;
using System.Collections;

public class startManager : MonoBehaviour {

    public AudioSource audioClip;

	// Use this for initialization
	void Start () {
        audioClip.Play();
        StartCoroutine(FadeIn());
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

    public void infoButton()
    {
        StartCoroutine(loadStart("info"));
    }
    private IEnumerator loadStart(string level)
    {
        float fadeTime = GameObject.Find("GM").GetComponent<FadeOut>().BeginFade(1);
        while (audioClip.volume > 0.1f)
        {
            audioClip.volume -= 0.1f;
            yield return null;
        }
        yield return new WaitForSeconds(fadeTime);
        Application.LoadLevel(level);
    }

    private IEnumerator FadeIn()
    {
        while (audioClip.volume < 1f)
        {
            audioClip.volume += 0.1f;
            yield return null;
        }
    }

}
