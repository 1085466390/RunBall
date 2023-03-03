using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Loading : MonoBehaviour
{
    // Start is called before the first frame update
    //百分比字体
    public Text loadingtext;
    //进度条
    public Slider bar;
    //进度条的值
    public int value = 0;
    //异步协程
    private AsyncOperation opre;
    void Start()
    {
        //判断当前场景是否是Loading场景
        if (SceneManager.GetActiveScene().name == "Loading")
        {
            //启动协程
            StartCoroutine(AsyncLoading());
        }
    }

    IEnumerator AsyncLoading()
    {
        //预加载选择难度
        opre = SceneManager.LoadSceneAsync("SelectLevel");
        //暂停加载完成的自动切换功能
        opre.allowSceneActivation = false;
        //保存当前的协程状态
        yield return opre;
    }

    // Update is called once per frame
    void Update()
    {
        //进度条的目标值
        int progressvalue = 100;
        //如果当前值小于目标值
        if (value < progressvalue)
        {
            value++;

        }
        //显示进度的百分比文字
        loadingtext.text = value + "%";
        //更新进度条的进度
        bar.value = value / 100f;
        //当进度条到达100的时候
        if (value == 100)
        {
            //启动场景切换
            opre.allowSceneActivation = true;
            //文本显示完成
            loadingtext.text = "OK";
        }
    }

}


