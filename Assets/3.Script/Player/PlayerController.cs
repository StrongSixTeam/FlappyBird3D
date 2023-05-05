using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody player_R;
    private AudioSource playerAudio;

    //left, right wing
    [SerializeField] Transform[] Wings;

    [Header("플레이어 오디오 클립")]
    [SerializeField] AudioClip deathClip;
    [SerializeField] AudioClip Item01Clip;
    [SerializeField] AudioClip Item02Clip;
    [SerializeField] AudioClip Item03Clip;
    [SerializeField] AudioClip breakClip;
    [SerializeField] AudioClip WingClip;

    private bool isUp = true;

    private void Awake()
    {
        TryGetComponent(out player_R);
        TryGetComponent(out playerAudio);

        //string name = PlayerPrefs.GetString("PlayerName");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pipe") || other.CompareTag("DeadZone"))
        {
            Die();
        }
        //if (other.CompareTag("Item01"))
        //{
        //    //거대화 아이템
        //    playerAudio.PlayOneShot(Item01Clip);
        //}
        //if (other.CompareTag("Item02"))
        //{
        //    //점수 증가 아이템
        //    playerAudio.PlayOneShot(Item02Clip);
        //}
        //if (other.CompareTag("Item03"))
        //{
        //    //변신 아이템
        //    playerAudio.PlayOneShot(Item03Clip);
        //}
    }
    private void Update()
    {
        //마우스 버튼으로 위쪽 방향 힘 주기
        if (Input.GetMouseButtonDown(0))
        {
            playerAudio.PlayOneShot(WingClip);  
            player_R.velocity = new Vector3(0, 0.5f, 0);
        }

        //힘에 따라 캐릭터 로테이션 돌리기
        if (player_R.velocity.y > 0 && player_R.rotation.x < 0.7f)
        {
            transform.Rotate(new Vector3(80f * Time.deltaTime, 0, 0));
        }
        if (player_R.velocity.y <= 0 && player_R.rotation.x > 0.1f)
        {
            transform.Rotate(new Vector3(-85f * Time.deltaTime, 0, 0));
        }

        //날개 움직임

        if (isUp)
        {
            Wings[0].localPosition -= Vector3.forward * Time.deltaTime;
            Wings[1].localPosition -= Vector3.forward * Time.deltaTime;

            if (Wings[0].localPosition.z <= -0.3)
            {
                isUp = false;
            }
        }
        else
        {
            Wings[0].localPosition += Vector3.forward * Time.deltaTime;
            Wings[1].localPosition += Vector3.forward * Time.deltaTime;

            if (Wings[0].localPosition.z >= 0)
            {
                isUp = true;
            }
        }
    }

    private void Die()
    {
        playerAudio.PlayOneShot(deathClip);

        Time.timeScale = 0;

        //Gameover, Restart UI 작성
        GameManager.Instance.Gameover_Active();
    }
}
