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

        // List saveData ������Ʈ
        // -> ���̺����Ͽ��ִ³����� saveData ����Ʈ�� ����
        //string���� �ɰ��� ����


    }

    //�÷��̾��� ����� ����
    public void _saveData(Record record)
    {
        //���� ����� ����Ʈ�� �߰�
        saveData.Add(record);

        //Record -> string ��ȯ
        string jsonData = JsonConvert.SerializeObject(saveData);
        print("��� : " + jsonData);

        //jsonData(string)�� ���Ϸ� ����
        File.WriteAllText(Application.dataPath + "/PBsave.json", jsonData);
    }


    public void _loadData()
    {
        string jsonData = File.ReadAllText(Application.dataPath + "/PBsave.json");
        
        //if ���� 5�� ���ϰ� �ִٸ� ��� ���� (����Ʈ���� ���� �� ���� ������Ʈ)

        //string�� �ɰ�
        //name�� score �迭�� ����
        //socre ������� �����ϰ� ������ ���󰡰� ����
        //��Ȱ���� �� ��ŷ UI Text�� ����ְ�, Ȱ��ȭ����

    }

}
