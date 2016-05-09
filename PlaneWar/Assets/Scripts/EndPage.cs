using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndPage : MonoBehaviour {

    public static EndPage Instance = null;
    public Text totalscore;     //总分
    public Text highscore;      //历史最高分
    static int highScore=0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }else if(Instance!=null)
        {
            Destroy(this.gameObject);
        }
        this.gameObject.SetActive(false);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShowEndPage()
    {
        //显示总分
        totalscore.text = "总分：" + Score.Instance.score;
        //显示最高分
        GetHighScore();
        highscore.text = "历史最高：" + highScore;
        //结束面板可见
        this.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    //获得历史最高分
    void GetHighScore()
    {
        if (highScore<Score.Instance.score)
	    {
            highScore = Score.Instance.score;
	    }
        
    }
}
