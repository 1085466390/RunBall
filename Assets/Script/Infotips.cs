using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Infotips : MonoBehaviour
{
    public Button closebutton;

    void Start()
    {
        closebutton.onClick.AddListener(Onclickbutton);
    }

    private void Onclickbutton()
    {
        Destroy(this.gameObject);
    }

}

