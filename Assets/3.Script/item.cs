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
        transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed); 
    }

    public void activeFalse() //������ ����
    {
        gameObject.SetActive(false);
    }
}
