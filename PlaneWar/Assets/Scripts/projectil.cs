using UnityEngine;
using System.Collections;

public class projectil : MonoBehaviour {

    public float speed = 500;
    public int hurt=1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //子弹向上运动
        transform.Translate(0,speed*Time.deltaTime,0);
        //超出屏幕上边缘销毁子弹
        if (transform.position.y>220)
        {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        //如果碰到敌人，则调用敌人的BeHit,然后销毁子弹
        if (other.gameObject.tag=="Rock"
            ||other.gameObject.tag=="Enemy1"||other.gameObject.tag=="Enemy2"||other.gameObject.tag=="Enemy3")
        {
            other.gameObject.SendMessage("BeHit",hurt);
            Destroy(gameObject);
        }
    }
}
