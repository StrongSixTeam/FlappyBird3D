using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json.Linq;

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


        //세이브파일에 저장된 기록이 있다면
        //List saveData에 업데이트합니다
        if (File.Exists("/PBsave.json"))
        {
            Debug.Log("저장된 기록이 존재합니다");
            string jsonData = File.ReadAllText(Application.persistentDataPath + "%userprofile%/AppData/LocalLow/FlappyBird3D/PBsave.json");
            //string jsonDataA = File.ReadAllText(Application.persistentDataPath + "/mnt/sdcard/Android/data/FlappyBird3D/files/PBsave.json");
            List<Record> list = JsonConvert.DeserializeObject<List<Record>>(jsonData);

            for(int i=0; i<list.Count; i++)
            {
                saveData.Add(list[i]);
            }

        }
        else
        {
            //확인용
            saveData.Add(new Record("A", 10));
            saveData.Add(new Record("B", 6));
            saveData.Add(new Record("C", 0));
            saveData.Add(new Record("D", 5));
            saveData.Add(new Record("E", 3));
            saveData.Add(new Record("F", 120));

        }


    }

    //플레이어의 기록을 저장
    public void Save_Record(Record record)
    {
        //현재 기록을 리스트에 추가
        saveData.Add(record);

        //Record -> string 변환
        string jsonData = JsonConvert.SerializeObject(saveData);
        print("List 기록 : " + jsonData);

        //jsonData(string)을 파일로 저장
        //File.WriteAllText(Application.dataPath + "/PBsave.json", jsonData);
        File.WriteAllText(Application.persistentDataPath + "%userprofile%/AppData/LocalLow/FlappyBird3D/PBsave.json", jsonData);
        //File.WriteAllText(Application.persistentDataPath + "/mnt/sdcard/Android/data/FlappyBird3D/files/PBsave.json", jsonData);

        //등수 비교
        Load_Record();
    }


    public void Load_Record()
    {
        string jsonData = File.ReadAllText(Application.persistentDataPath + "%userprofile%/AppData/LocalLow/FlappyBird3D/PBsave.json");
        //string jsonDataA = File.ReadAllText(Application.persistentDataPath + "/mnt/sdcard/Android/data/FlappyBird3D/files/PBsave.json");
        print("파일 : " + jsonData);


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
        

        //5등 이상까지만 보여주기
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
            saveData.Add(new Record(ranking_n[i], ranking_s[i]));
        }

        //5등 아래 자르기
        while (saveData.Count > 5)
        {
            saveData.RemoveAt(5);
        }


        //파일 업데이트
        jsonData = JsonConvert.SerializeObject(saveData);
        print("갱신 : " + jsonData);
        File.WriteAllText(Application.persistentDataPath + "%userprofile%/AppData/LocalLow/FlappyBird3D/PBsave.json", jsonData);
        //File.WriteAllText(Application.persistentDataPath + "/mnt/sdcard/Android/data/FlappyBird3D/files/PBsave.json", jsonData);

    }

}
