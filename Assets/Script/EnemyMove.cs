using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, 2f, 0f), Space.World);//按照世界坐标系转动

    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        other.gameObject.transform.localScale *= 1.1f;
        GameManager.Instance.Addscore();

    }

}

