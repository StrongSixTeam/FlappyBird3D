using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemSpawn : MonoBehaviour
{
    public GameObject item01;
    public GameObject item02;
    public GameObject item03;
    private bool makeItem = false;

    [Header("���� üũ")]
    [SerializeField] private BackgroundScrolling background;

    [Header("�����p ������ ��ġ��")]
    [SerializeField] private Transform spawn;

    // Update is called once per frame
    void Update()
    {
        //������ ��ġ�� �°� �������� ����

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
