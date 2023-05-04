using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody player_R;

    //left, right wing
    [SerializeField] Transform[] Wings;

    private void Awake()
    {
        TryGetComponent(out player_R);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pipe"))
        {
            Die();
        }
        if (other.CompareTag("Item01"))
        {
            //거대화 아이템
        }
        if (other.CompareTag("Item02"))
        {
            //점수 증가 아이템
        }
        if (other.CompareTag("Item03"))
        {
            //변신 아이템
        }
    }
    private void Update()
    {
        //마우스 버튼으로 위쪽 방향 힘 주기
        if (Input.GetMouseButtonDown(0))
        {
            player_R.velocity = new Vector3(0, 0.5f, 0);
        }

        //힘에 따라 캐릭터 로테이션 돌리기
        if (player_R.velocity.y > 0)
        {
            transform.Rotate(new Vector3(50f * Time.deltaTime, 0, 0));
        }
        if (player_R.velocity.y <= 0)
        {
            transform.Rotate(new Vector3(-50f * Time.deltaTime, 0, 0));
        }
    }
    private void Die()
    {
        Time.timeScale = 0;
        //Gameover, Restart UI 작성
    }
}
