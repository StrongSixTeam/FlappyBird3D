using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{//�ڽ� �ݶ��̴� �Ӽ��� ������ �ִ� ������Ʈ�� x�� ������
    private float width;
    private PipeMove pipeMove;
    private CloudHeight cloudHeight;

    public bool isMove = false;

    private void Awake()
    {
        BoxCollider backgroundcol = gameObject.GetComponent<BoxCollider>();
        width = backgroundcol.size.x;
        pipeMove = gameObject.GetComponent<PipeMove>();
        cloudHeight = gameObject.GetComponent<CloudHeight>();
        //������Ʈ��X�޸� ���� ����
    }
    // x�� �������� width���� ������� �������� �޼ҵ� ���� 
    void Update()
    {
      
        if (transform.position.x <= -width)
        {
            Reposition();
        }
    }
    //offset-> ���� 3�� ���ο� ��ǥ
    public void Reposition()
    {
        if (pipeMove!=null)
        {
            pipeMove.PipeControll();
        }
        
        if(cloudHeight!=null)
        {
            cloudHeight.CloudControll();
        }

        Vector3 offset = new Vector3(width * 2f, 0, 0);
        transform.position = transform.position + offset;

        isMove = true;
    }
}
