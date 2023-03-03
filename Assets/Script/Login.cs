using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Login : MonoBehaviour
{
    public InputField usernameinput;
    public InputField passwordinput;
    public Button loginbtn;
    public GameObject Tips;
    public Transform canvas;

    // Start is called before the first frame update
    void Start()
    {
        loginbtn.onClick.AddListener(onloginbtnclick);

    }
    private void onloginbtnclick()
    {
        if (usernameinput.text == "admin" && passwordinput.text == "admin")
        {
           SceneManager.LoadScene("Loading");
        }
        else
        {
           GameObject go = Instantiate(Tips);
           go.transform.parent = canvas;
           go.transform.position = canvas.transform.position;
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
