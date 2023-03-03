using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public Rigidbody player;//公开刚体组件
    public float speed = 0.5f; //设置小球移动速度
    public Vector3 offset;//定义摄像机坐标偏移量
    public GameObject mainCamera;//引用主摄像机


    // Start is called before the first frame update
    void Start()
    {
        offset = mainCamera.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        //为小球施加普通力，方向为上一步获取的新方向
        player.AddForce(new Vector3(h, 0, v) * speed);
        //更新主摄像机的位置，使其跟随小球移动
        mainCamera.transform.position = transform.position + offset;
    }
}
