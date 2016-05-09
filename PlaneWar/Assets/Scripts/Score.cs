using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public static Score Instance = null;
    //得分
    public int score = 0;
    public Text scoreNum;
    ////历史最高分
    //int highscore = 0;

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
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //显示分数
        scoreNum.text = System.Convert.ToString(score);
	}

    void EnemyScore(int addScore){
        score = score + addScore;
    }

    void PowerScore(int addScore)
    {
        score = score + addScore;
    }


}
