using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    private float rotationSpeed = 50f; //회전 속도
    public int type = 1; //결정해주기 1: 커지는, 2: 점수, 3: 캐릭터 변경

    void Update()
    {
        //항상 빙글 돌기
        transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed); 
    }

    public void activeFalse() //아이템 끄기
    {
        gameObject.SetActive(false);
    }
}
