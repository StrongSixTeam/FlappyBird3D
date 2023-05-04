using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;
using System.IO;


public class Record
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

    List<Record> saveData = new List<Record>();

    [Header("랭킹 UI Text")]
    [SerializeField] private GameObject[] nameText = new GameObject[5];
    [SerializeField] private GameObject[] scoreText = new GameObject[5];

    private void Start()
    {
        for(int i=0; i<5; i++)
        {
            nameText[i].SetActive(false);
            scoreText[i].SetActive(false);
        }

        // List saveData 업데이트
        // -> 세이브파일에있는내용을 saveData 리스트에 저장
        //string으로 쪼개서 넣자


    }

    //플레이어의 기록을 저장
    public void _saveData(Record record)
    {
        //현재 기록을 리스트에 추가
        saveData.Add(record);

        //Record -> string 변환
        string jsonData = JsonConvert.SerializeObject(saveData);
        print("기록 : " + jsonData);

        //jsonData(string)을 파일로 저장
        File.WriteAllText(Application.dataPath + "/PBsave.json", jsonData);
    }


    public void _loadData()
    {
        string jsonData = File.ReadAllText(Application.dataPath + "/PBsave.json");
        
        //if 순위 5등 이하가 있다면 기록 삭제 (리스트에서 제거 후 파일 업데이트)

        //string을 쪼개
        //name과 score 배열에 담자
        //socre 순서대로 정렬하고 네임이 따라가게 하자
        //비활성해 둔 랭킹 UI Text에 집어넣고, 활성화하자

    }

}
