using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSet : MonoBehaviour
{
    
    private Slider slider;//滑动条
    public AudioSource audiclip; //音效
    public Button closenamebtn; //关闭按钮

    // Start is called before the first frame update
    void Start()
    {
        slider = transform.Find("Slider").GetComponent<Slider>();
        closenamebtn.onClick.AddListener(OnCloseBtnClick);
        //滑动条值发生改变
        slider.onValueChanged.AddListener(OnSliderChanged);

    }

    private void OnCloseBtnClick()
    {
        this.gameObject.SetActive(false);
    }

    private void OnSliderChanged(float value)
    {
        audiclip.volume = slider.value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
