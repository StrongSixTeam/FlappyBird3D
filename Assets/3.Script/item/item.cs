using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    public int type = 1; //결정해주기 1: 커지는, 2: 점수, 3: 캐릭터 변경

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
