using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{//박스 콜라이더 속성을 가지고 있는 오브젝트의 x측 사이즈
    private float width;
    private PipeMove pipeMove;
    private CloudHeight cloudHeight;


    private void Awake()
    {
        BoxCollider backgroundcol = gameObject.GetComponent<BoxCollider>();
        width = backgroundcol.size.x;
        pipeMove = gameObject.GetComponent<PipeMove>();
        cloudHeight = gameObject.GetComponent<CloudHeight>();
        //업데이트문X메모리 많이 먹음
    }
    // x축 포지션이 width보다 길어지면 리포지션 메소드 실행 
    void Update()
    {
      
        if (transform.position.x <= -width)
        {
            Reposition();
        }
    }
    //offset-> 벡터 3의 새로운 좌표
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
       
      
    }
}
