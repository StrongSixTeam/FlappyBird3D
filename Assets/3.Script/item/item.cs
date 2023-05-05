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
        //transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
        if (transform.position.x < -5)
        {
            gameObject.SetActive(false);
        }
    }

    public void activeFalse() //플레이어 스크립트에서 item과 부딪힐 때 아이템 끄기 불러오기
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
