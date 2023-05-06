using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollObj : MonoBehaviour
{
    public float speed;

    private ScrollObj mapScroll;
    private bool isSpeedUp = false;

    private void Awake()
    {
        mapScroll = GameObject.Find("Map").GetComponent<ScrollObj>();
    }

    void Update()
    {
        if (GameManager.Instance.isCheck)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime * 0.5f);

            if (GameManager.Instance.score % 30 == 0 && !isSpeedUp)
            {
                speed += 0.5f;

                isSpeedUp = true;
            }
            if(GameManager.Instance.score % 30 != 0)
            {
                isSpeedUp = false;
            }

            if (gameObject.CompareTag("Item"))
            {
                speed = mapScroll.speed;
            }
        }
    }

}
