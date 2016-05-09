using UnityEngine;
using System.Collections;

public class Power : MonoBehaviour {

    public float speed = 150;

    GameObject camera;
    AudioSource audio;
    public AudioClip clip;

	// Use this for initialization
	void Start () {
        camera = GameObject.FindWithTag("MainCamera");
        audio = camera.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        //向下运动
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        //超出边界销毁
        if (transform.position.y < -300)
        {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag=="Player"||other.transform.tag=="Player2")
        {
            Destroy(gameObject);
            audio.PlayOneShot(clip);
        }
    }
}
