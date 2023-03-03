using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tips : MonoBehaviour
{
    // Start is called before the first frame update
    public Button bnt;
    void Start()
    {
        this.transform.localScale = Vector3.one;
        bnt.onClick.AddListener(OnClickbtn);
    }

    private void OnClickbtn()
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
