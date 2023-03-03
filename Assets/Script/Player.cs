using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public Rigidbody player;//�����������
    public float speed = 0.5f; //����С���ƶ��ٶ�
    public Vector3 offset;//�������������ƫ����
    public GameObject mainCamera;//�����������


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
        //ΪС��ʩ����ͨ��������Ϊ��һ����ȡ���·���
        player.AddForce(new Vector3(h, 0, v) * speed);
        //�������������λ�ã�ʹ�����С���ƶ�
        mainCamera.transform.position = transform.position + offset;
    }
}
