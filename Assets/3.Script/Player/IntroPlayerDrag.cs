using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroPlayerDrag : MonoBehaviour
{
    private Vector3 pos;
    
    private void Start()
    {
        pos = transform.position;
    }
    void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y, 1.5f);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;
    }
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            transform.position = pos;
        }
    }
}
