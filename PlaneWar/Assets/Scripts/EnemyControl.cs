using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum ENEMY
{
    rock,
    enemy1,
    enemy2,
    enemy3
}

public class EnemyControl : MonoBehaviour {

    //游戏物体，石头和敌机
    //public GameObject rock;
    //public GameObject enemy1;
    //public GameObject enemy2;
    //public GameObject enemy3;
    public ENEMY enemys = ENEMY.rock;

    //速度
    //public float rockSpeed = 100;
    //public float enemy1Speed = 100;
    //public float enemy2Speed = 100;
    //public float enemy3Speed = 100;
    public float speed = 100;

    //生命
    //public float rockLife = 1;
    //public float enemy1Life = 3;
    //public float enemy2Life = 5;
    //public float enemy3Life = 10;
    public float life = 1;

    GameObject camera;
    AudioSource audio;
    public AudioClip clip;

    //分数
    GameObject scoreNum;

    //动画组件
    Animator anim;

    //是否存活
    //bool alive = true;

    void Start()
    {
        anim=GetComponent<Animator>();    //获取动画组件
        scoreNum = GameObject.FindWithTag("score");
        camera = GameObject.FindWithTag("MainCamera");
        audio = camera.GetComponent<AudioSource>();
    }

    void Update()
    {
        //向下运动
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        //transform.Translate(Vector3.down * enemy1Speed * Time.deltaTime);
        //transform.Translate(Vector3.down * enemy2Speed * Time.deltaTime);
        //transform.Translate(Vector3.down * enemy3Speed * Time.deltaTime);
        
        
        //超出下边界销毁
        if (transform.position.y < -300)
        {
            Destroy(gameObject);
        }
        //if (enemy1.transform.position.y < -300)
        //{
        //    Destroy(enemy1.gameObject);
        //}
        //if (enemy2.transform.position.y < -300)
        //{
        //    Destroy(enemy2.gameObject);
        //}
        //if (enemy3.transform.position.y < -300)
        //{
        //    Destroy(enemy3.gameObject);
        //}
    }


    void BeHit(int hurt)
    {
        //受到伤害，生命减去伤害值，播放爆炸动画，再调用延迟方法
        if(enemys==ENEMY.rock)
        {
            life=life-hurt;
            if (life<=0)
            {
                StartCoroutine("Delay",2);
                anim.SetBool("Dead", true);
                //scoreNum.gameObject.SendMessage("EnemyScore", 2);
            }
            
        }
        else if (enemys == ENEMY.enemy1)
        {
            life=life-hurt;
            if (life <= 0)
            {
                StartCoroutine("Delay",1);
                anim.SetBool("Dead", true);
                //scoreNum.gameObject.SendMessage("EnemyScore", 1);
            }
            
        }
        else if (enemys == ENEMY.enemy2)
        {
            life = life - hurt;
            if (life <= 0)
            {
                StartCoroutine("Delay",5);
                anim.SetBool("Dead", true);
                //scoreNum.gameObject.SendMessage("EnemyScore", 5);
            }
            
        }
        else if (enemys == ENEMY.enemy3)
        {
            life = life - hurt;
            if (life <= 0)
            {
                StartCoroutine("Delay",10);
                anim.SetBool("Dead", true);
                //scoreNum.gameObject.SendMessage("EnemyScore", 10);
            }
            
        }
    }

    IEnumerator Delay(int score)
    {
        audio.PlayOneShot(clip);
        //延时，然后销毁,得分
        yield return new WaitForSeconds(0.27f);
        Destroy(gameObject);
        scoreNum.gameObject.SendMessage("EnemyScore", score);
    }

}
