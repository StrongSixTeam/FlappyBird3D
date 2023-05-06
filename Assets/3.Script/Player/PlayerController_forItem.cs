using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_forItem : MonoBehaviour
{
    [SerializeField] private GameObject switchEach;

    private Rigidbody player_R;
    private AudioSource playerAudio;

    //left, right wing
    [SerializeField] Transform[] Wings;

    [Header("�÷��̾� ����� Ŭ��")]
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

    private void OnEnable()
    {
        transform.position = switchEach.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((!isBig && other.CompareTag("Pipe")) || other.CompareTag("DeadZone"))
        {
            Debug.Log("������ ����");
            //Die();
        }
        else if (isBig && other.CompareTag("Pipe"))
        {
            //���װ� ������ �����
            other.GetComponent<pipe>().Hide();
            other.transform.parent.GetChild(0).gameObject.SetActive(true); //��ƼŬ �°�
        }
        if (other.CompareTag("Item"))
        {
            item getItem = other.transform.parent.GetComponent<item>();
            getItem.waitSeconds();
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
                playerAudio.PlayOneShot(Item03Clip);
                switchEach.SetActive(true);
                gameObject.SetActive(false);
                //�𵨸� �ٲٱ�
            }
        }
    }
    private void Update()
    {
        //���콺 ��ư���� ���� ���� �� �ֱ�
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

        //���� ���� ĳ���� �����̼� ������
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

        //Gameover, Restart UI �ۼ�
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
        transform.localScale = new Vector3(1.2f, 1.2f, 1.22f);
        yield return new WaitForSeconds(4f);
        isBig = false;
        transform.localScale = new Vector3(0.45f, 0.45f, 0.45f);
    }
}
