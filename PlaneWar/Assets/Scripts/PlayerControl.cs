using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PlayerControl : MonoBehaviour {

    public static PlayerControl Instance = null;

    public int speed=100;     //飞机速度
    public int life=50;      //飞机生命

    public GameObject player2;   //飞机2

    public Text LifeNum;   //生命值显示
    public Scrollbar timeBar;   //飞机2持续时长进度条
    public Text scoreNum;      //分数

    public AudioSource audio;
    public AudioClip clip;

    //动画组件
    Animator anim;
    

    void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(this.gameObject);
        }
    }

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();    //获取动画组件
	}
	
	// Update is called once per frame
    void Update()
    {

        float distance = speed * Time.deltaTime;       //飞机移动距离
        int horizontal = (int)Input.GetAxisRaw("Horizontal");
        int vertical = (int)Input.GetAxisRaw("Vertical");
        //飞机移动
        Move(horizontal, vertical, distance);

        //如果生命值为0，播放爆炸动画，爆炸音效，停止开火
        if (life <= 0)
        {
            StartCoroutine("Delay");
            anim.SetBool("Dead", true);
            audio.PlayOneShot(clip);
            Shoot.Instance.StopFire();
        }

        //显示生命值
        LifeNum.text = System.Convert.ToString(life);
        ////显示得分
        //ScoreNum.text = System.Convert.ToString(score);
    }

    //控制移动
    void Move(int horizontal, int vertical, float distance)
    {
        //水平移动
        if (transform.position.x <= 600 && transform.position.x >= -600)
        {
            if (horizontal == -1)
            {
                transform.Translate(-distance, 0, 0);
            }
            else if (horizontal == 1)
            {
                transform.Translate(distance, 0, 0);
            }
        }
        else if (transform.position.x < -600)
        {
            transform.position = new Vector3(-600, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > 600)
        {
            transform.position = new Vector3(600, transform.position.y, transform.position.z);
        }

        //垂直移动
        if (transform.position.y <= 212 && transform.position.y >= -208)
        {
            if (vertical == -1)
            {
                transform.Translate(0, -distance, 0);
            }
            else if (vertical == 1)
            {
                transform.Translate(0, distance, 0);
            }
        }
        else if (transform.position.y < -208)
        {
            transform.position = new Vector3(transform.position.x, -208, transform.position.z);
        }
        else if (transform.position.y > 212)
        {
            transform.position = new Vector3(transform.position.x, 212, transform.position.z);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //碰到敌人生命减少
        HitByEnemy(other);
        
        
        //碰到power1射击速率增加，飞机速度增加
        if (other.gameObject.tag == "power1")
        {
            float power = 0.7f;
            int speedAdd = 100;
            Shoot.Instance.rate = Shoot.Instance.rate*power;
            speed = speed+speedAdd;
            Shoot.Instance.StopFire();
            Shoot.Instance.OpenFire();
            //score = score + 10;     //得分
            scoreNum.SendMessage("PowerScore",10);
        }
        //碰到power2变身
        if (other.gameObject.tag == "power2")
        {
            player2.SetActive(true);      //飞机2可见
            player2.transform.position = this.transform.position;    //将飞机1的位置赋给飞机2
            Player2Control.Instance.time = Player2Control.Instance.TIME;    //重置飞机2持续时长
            this.gameObject.SetActive(false);      //飞机1不可见
            Shoot.Instance.StopFire();      //Shoot停火
            Shoot2.Instance.OpenFire();     //shoot2开火
            timeBar.gameObject.SetActive(true);    //飞机2时长进度条可见
            //score = score + 50;     //得分
            scoreNum.SendMessage("PowerScore", 50);
        }
    }

    void BeHit(int hurt)
    {
        life = life - hurt;
        anim.SetBool("Hit", true);    //播放被击中的动画
        StartCoroutine("StopAnim");   //等待一段时间后停止动画
    }

    void HitByEnemy(Collider2D other)
    {
        int hurt = 0;
        
        if (other.gameObject.tag == "Rock")
        {
            hurt = 2;
            BeHit(hurt);
        }
        else if (other.gameObject.tag == "Enemy1")
        {
            hurt = 1;
            BeHit(hurt);
        }
        else if (other.gameObject.tag == "Enemy2")
        {
            hurt = 3;
            BeHit(hurt);
        }
        else if (other.gameObject.tag == "Enemy3")
        {
            hurt = 5;
            BeHit(hurt);
        }
    }

    IEnumerator Delay()
    {
        //延时，然后销毁
        yield return new WaitForSeconds(0.28f);
        Destroy(gameObject);
        //弹出结束游戏面板
        EndPage.Instance.ShowEndPage();
    }

    IEnumerator StopAnim()
    {
        //延时，然后停止动画
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("Hit", false);
    }
}
