using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public enum GameMode
{
    //�Ѷ����� ��������ֵ1�����0��ʼ   
    Easy = 1,
    Normal,
    Hard,
}

//����Selectlevel����ģʽ��������֮�����
public class Selectlevel : MonoBehaviour
{//����ģʽ��ȷ����Ŀ�ڼ�ֻ��һ��ʵ��
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
    //���ö�����Awake()����������Ĭ��ΪEasy�Ѷ�ģʽ����Ӷ԰�ť�ĵ���¼�
    //��ȡ��Ϸ�Ѷ�
    public static GameMode gameMode;
    //��ʼ��ť
    public Button startBtn;
    void Awake()
    {
        //��ʼ�Ѷ���Ϊ��
        gameMode = GameMode.Easy;
        //��ӿ�ʼ��ť����¼�
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




