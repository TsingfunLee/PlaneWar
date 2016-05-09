using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player2Control : MonoBehaviour {

    public static Player2Control Instance = null;

    public int speed=300;     //飞机速度
    public int life=50;      //飞机生命
    public int LIFE = 50;  //飞机2固有生命值
    public float time=10;      //飞机2持续时长
    public int TIME = 10;   //飞机2持续固有时长

    public GameObject player;    //飞机1
    public Scrollbar timeBar;    //持续时间进度条
    public Text scoreNum;     //显示分数

    public Text LifeNum;     //显示生命值

    public AudioSource audio;
    public AudioClip clip;

    //动画组件
    Animator anim;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(this.gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();    //获取动画组件
        //score = PlayerControl.Instance.score;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = speed * Time.deltaTime;       //飞机移动距离
        int horizontal = (int)Input.GetAxisRaw("Horizontal");
        int vertical = (int)Input.GetAxisRaw("Vertical");
        //飞机移动
        Move(horizontal, vertical, distance);    

        //每帧时长减去一帧
        time = time - Time.deltaTime;
        //print(time);
        //如果时长为0，变回飞机1
        if (time<=0)
        {
            player.transform.position = this.transform.position;  //飞机2的位置赋给飞机1
            player.gameObject.SetActive(true);   //飞机1可见
            this.gameObject.SetActive(false);    //飞机2不可见
            Shoot2.Instance.StopFire();          //shoot2停火
            Shoot.Instance.OpenFire();           //shoot1开火
            timeBar.gameObject.SetActive(false);  //时长进度条可见
        }

        //如果生命值为0，播放爆炸动画,音效，停止开火
        if (life<=0)
        {
            StartCoroutine("Delay");
            anim.SetBool("Dead", true);
            audio.PlayOneShot(clip);
            Shoot2.Instance.StopFire();
        }

        //显示生命值
        LifeNum.text = System.Convert.ToString(life);
        //显示得分
        //scoreNum.text = System.Convert.ToString(score);

        //显示持续时长
        TimeScroll();
    }

    //控制飞机移动
    void Move(int horizontal,int vertical,float distance)
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
            float power = 0.7f;    //射击速率增加倍率
            int speedAdd = 100;    //飞机速度增加数量
            Shoot2.Instance.rate = Shoot2.Instance.rate * power;
            speed = speed+speedAdd;
            Shoot2.Instance.StopFire();
            Shoot2.Instance.OpenFire();
            //score = score + 10;
            scoreNum.SendMessage("PowerScore", 10);
        }
        //碰到power2变身
        if (other.gameObject.tag == "power2")
        {
            time = TIME;
            life = LIFE;
            scoreNum.SendMessage("PowerScore", 50);
            //score = score + 50;
            //player2.SetActive(true);      //飞机2可见
            //player2.transform.position = this.transform.position;    //将飞机1的位置赋给飞机2
            //this.gameObject.SetActive(false);      //飞机1不可见
            //Shoot.Instance.StopFire();      //Shoot停火
        }
    }

    //掉血
    void BeHit(int hurt)
    {
        life = life - hurt;
        anim.SetBool("Hit", true);
        StartCoroutine("StopAnim");
    }

    //被敌人击中
    void HitByEnemy(Collider2D other)
    {
        int hurt = 0;   //受到伤害值
        if (other.gameObject.tag == "Rock")
        {
            hurt = 1;
            BeHit(hurt);
        }
        else if (other.gameObject.tag == "Enemy1")
        {
            hurt = 1;
            BeHit(hurt);
        }
        else if (other.gameObject.tag == "Enemy2")
        {
            hurt = 2;
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
    }

    //显示飞机2持续时间进度条
    public void TimeScroll()
    {
        timeBar.size = time/TIME;
    }

    IEnumerator StopAnim()
    {
        //延时，然后停止动画
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("Hit", false);
    }
}
