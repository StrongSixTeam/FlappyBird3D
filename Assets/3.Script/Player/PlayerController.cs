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

    [Header("�÷��̾� ����� Ŭ��")]
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

        //string name = PlayerPrefs.GetString("PlayerName");
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
            //Ŀ�����·� �������� �ε����� ���װ� ������ �����
            playerAudio.PlayOneShot(breakClip);
            other.GetComponent<pipe>().Hide();
            Debug.Log(other.name + other.transform.parent.GetChild(0).name + "Ȱ��ȭ");
            other.transform.parent.GetChild(0).gameObject.SetActive(true); //��ƼŬ �°�
        }

        if (other.CompareTag("Item"))
        {
            item getItem = other.transform.parent.GetComponent<item>();
            getItem.waitSeconds(); //������ ��� �Ⱥ��̰�
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
                //�𵨸� �ٲٱ�
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

            //���콺 ��ư���� ���� ���� �� �ֱ�
            if (Input.GetMouseButtonDown(0))
            {
                playerAudio.PlayOneShot(WingClip);
                player_R.velocity = new Vector3(0, 0.5f, 0);
            }

            //���� ���� ĳ���� �����̼� ������ - bird ��
            if (player_R.velocity.y > 0 && player_R.rotation.x < 0.7f && gameObject.CompareTag("Player"))
            {
                transform.Rotate(new Vector3(80f * Time.deltaTime, 0, 0));
            }
            if (player_R.velocity.y <= 0 && player_R.rotation.x > 0.1f && gameObject.CompareTag("Player"))
            {
                transform.Rotate(new Vector3(-85f * Time.deltaTime, 0, 0));
            }

            //���� ������

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

        //Gameover, Restart UI �ۼ�
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
