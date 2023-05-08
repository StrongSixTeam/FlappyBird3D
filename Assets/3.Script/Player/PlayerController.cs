using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject skin;

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
    private bool isBig = false;

    private void Awake()
    {
        TryGetComponent(out player_R);
        TryGetComponent(out playerAudio);
    }
    private void OnEnable()
    {
        transform.localScale = new Vector3(0.45f, 0.45f, 0.45f);
        transform.position = skin.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((!isBig && other.CompareTag("Pipe")) || other.CompareTag("DeadZone"))
        {
            Die();
        }
        else if (isBig && other.CompareTag("Pipe"))
        {
            //커진상태로 파이프랑 부딪히면 안죽고 파이프 숨기기
            playerAudio.PlayOneShot(breakClip);
            other.GetComponent<pipe>().Hide();
            other.transform.parent.GetChild(0).gameObject.SetActive(true); //파티클 맞게
        }

        if (other.CompareTag("Item"))
        {
            item getItem = other.transform.parent.GetComponent<item>();
            getItem.waitSeconds(); //아이템 잠시 안보이게
            if (getItem.type.Equals(1))
            {
                playerAudio.PlayOneShot(Item01Clip);
                StartCoroutine(biggerCo());
            }
            else if (getItem.type.Equals(2))
            {
                playerAudio.PlayOneShot(Item02Clip);
                GameManager.Instance.score += 2;
            }
            else if (getItem.type.Equals(3))
            {
                skin.SetActive(true);
                skin.GetComponent<PlayerController>().playItem3();
                gameObject.SetActive(false);
                //모델링 바꾸기
            }
        }
    }
    private void Update()
    {
        if (!GameManager.Instance.isCheck)
        {
            player_R.useGravity = false;
        }
        else
        {
            player_R.useGravity = true;

            //마우스 버튼으로 위쪽 방향 힘 주기
            if (Input.GetMouseButtonDown(0))
            {
                playerAudio.PlayOneShot(WingClip);
                player_R.velocity = new Vector3(0, 0.5f, 0);
            }

            //힘에 따라 캐릭터 로테이션 돌리기 - bird 만
            if (player_R.velocity.y > 0 && player_R.rotation.x < 0.7f && gameObject.CompareTag("Player"))
            {
                transform.Rotate(new Vector3(80f * Time.deltaTime, 0, 0));
            }
            if (player_R.velocity.y <= 0 && player_R.rotation.x > 0.1f && gameObject.CompareTag("Player"))
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
    }

    private void Die()
    {
        playerAudio.PlayOneShot(deathClip);

        Time.timeScale = 0;

        //Gameover, Restart UI 작성
        GameManager.Instance.Gameover_Active();
    }
    private IEnumerator biggerCo()
    {
        isBig = true;
        transform.localScale = new Vector3(1.2f, 1.2f, 1.22f);
        yield return new WaitForSeconds(4f);
        isBig = false;
        transform.localScale = new Vector3(0.45f, 0.45f, 0.45f);
    }
    public void playItem3()
    {
        playerAudio.PlayOneShot(Item03Clip);
    }
}
