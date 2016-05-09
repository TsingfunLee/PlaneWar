using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    //public Toggle beijing;
    //public AudioSource audio;
    bool IsOn=true;

    // Use this for initialization
    void Start()
    {
        //Screen.SetResolution(1000, 600, false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void QuitGame()
    {
        Application.Quit();   //退出游戏
    }

    public void PauseGame()
    {
        //暂停游戏
        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        //继续游戏
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        //重新开始游戏
        Application.LoadLevel(0);
        Time.timeScale = 1;
    }

    //控制背景音乐
    public void ControlAudio(AudioSource audio)
    {
        if(IsOn){
            audio.Stop();
            IsOn = false;
        }
        else if (!IsOn)
        {
            audio.Play();
            IsOn = true;
        }
    }

    //控制音效
    public void ControlAudio2(AudioSource audio)
    {
        if (IsOn)
        {
            audio.volume=0;
            IsOn = false;
        }
        else if (!IsOn)
        {
            audio.volume=1;
            IsOn = true;
        }
    }
}
