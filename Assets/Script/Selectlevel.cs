using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public enum GameMode
{
    //难度类型 ，若不赋值1，则从0开始   
    Easy = 1,
    Normal,
    Hard,
}

//创建Selectlevel单例模式，方便在之后调用
public class Selectlevel : MonoBehaviour
{//单例模式，确保项目期间只有一个实例
    private static Selectlevel instance;
    public static Selectlevel Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Selectlevel>();
            }
            return instance;
        }
    }
    //引用对象并在Awake()方法中设置默认为Easy难度模式并添加对按钮的点击事件
    //获取游戏难度
    public static GameMode gameMode;
    //开始按钮
    public Button startBtn;
    void Awake()
    {
        //初始难度设为简单
        gameMode = GameMode.Easy;
        //添加开始按钮点击事件
        startBtn.onClick.AddListener(ClickStartBtn);
    }
    private void ClickStartBtn()
    {
        SceneManager.LoadScene("Main");
    }
    public void SetEasy()
    {
        gameMode = GameMode.Easy;
        Debug.Log(gameMode);
    }
    public void SetNormal()
    {
        gameMode = GameMode.Normal;
        Debug.Log(gameMode);
    }
    public void SetHard()
    {
        gameMode = GameMode.Hard;
        Debug.Log(gameMode);
    }
    void Start()
    { }
    void Update()
    { }
}




