using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    private float rotationSpeed = 50f; //ȸ�� �ӵ�
    public int type = 1; //�������ֱ� 1: Ŀ����, 2: ����, 3: ĳ���� ����

    void Update()
    {
        //�׻� ���� ����
        //transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
        if (transform.position.x < -5)
        {
            gameObject.SetActive(false);
        }
    }

    public void activeFalse() //�÷��̾� ��ũ��Ʈ���� item�� �ε��� �� ������ ���� �ҷ�����
    {
        gameObject.SetActive(false);
    }

    public void waitSeconds()
    {
        gameObject.SetActive(false);
    }

    public void setActive()
    {
        transform.position = new Vector3(-10, transform.position.y, transform.position.z);
    }
}
