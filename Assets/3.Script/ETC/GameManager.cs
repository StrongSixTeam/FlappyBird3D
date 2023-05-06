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

    [Header("count ������Ʈ")]
    [SerializeField] private GameObject count_ui;
    [SerializeField] private Text count_Text;
    private int count;

    [Header("SpeedUP ������Ʈ")]
    [SerializeField] private GameObject SpeedUp_Text;
    private Color color;

    [Header("Json ������Ʈ")]
    [SerializeField] private GameObject json;

    public bool isCheck = false;

    private void Start()
    {
        Time.timeScale = 1;
        score = 0;
        count = 3;

        isCheck = false;
        CountDown();
    }
    private void Update()
    {
        if (count < 0 && !isCheck)
        {
            StopCoroutine(CountDown_co());
            isCheck = true;
            count_ui.SetActive(false);
            ScoreUp();
        }

        if(score % 30 == 0 && score != 0)
        {
            SpeedUp_Text.SetActive(true);
        }
        else
        {
            SpeedUp_Text.SetActive(false);
        }
    }
    public void ScoreUp()
    {
        StartCoroutine(ScoreUp_co());
    }
    public void CountDown() //���� ���� �� ī��Ʈ�ٿ� �޼ҵ�
    {
        StartCoroutine(CountDown_co());
    }

    IEnumerator CountDown_co() //ī��Ʈ�ٿ� �ڷ�ƾ
    {
        while (true)
        {
            if (count > 0)
            {
                count_Text.text = count.ToString();
            }
            else
            {
                count_Text.text = "START!";
            }
            yield return new WaitForSeconds(1);
            count--;
        }
    }

    IEnumerator ScoreUp_co()
    {
        //1�� ���� ���ھ ������
        while (true)
        {
            score++;
            score_Text.text = "Score : " + score;
            yield return new WaitForSeconds(1);
        }
    }

    //���ӿ����� PlayerController > Die() ���� ȣ��
    public void Gameover_Active()
    {
        StopCoroutine(ScoreUp_co());
        gameover_ui.SetActive(true);

        //��Ʈ�ο��� �Է¹��� �̸�
        string playerName = PlayerPrefs.GetString("PlayerName");
        if(playerName == "")
        {
            playerName = "�͸�";
        }

        name_Text.text = playerName;
        totalScore_Text.text = "" + score;

        //�̸��� ���ھ ����մϴ�
        Record record = new Record
        {
            name = playerName,
            score = score
        };

        json.GetComponent<JsonSaveLoader>().Save_Record(record);
    }


    //�����⸦ ������ ��Ʈ�� �� �ε� (�г��� �ٽ� �Է� ����)
    public void IntroScene()
    {
        SceneManager.LoadScene("IntroScene");
    }

    //�ٽý��� ��ư�� ������ ���� �� �ε� (�г����� ������ä ��õ�)
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
