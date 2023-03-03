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
    //��Ϸ�÷�
    private int score = 0;
    //��Ϸ�÷ֵ�UI��ʾ 
    public Text scoreText;
    //ʣ����Ϸʱ���UI��ʾ
    public Text CountDownText;
    //��Ϸ��ʱ�� 
    private int totalTime;
    //ʤ���ķ���
    private int winScore = 0;
    //��Ч�������
    public GameObject audioSet;


    //��ģʽ����
    public GameObject easyEnemy;
    //��ͨģʽ����
    public GameObject normalEnemy;
    //����ģʽ����
    public GameObject hardEnemy;
    //ѡ����Ϸ�Ѷ�
    private GameMode selectMode;
    //���λ����Ϣ
    public Transform player;
    //����λ����Ϣ
    public Transform canvas;
    //��ʾ����
    public GameObject InfoTips;
    //��Ч���ð�ť
    public Button audioSetBtn;
    //�����ʼλ��
    public Transform playerPos;
    //��������
    public List<GameObject> enemys;
    GameObject go = null;  //������

    private void Awake()
    {//�������ü����¼�
        selectMode = Selectlevel.gameMode;//Selectlevel�ǵ���ģʽ
        audioSetBtn.onClick.AddListener(OnAudiosetBtnClick);
    }

    void Start()
    {

        InitGame(selectMode);//��Awake�еĲ���selectMode���룬��ʼ����Ϸ

        StartCoroutine(CountDown());//����Э��,����ʱ

    }

    // Update is called once per frame
    void Update()
    {
        if (totalTime <= 0 && score < winScore)
        {
            if (go == null)  //һ��ʼ���϶��ܽ���
            {
                go = InsTips("��Ϸʧ��"); //������������������Ϊ�ַ���
                player.GetComponent<Player>().enabled = false; //������ʧЧ

            }
        }

    }
    //��ʼ����Ϸ
    private void InitGame(GameMode gamemode)
    {
        player.position = playerPos.position;//��ʼλ��
        player.localScale = new Vector3(0.5f, 0.5f, 0.5f);//��ʼ����
        player.GetComponent<Player>().enabled = true;
        //����Playercontrol�ű�����
        switch (gamemode)     //ѡ���Ѷȷ�֧
        {
            case GameMode.Easy:  //Easyģʽ
                easyEnemy.SetActive(true);
                normalEnemy.SetActive(false);
                hardEnemy.SetActive(false);
                winScore = 4;   //��Ϸ����ʤ���ĵ÷�
                totalTime = 30;  //��Ϸʱ��
                CountDownText.text = 30 + " ";//��ʾʣ��ʱ��
                for (int i = 0; i < enemys.Count; i++)  //������������
                {
                    if (enemys[i].tag == "easyEnemy")   //ͨ����ǩ�жϼ�ģʽ�µĵ���
                    {
                        enemys[i].SetActive(true);
                    }
                }
                break;

            case GameMode.Normal:
                easyEnemy.SetActive(true);
                normalEnemy.SetActive(true);
                hardEnemy.SetActive(false);
                winScore = 6;   //��Ϸ����ʤ���ĵ÷�
                totalTime = 60;  //��Ϸʱ��
                CountDownText.text = 60 + " ";//��ʾʣ��ʱ��
                for (int i = 0; i < enemys.Count; i++)  //������������
                {
                    if (enemys[i].tag == "easyEnemy" || enemys[i].tag == "normalEnemy")
                    //ͨ���� ǩ�ж���ͨģʽ�µĵ���
                    {
                        enemys[i].SetActive(true);
                    }
                }
                break;

            case GameMode.Hard:
                easyEnemy.SetActive(true);
                normalEnemy.SetActive(true);
                hardEnemy.SetActive(true);
                winScore = 8;   //��Ϸ����ʤ���ĵ÷�
                totalTime = 170;  //��Ϸʱ��
                CountDownText.text = 80 + " ";//��ʾʣ��ʱ��
                for (int i = 0; i < enemys.Count; i++)  //������������
                {
                    if (enemys[i].tag == "easyEnemy" || enemys[i].tag == "normalEnemy" || enemys[i].tag == "hardEnemy")
                    //ͨ���� ǩ�ж�����ģʽ�µĵ���
                    {
                        enemys[i].SetActive(true);
                    }
                }
                break;
        }

    }
    private GameObject InsTips(string v)
    {   //Ԥ����ʵ����
        GameObject go = Instantiate(InfoTips);
        //��ȡԤ�����text���
        Text text = go.transform.Find("Text").GetComponent<Text>();
        Button nextBtn = go.transform.Find("NextButton").GetComponent<Button>();
        Text nextBtnName = nextBtn.transform.Find("NextText").GetComponent<Text>();
        nextBtnName.text = "��һ��";
        //�������¿�ʼ��ť
        Button restartBtn = go.transform.Find("RestartButton").GetComponent<Button>();
        Text restartBtnName = restartBtn.transform.Find("RestartText").GetComponent<Text>();
        restartBtnName.text = "���¿�ʼ";
        text.text = v;  //���������ַ�
        Debug.Log("���������ַ�" + v);
        go.transform.parent = canvas;
        go.transform.position = canvas.transform.position; //����
        nextBtn.onClick.AddListener(OnNextClick);//�����һ����ť�ĵ������
        restartBtn.onClick.AddListener(OnRestartClick); //������¿�ʼ�ĵ������
        return go;
    }
    //���¿�ʼ������ת����
    private void OnRestartClick()
    {
        SceneManager.LoadScene("SelectLevel");
    }
    //���ݵ�ǰ���Ѷȿ�����һ��
    private void OnNextClick()
    {
        Debug.Log("��һ��");
        score = 0;
        scoreText.text = "�÷֣�" + score;

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
        //�����Ϸ����ʱ������0
        while (totalTime > 0)
        {
            yield return new WaitForSeconds(1); //ÿ����1s��ʱ���1
            totalTime--;
            CountDownText.text = "ʱ�䣺" + totalTime.ToString();

        }
    }
    private int frag = 0;

    //����Ƶ�������
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
    //������˼ӷַ�����ÿ����1�����ˣ���1��
    public void Addscore()
    {
        score++;
        scoreText.text = "�÷֣�" + score;
        if (score >= winScore)
        {
            InsTips("��Ϸʤ��");
            player.GetComponent<Player>().enabled = false;
        }

    }
}

