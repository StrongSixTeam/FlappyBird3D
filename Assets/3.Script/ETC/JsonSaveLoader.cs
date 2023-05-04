using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

class Record
{
    public string name;
    public int score;

    //생성자
    public Record(string name, int score)
    {
        this.name = name;
        this.score = score;
    }
}


public class JsonSaveLoader : MonoBehaviour
{

    Record record = new Record("temp_name", 0);

    private void Start()
    {
        //temp_name를 Intro씬에서 입력받은 이름으로 교체
        //record.name = "temp";

        //게임오버 시 
        //record.score = GameManager.Instance.score;

        //기록을 문장형데이터로
        string jsonData = JsonConvert.SerializeObject(record);
        print("기록 : " + jsonData);
    }
}
