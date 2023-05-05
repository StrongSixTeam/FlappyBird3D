using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudHeight : MonoBehaviour
{
    
    float xPos;

    public void CloudControll()
    {


        xPos = transform.localPosition.x;
            transform.localPosition = new Vector3(xPos,Random.Range(0.3f,1.0f),0.7f);
        
    }


}
