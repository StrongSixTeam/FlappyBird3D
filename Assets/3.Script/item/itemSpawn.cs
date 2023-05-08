using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemSpawn : MonoBehaviour
{
    public GameObject item01;
    public GameObject item02;
    public GameObject item03;
    private bool makeItem = false;

    [Header("루프 체크")]
    [SerializeField] private BackgroundScrolling background;

    [Header("스폰퇼 파이프 위치값")]
    [SerializeField] private Transform spawn;

    // Update is called once per frame
    void Update()
    {
        //파이프 위치에 맞게 나오도록 조정

        if (background.isMove && !makeItem && GameManager.Instance.isCheck)
        {
            makeItem = true;
            background.isMove = false;
            
            int num = Random.Range(1, 4);
            if (num == 1)
            {
                item01.SetActive(true);
                item01.transform.position = new Vector3(spawn.position.x, spawn.position.y - 0.7f, spawn.position.z);
            }
            else if (num == 2)
            {
                item02.SetActive(true);
                item02.transform.position = new Vector3(spawn.position.x, spawn.position.y - 0.7f, spawn.position.z);
            }
            else if (num == 3)
            {
                item03.SetActive(true);
                item03.transform.position = new Vector3(spawn.position.x, spawn.position.y - 0.7f, spawn.position.z);
            }
        }
        else if (!background.isMove)
        {
            makeItem = false;
        }
    }
}
