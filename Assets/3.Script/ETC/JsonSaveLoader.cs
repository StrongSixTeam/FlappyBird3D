using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

[Serializable]
public class Record
{
    public string name;
    public int score;
}

public class ArryData
{
    public Record[] records;
}

public class JsonSaveLoader : MonoBehaviour
{

    List<Record> saveData = new List<Record>();
    ArryData arryData = new ArryData();

    [Header("랭킹 UI Text")]
    [SerializeField] private GameObject[] nameText = new GameObject[5];
    [SerializeField] private GameObject[] scoreText = new GameObject[5];

    string path_Android;

    private void Start()
    {

        path_Android = Path.Combine(Application.persistentDataPath + "Save.json");
        print("파일 저장 위치 : "+path_Android);


        for (int i=0; i<5; i++)
        {
            nameText[i].SetActive(false);
            scoreText[i].SetActive(false);
        }


        //세이브파일에 저장된 기록이 있다면
        //List saveData에 업데이트합니다
        if (File.Exists(path_Android))
        {
            
            //Debug.Log("저장된 기록이 존재합니다");

            //배열로 담고 리스트로 변환
            arryData = JsonUtility.FromJson<ArryData>(File_Read());

            for(int i=0; i< arryData.records.Length; i++)
            {
                saveData.Add(arryData.records[i]);
            }

            print("List = " + saveData);
        }

    }

    //플레이어의 기록을 저장
    public void Save_Record(Record record)
    {
        bool isRetry = false;

        for(int i=0; i<saveData.Count; i++)
        {
            //똑같은 이름으로 플레이 기록이 있다면? 더 높은 기록으로 갈아치우기
            if (saveData[i].name == record.name)
            {
                if(saveData[i].score < record.score)
                {
                    saveData[i].score = record.score;
                }
                
                isRetry = true;
            }
        }

        if (!isRetry)
        {
            //현재 기록을 리스트에 추가
            saveData.Add(record);
        }


        Ranking_Result();
    }

    private void Ranking_Result()
    {

        //List에 담긴 내용 - 배열에 담고 score에 따라 등수 매기자
        //비활성해 둔 랭킹 UI Text에 집어넣고, 활성화하자 (5까지만)
        
        string[] ranking_n = new string[saveData.Count];
        int[] ranking_s = new int[saveData.Count];

        //배열 채워넣기
        for(int i=0; i<saveData.Count; i++)
        {
            ranking_n[i] = saveData[i].name;
            ranking_s[i] = saveData[i].score;
        }

        //순서매김
        if(saveData.Count > 1)
        {
            string temp_n;
            int temp_s;
            for (int j = 0; j < saveData.Count - 1; j++)
            {
                for (int i = 0; i < saveData.Count - 1; i++)
                {
                    //i+1 > i 주소의 값보다 클경우 자리바꾸기 (큰수를 왼쪽으로)
                    if (ranking_s[i] < ranking_s[i + 1]) 
                    {
                        temp_n = ranking_n[i + 1];
                        ranking_n[i + 1] = ranking_n[i];
                        ranking_n[i] = temp_n;

                        temp_s = ranking_s[i + 1];
                        ranking_s[i + 1] = ranking_s[i];
                        ranking_s[i] = temp_s;
                    }
                }
            }
        }
        

        //5등 이상까지만 UI에서 보여주기
        if(ranking_n.Length >= 5)
        {
            for (int i = 0; i < 5; i++)
            {
                nameText[i].GetComponent<Text>().text = ranking_n[i];
                scoreText[i].GetComponent<Text>().text = ranking_s[i].ToString();
                nameText[i].SetActive(true);
                scoreText[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < saveData.Count; i++)
            {
                nameText[i].GetComponent<Text>().text = ranking_n[i];
                scoreText[i].GetComponent<Text>().text = ranking_s[i].ToString();
                nameText[i].SetActive(true);
                scoreText[i].SetActive(true);
            }
        }


        //List saveData 업데이트
        saveData.Clear();
        for(int i=0; i<ranking_n.Length; i++)
        {
            saveData.Add(new Record
            {
                name = ranking_n[i],
                score = ranking_s[i]
            });
        }

        //5등 아래 자르기
        while (saveData.Count > 5)
        {
            saveData.RemoveAt(5);
        }


        File_Update();

    }
    

    private void File_Update()
    {
        //리스트에 저장된 내용을 string으로
        ArryData arryData = new ArryData
        {
            records = saveData.ToArray()
        };

        string jsonData = JsonUtility.ToJson(arryData);

        File.WriteAllText(path_Android, jsonData);

        print("파일 업데이트 : " + jsonData);
    }


    private string File_Read()
    {
        //파일에 저장된 내용을 string으로
        string jsonData = File.ReadAllText(path_Android);

        print("파일 불러오기 : " + jsonData);

        return jsonData;

    }

}
