using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    public int type = 1; //�������ֱ� 1: Ŀ����, 2: ����, 3: ĳ���� ����

    void Update()
    {
        if (transform.position.x < -5)
        {
            gameObject.SetActive(false);
        }
    }

    public void waitSeconds()
    {
        gameObject.SetActive(false);
    }

}
