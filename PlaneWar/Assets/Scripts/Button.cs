using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

    public AudioSource audio;
    public AudioClip clip;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseEnter()
    {
        audio.PlayOneShot(clip);
    }
}
