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


    [Header("스코어")]
    [SerializeField] private Text score_Text; //플레이화면에 띄울 현재 스코어
    public int score;

    [Header("Gameover 오브젝트")]
    [SerializeField] private GameObject gameover_ui;
    [SerializeField] private Text name_Text; //이름
    [SerializeField] private Text totalScore_Text; //최종스코어

    [Header("count 오브젝트")]
    [SerializeField] private GameObject count_ui;
    [SerializeField] private Text count_Text;
    private int count;

    [Header("SpeedUP 오브젝트")]
    [SerializeField] private GameObject SpeedUp_Text;
    private Color color;

    [Header("Json 오브젝트")]
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
    public void CountDown() //게임 시작 전 카운트다운 메소드
    {
        StartCoroutine(CountDown_co());
    }

    IEnumerator CountDown_co() //카운트다운 코루틴
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
        //1초 마다 스코어가 오른다
        while (true)
        {
            score++;
            score_Text.text = "Score : " + score;
            yield return new WaitForSeconds(1);
        }
    }

    //게임오버시 PlayerController > Die() 에서 호출
    public void Gameover_Active()
    {
        StopCoroutine(ScoreUp_co());
        gameover_ui.SetActive(true);

        //인트로에서 입력받은 이름
        string playerName = PlayerPrefs.GetString("PlayerName");
        if(playerName == "")
        {
            playerName = "익명";
        }

        name_Text.text = playerName;
        totalScore_Text.text = "" + score;

        //이름과 스코어를 기록합니다
        Record record = new Record
        {
            name = playerName,
            score = score
        };

        json.GetComponent<JsonSaveLoader>().Save_Record(record);
    }


    //나가기를 누르면 인트로 씬 로드 (닉네임 다시 입력 가능)
    public void IntroScene()
    {
        SceneManager.LoadScene("IntroScene");
    }

    //다시시작 버튼을 누르면 현재 씬 로드 (닉네임을 유지한채 재시도)
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
