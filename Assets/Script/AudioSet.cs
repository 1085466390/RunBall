using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSet : MonoBehaviour
{
    
    private Slider slider;//������
    public AudioSource audiclip; //��Ч
    public Button closenamebtn; //�رհ�ť

    // Start is called before the first frame update
    void Start()
    {
        slider = transform.Find("Slider").GetComponent<Slider>();
        closenamebtn.onClick.AddListener(OnCloseBtnClick);
        //������ֵ�����ı�
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
