using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Loading : MonoBehaviour
{
    // Start is called before the first frame update
    //�ٷֱ�����
    public Text loadingtext;
    //������
    public Slider bar;
    //��������ֵ
    public int value = 0;
    //�첽Э��
    private AsyncOperation opre;
    void Start()
    {
        //�жϵ�ǰ�����Ƿ���Loading����
        if (SceneManager.GetActiveScene().name == "Loading")
        {
            //����Э��
            StartCoroutine(AsyncLoading());
        }
    }

    IEnumerator AsyncLoading()
    {
        //Ԥ����ѡ���Ѷ�
        opre = SceneManager.LoadSceneAsync("SelectLevel");
        //��ͣ������ɵ��Զ��л�����
        opre.allowSceneActivation = false;
        //���浱ǰ��Э��״̬
        yield return opre;
    }

    // Update is called once per frame
    void Update()
    {
        //��������Ŀ��ֵ
        int progressvalue = 100;
        //�����ǰֵС��Ŀ��ֵ
        if (value < progressvalue)
        {
            value++;

        }
        //��ʾ���ȵİٷֱ�����
        loadingtext.text = value + "%";
        //���½������Ľ���
        bar.value = value / 100f;
        //������������100��ʱ��
        if (value == 100)
        {
            //���������л�
            opre.allowSceneActivation = true;
            //�ı���ʾ���
            loadingtext.text = "OK";
        }
    }

}


