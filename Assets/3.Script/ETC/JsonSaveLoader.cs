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

    //������
    public Record(string name, int score)
    {
        this.name = name;
        this.score = score;
    }
}

public class JsonSaveLoader : MonoBehaviour
{

    List<Record> saveData = new List<Record>();

    [Header("��ŷ UI Text")]
    [SerializeField] private GameObject[] nameText = new GameObject[5];
    [SerializeField] private GameObject[] scoreText = new GameObject[5];

    private void Start()
    {
        for(int i=0; i<5; i++)
        {
            nameText[i].SetActive(false);
            scoreText[i].SetActive(false);
        }


        //���̺����Ͽ� ����� ����� �ִٸ�
        //List saveData�� ������Ʈ�մϴ�
        if (File.Exists("/PBsave.json"))
        {
            Debug.Log("����� ����� �����մϴ�");
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
            //Ȯ�ο�
            saveData.Add(new Record("A", 10));
            saveData.Add(new Record("B", 6));
            saveData.Add(new Record("C", 0));
            saveData.Add(new Record("D", 5));
            saveData.Add(new Record("E", 3));
            saveData.Add(new Record("F", 120));

        }


    }

    //�÷��̾��� ����� ����
    public void Save_Record(Record record)
    {
        //���� ����� ����Ʈ�� �߰�
        saveData.Add(record);

        //Record -> string ��ȯ
        string jsonData = JsonConvert.SerializeObject(saveData);
        print("List ��� : " + jsonData);

        //jsonData(string)�� ���Ϸ� ����
        //File.WriteAllText(Application.dataPath + "/PBsave.json", jsonData);
        File.WriteAllText(Application.persistentDataPath + "%userprofile%/AppData/LocalLow/FlappyBird3D/PBsave.json", jsonData);
        //File.WriteAllText(Application.persistentDataPath + "/mnt/sdcard/Android/data/FlappyBird3D/files/PBsave.json", jsonData);

        //��� ��
        Load_Record();
    }


    public void Load_Record()
    {
        string jsonData = File.ReadAllText(Application.persistentDataPath + "%userprofile%/AppData/LocalLow/FlappyBird3D/PBsave.json");
        //string jsonDataA = File.ReadAllText(Application.persistentDataPath + "/mnt/sdcard/Android/data/FlappyBird3D/files/PBsave.json");
        print("���� : " + jsonData);


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
        

        //5�� �̻������ �����ֱ�
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
            saveData.Add(new Record(ranking_n[i], ranking_s[i]));
        }

        //5�� �Ʒ� �ڸ���
        while (saveData.Count > 5)
        {
            saveData.RemoveAt(5);
        }


        //���� ������Ʈ
        jsonData = JsonConvert.SerializeObject(saveData);
        print("���� : " + jsonData);
        File.WriteAllText(Application.persistentDataPath + "%userprofile%/AppData/LocalLow/FlappyBird3D/PBsave.json", jsonData);
        //File.WriteAllText(Application.persistentDataPath + "/mnt/sdcard/Android/data/FlappyBird3D/files/PBsave.json", jsonData);

    }

}
