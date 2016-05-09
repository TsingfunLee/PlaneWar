using UnityEngine;
using System.Collections;

public class BackGroundMove : MonoBehaviour {

    int speed;

	// Use this for initialization
	void Start () {
        speed = 50;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, -speed * Time.deltaTime, 0);
        if (transform.position.y<=-800)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y+1600, transform.position.z);
        }
	
	}
}
