using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }
    //游戏得分
    private int score = 0;
    //游戏得分的UI显示 
    public Text scoreText;
    //剩余游戏时间的UI显示
    public Text CountDownText;
    //游戏总时长 
    private int totalTime;
    //胜利的分数
    private int winScore = 0;
    //音效设置面板
    public GameObject audioSet;


    //简单模式敌人
    public GameObject easyEnemy;
    //普通模式敌人
    public GameObject normalEnemy;
    //困难模式敌人
    public GameObject hardEnemy;
    //选择游戏难度
    private GameMode selectMode;
    //玩家位置信息
    public Transform player;
    //画布位置信息
    public Transform canvas;
    //提示弹窗
    public GameObject InfoTips;
    //音效设置按钮
    public Button audioSetBtn;
    //玩家起始位置
    public Transform playerPos;
    //敌人数组
    public List<GameObject> enemys;
    GameObject go = null;  //管理弹窗

    private void Awake()
    {//音乐设置监听事件
        selectMode = Selectlevel.gameMode;//Selectlevel是单例模式
        audioSetBtn.onClick.AddListener(OnAudiosetBtnClick);
    }

    void Start()
    {

        InitGame(selectMode);//将Awake中的参数selectMode传入，初始化游戏

        StartCoroutine(CountDown());//开启协程,倒计时

    }

    // Update is called once per frame
    void Update()
    {
        if (totalTime <= 0 && score < winScore)
        {
            if (go == null)  //一开始，肯定能进入
            {
                go = InsTips("游戏失败"); //创建弹窗方法，参数为字符串
                player.GetComponent<Player>().enabled = false; //玩家组件失效

            }
        }

    }
    //初始化游戏
    private void InitGame(GameMode gamemode)
    {
        player.position = playerPos.position;//初始位置
        player.localScale = new Vector3(0.5f, 0.5f, 0.5f);//初始缩放
        player.GetComponent<Player>().enabled = true;
        //开启Playercontrol脚本功能
        switch (gamemode)     //选择难度分支
        {
            case GameMode.Easy:  //Easy模式
                easyEnemy.SetActive(true);
                normalEnemy.SetActive(false);
                hardEnemy.SetActive(false);
                winScore = 4;   //游戏可以胜利的得分
                totalTime = 30;  //游戏时长
                CountDownText.text = 30 + " ";//显示剩余时间
                for (int i = 0; i < enemys.Count; i++)  //遍历敌人数组
                {
                    if (enemys[i].tag == "easyEnemy")   //通过标签判断简单模式下的敌人
                    {
                        enemys[i].SetActive(true);
                    }
                }
                break;

            case GameMode.Normal:
                easyEnemy.SetActive(true);
                normalEnemy.SetActive(true);
                hardEnemy.SetActive(false);
                winScore = 6;   //游戏可以胜利的得分
                totalTime = 60;  //游戏时长
                CountDownText.text = 60 + " ";//显示剩余时间
                for (int i = 0; i < enemys.Count; i++)  //遍历敌人数组
                {
                    if (enemys[i].tag == "easyEnemy" || enemys[i].tag == "normalEnemy")
                    //通过标 签判断普通模式下的敌人
                    {
                        enemys[i].SetActive(true);
                    }
                }
                break;

            case GameMode.Hard:
                easyEnemy.SetActive(true);
                normalEnemy.SetActive(true);
                hardEnemy.SetActive(true);
                winScore = 8;   //游戏可以胜利的得分
                totalTime = 170;  //游戏时长
                CountDownText.text = 80 + " ";//显示剩余时间
                for (int i = 0; i < enemys.Count; i++)  //遍历敌人数组
                {
                    if (enemys[i].tag == "easyEnemy" || enemys[i].tag == "normalEnemy" || enemys[i].tag == "hardEnemy")
                    //通过标 签判断困难模式下的敌人
                    {
                        enemys[i].SetActive(true);
                    }
                }
                break;
        }

    }
    private GameObject InsTips(string v)
    {   //预制体实例化
        GameObject go = Instantiate(InfoTips);
        //获取预制物的text组件
        Text text = go.transform.Find("Text").GetComponent<Text>();
        Button nextBtn = go.transform.Find("NextButton").GetComponent<Button>();
        Text nextBtnName = nextBtn.transform.Find("NextText").GetComponent<Text>();
        nextBtnName.text = "下一关";
        //定义重新开始按钮
        Button restartBtn = go.transform.Find("RestartButton").GetComponent<Button>();
        Text restartBtnName = restartBtn.transform.Find("RestartText").GetComponent<Text>();
        restartBtnName.text = "重新开始";
        text.text = v;  //传进来的字符
        Debug.Log("传进来的字符" + v);
        go.transform.parent = canvas;
        go.transform.position = canvas.transform.position; //坐标
        nextBtn.onClick.AddListener(OnNextClick);//添加下一步按钮的点击方法
        restartBtn.onClick.AddListener(OnRestartClick); //添加重新开始的点击方法
        return go;
    }
    //重新开始，需跳转场景
    private void OnRestartClick()
    {
        SceneManager.LoadScene("SelectLevel");
    }
    //根据当前的难度开启下一关
    private void OnNextClick()
    {
        Debug.Log("下一关");
        score = 0;
        scoreText.text = "得分：" + score;

        if (selectMode == GameMode.Hard)
        {
            SceneManager.LoadScene("SelectLevel");
            return;
        }
        switch (selectMode)
        {
            case GameMode.Easy:
                selectMode = GameMode.Normal;
                break;
            case GameMode.Normal:
                selectMode = GameMode.Hard;
                break;
            case GameMode.Hard:
                selectMode = GameMode.Easy;
                break;
        }
        InitGame(selectMode);
    }
    IEnumerator CountDown()
    {
        //如果游戏的总时长大于0
        while (totalTime > 0)
        {
            yield return new WaitForSeconds(1); //每经过1s，时间减1
            totalTime--;
            CountDownText.text = "时间：" + totalTime.ToString();

        }
    }
    private int frag = 0;

    //打开音频设置面板
    private void OnAudiosetBtnClick()
    {
        if (frag == 0)
        {
            audioSet.SetActive(true);
            frag = 1;
        }
        else
        {
            audioSet.SetActive(false);
            frag = 0;
        }

    }
    //消灭敌人加分方法，每消灭1个敌人，加1分
    public void Addscore()
    {
        score++;
        scoreText.text = "得分：" + score;
        if (score >= winScore)
        {
            InsTips("游戏胜利");
            player.GetComponent<Player>().enabled = false;
        }

    }
}

