using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

class Record
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

    Record record = new Record("temp_name", 0);

    private void Start()
    {
        //temp_name�� Intro������ �Է¹��� �̸����� ��ü
        //record.name = "temp";

        //���ӿ��� �� 
        //record.score = GameManager.Instance.score;

        //����� �����������ͷ�
        string jsonData = JsonConvert.SerializeObject(record);
        print("��� : " + jsonData);
    }
}
