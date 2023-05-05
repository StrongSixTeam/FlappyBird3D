using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMove : MonoBehaviour
{
    public Transform[] pipes;

    float xPos;




    public void PipeControll()
    {
        for (int i = 0; i < pipes.Length; i++)
        {
            pipes[i].GetChild(0).GetChild(0).gameObject.SetActive(false);
            pipes[i].gameObject.SetActive(true);

            xPos = pipes[i].localPosition.x;
            pipes[i].localPosition = new Vector3(xPos, Random.Range(-0.5f, 0.4f), 0);
        }
    }

}
