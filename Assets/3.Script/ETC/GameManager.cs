using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region singleton
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Destroy(gameObject);
        }
    }
    #endregion


    [Header("���ھ�")]
    [SerializeField] private Text score_Text; //�÷���ȭ�鿡 ��� ���� ���ھ�
    public int score;


    [Header("Gameover ������Ʈ")]
    [SerializeField] private GameObject gameover_ui;
    [SerializeField] private Text name_Text; //�̸�
    [SerializeField] private Text totalScore_Text; //�������ھ�



    private void Start()
    {
        gameover_ui.SetActive(false);
    }



    public void ScoreUp()
    {
        //StartCoroutine(ScoreUp_co());
    }

    IEnumerator ScoreUp_co()
    {
        //1�� ���� ���ھ ������
        //while (true)
        //{
        //score++;
        //score_Text.text = "Score : " + score;
        yield return new WaitForSeconds(1);
        //}
    }




    //���ӿ����� PlayerController > Die() ���� ȣ��
    public void Gameover_Active()
    {
        //StopCoroutine(ScoreUp_co());
        gameover_ui.SetActive(true);
    }

    //�����⸦ ������ ��Ʈ�� �� �ε� (�г��� �ٽ� �Է� ����)
    public void IntroScene()
    {

    }

    //�ٽý��� ��ư�� ������ ���� �� �ε� (�г����� ������ä ��õ�)
    public void Restart()
    {

    }



}
