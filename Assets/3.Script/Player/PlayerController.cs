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
        }

        //���� ���� ĳ���� �����̼� ������
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
        //Gameover, Restart UI �ۼ�
    }
}
