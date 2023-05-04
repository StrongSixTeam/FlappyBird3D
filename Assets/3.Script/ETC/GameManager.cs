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
        //1초 마다 스코어가 오른다
        //while (true)
        //{
        //score++;
        //score_Text.text = "Score : " + score;
        yield return new WaitForSeconds(1);
        //}
    }




    //게임오버시 PlayerController > Die() 에서 호출
    public void Gameover_Active()
    {
        //StopCoroutine(ScoreUp_co());
        gameover_ui.SetActive(true);
    }

    //나가기를 누르면 인트로 씬 로드 (닉네임 다시 입력 가능)
    public void IntroScene()
    {
        SceneManager.LoadScene(0);
    }

    //다시시작 버튼을 누르면 현재 씬 로드 (닉네임을 유지한채 재시도)
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }



}
