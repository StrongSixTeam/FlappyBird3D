using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMove : MonoBehaviour
{
    public Transform[] pipes;

    float xPos;

    private void Start()
    {
        PipeControll();
    }


    public void PipeControll()
    {
        for (int i = 0; i < pipes.Length; i++)
        {
            xPos = pipes[i].localPosition.x;
            pipes[i].localPosition = new Vector3(xPos, Random.Range(0f, 0.8f), 0);
        }
    }
}
