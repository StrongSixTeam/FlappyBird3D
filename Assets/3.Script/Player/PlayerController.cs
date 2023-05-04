using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody player_R;

    //left, right wing
    [SerializeField] Transform[] Wings;

    private bool isJump = false;

    private void Awake()
    {
        TryGetComponent(out player_R);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pipe") || other.CompareTag("DeadZone"))
        {
            Die();
        }
        if (other.CompareTag("Item01"))
        {
            //�Ŵ�ȭ ������
        }
        if (other.CompareTag("Item02"))
        {
            //���� ���� ������
        }
        if (other.CompareTag("Item03"))
        {
            //���� ������
        }
    }
    private void Update()
    {
        //���콺 ��ư���� ���� ���� �� �ֱ�
        if (Input.GetMouseButtonDown(0))
        {
            player_R.velocity = new Vector3(0, 0.5f, 0);
            isJump = true;
        }
        if (isJump)
        {
            StartCoroutine(WingMove_co());
            isJump = false;
        }

        //���� ���� ĳ���� �����̼� ������
        if (player_R.velocity.y > 0 && (player_R.rotation.eulerAngles.x < 20))
        {
            transform.Rotate(new Vector3(80f * Time.deltaTime, 0, 0));
        }
        if (player_R.velocity.y <= 0 && player_R.rotation.eulerAngles.x > 20)
        {
            transform.Rotate(new Vector3(-85f * Time.deltaTime, 0, 0));
        }
        Debug.Log(player_R.rotation.eulerAngles.x);
    }

    private void Die()
    {
        Time.timeScale = 0;

        //Gameover, Restart UI �ۼ�
    }

    private IEnumerator WingMove_co()
    {
        Wings[0].localPosition -= Vector3.forward * Time.deltaTime;

        yield return new WaitForSeconds(2f);

        Wings[0].localPosition += Vector3.forward * Time.deltaTime;
    }

}
