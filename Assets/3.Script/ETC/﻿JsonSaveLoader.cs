using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;


public class ﻿JsonSaveLoader : MonoBehaviour
{

    //Record record = new Record("temp_name", GameManager.Instance.score);

    private void Start()
    {
        //temp_name를 Intro씬에서 입력받은 이름으로 교체
        //record.name = "temp";

        //기록을 직렬화
        //string jsonData = JsonConvert.SerializeObject(record);
        //print("record : " + jsonData);
    }



}


//class Record
//{
//    public string name;
//    public int score;

//    //생성자
//    public Record(string name, int score)
//    {
//        this.name = name;
//        this.score = score;
//    }

//}
