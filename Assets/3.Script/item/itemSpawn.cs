using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemSpawn : MonoBehaviour
{
    public GameObject item01;
    public GameObject item02;
    public GameObject item03;
    private bool makeItem = false;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.score % 10 == 0 && !makeItem) //5의 배수면 생성
        {
            makeItem = true;
            int num = Random.Range(1, 4);
            Debug.Log(num);
            if (num == 1)
            {
                item01.SetActive(true);
                item01.transform.position = new Vector3(6, 1, 0);
            }
            else if (num == 2)
            {
                item02.SetActive(true);
                item02.transform.position = new Vector3(6, 1, 0);
            }
            else if (num == 3)
            {
                item03.SetActive(true);
                item03.transform.position = new Vector3(6, 1, 0);
            }
        }
        else if (GameManager.Instance.score % 5 != 0)
        {
            makeItem = false;
        }
    }
}
