using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_forItem : MonoBehaviour
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

    private bool isJump = false;
    private bool isBig = false;

    private void Awake()
    {
        TryGetComponent(out player_R);
        TryGetComponent(out playerAudio);

        //string name = PlayerPrefs.GetString("PlayerName");
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((!isBig && other.CompareTag("Pipe")) || other.CompareTag("DeadZone"))
        {
            Debug.Log("원래면 죽음");
            //Die();
        }
        else if (isBig && other.CompareTag("Pipe"))
        {
            //안죽고 파이프 부서지기
            Debug.Log("파이프 부숨");
        }
        if (other.CompareTag("Item"))
        {
            item getItem = other.transform.parent.GetComponent<item>();
            getItem.waitSeconds();
            if (getItem.type.Equals(1))
            {
                StartCoroutine(biggerCo());
            }
            else if (getItem.type.Equals(2))
            {
                GameManager.Instance.score += 2;
            }
            else if (getItem.type.Equals(3))
            {
                //모델링 바꾸기
            }
        }
    }
    private void Update()
    {
        //마우스 버튼으로 위쪽 방향 힘 주기
        if (Input.GetMouseButtonDown(0))
        {
            playerAudio.PlayOneShot(WingClip);
            player_R.velocity = new Vector3(0, 0.5f, 0);
            isJump = true;
        }
        if (isJump)
        {
            //StartCoroutine(WingMove_co());
            isJump = false;
        }

        //힘에 따라 캐릭터 로테이션 돌리기
        if (player_R.velocity.y > 0 && (player_R.rotation.eulerAngles.x > 20))
        {
            transform.Rotate(new Vector3(80f * Time.deltaTime, 0, 0));
        }
        if (player_R.velocity.y <= 0 && player_R.rotation.eulerAngles.x > 20)
        {
            transform.Rotate(new Vector3(-85f * Time.deltaTime, 0, 0));
        }
    }

    private void Die()
    {
        playerAudio.PlayOneShot(deathClip);

        Time.timeScale = 0;

        //Gameover, Restart UI 작성
        GameManager.Instance.Gameover_Active();
    }

    private IEnumerator WingMove_co()
    {
        Wings[0].localPosition -= Vector3.forward * Time.deltaTime;

        yield return new WaitForSeconds(2f);

        Wings[0].localPosition += Vector3.forward * Time.deltaTime;
    }
    private IEnumerator biggerCo()
    {
        isBig = true;
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        yield return new WaitForSeconds(4f);
        isBig = false;
        transform.localScale = new Vector3(0.45f, 0.45f, 0.45f);
    }

}
