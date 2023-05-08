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

    [Header("��ŷ UI Text")]
    [SerializeField] private GameObject[] nameText = new GameObject[5];
    [SerializeField] private GameObject[] scoreText = new GameObject[5];

    string path_Android;

    private void Start()
    {

        path_Android = Path.Combine(Application.persistentDataPath + "Save.json");
        print("���� ���� ��ġ : "+path_Android);


        for (int i=0; i<5; i++)
        {
            nameText[i].SetActive(false);
            scoreText[i].SetActive(false);
        }


        //���̺����Ͽ� ����� ����� �ִٸ�
        //List saveData�� ������Ʈ�մϴ�
        if (File.Exists(path_Android))
        {
            
            //Debug.Log("����� ����� �����մϴ�");

            //�迭�� ��� ����Ʈ�� ��ȯ
            arryData = JsonUtility.FromJson<ArryData>(File_Read());

            for(int i=0; i< arryData.records.Length; i++)
            {
                saveData.Add(arryData.records[i]);
            }

            print("List = " + saveData);
        }

    }

    //�÷��̾��� ����� ����
    public void Save_Record(Record record)
    {
        bool isRetry = false;

        for(int i=0; i<saveData.Count; i++)
        {
            //�Ȱ��� �̸����� �÷��� ����� �ִٸ�? �� ���� ������� ����ġ���
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
            //���� ����� ����Ʈ�� �߰�
            saveData.Add(record);
        }


        Ranking_Result();
    }

    private void Ranking_Result()
    {

        //List�� ��� ���� - �迭�� ��� score�� ���� ��� �ű���
        //��Ȱ���� �� ��ŷ UI Text�� ����ְ�, Ȱ��ȭ���� (5������)
        
        string[] ranking_n = new string[saveData.Count];
        int[] ranking_s = new int[saveData.Count];

        //�迭 ä���ֱ�
        for(int i=0; i<saveData.Count; i++)
        {
            ranking_n[i] = saveData[i].name;
            ranking_s[i] = saveData[i].score;
        }

        //�����ű�
        if(saveData.Count > 1)
        {
            string temp_n;
            int temp_s;
            for (int j = 0; j < saveData.Count - 1; j++)
            {
                for (int i = 0; i < saveData.Count - 1; i++)
                {
                    //i+1 > i �ּ��� ������ Ŭ��� �ڸ��ٲٱ� (ū���� ��������)
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
        

        //5�� �̻������ UI���� �����ֱ�
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


        //List saveData ������Ʈ
        saveData.Clear();
        for(int i=0; i<ranking_n.Length; i++)
        {
            saveData.Add(new Record
            {
                name = ranking_n[i],
                score = ranking_s[i]
            });
        }

        //5�� �Ʒ� �ڸ���
        while (saveData.Count > 5)
        {
            saveData.RemoveAt(5);
        }


        File_Update();

    }
    

    private void File_Update()
    {
        //����Ʈ�� ����� ������ string����
        ArryData arryData = new ArryData
        {
            records = saveData.ToArray()
        };

        string jsonData = JsonUtility.ToJson(arryData);

        File.WriteAllText(path_Android, jsonData);

        print("���� ������Ʈ : " + jsonData);
    }


    private string File_Read()
    {
        //���Ͽ� ����� ������ string����
        string jsonData = File.ReadAllText(path_Android);

        print("���� �ҷ����� : " + jsonData);

        return jsonData;

    }

}
