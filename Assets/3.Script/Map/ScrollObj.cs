using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollObj : MonoBehaviour
{
    public float speed = 1.5f;

    void Update()
    {
        if (true)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime * 0.5f);
        }
    }

}
