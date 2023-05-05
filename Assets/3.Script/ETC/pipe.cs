using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pipe : MonoBehaviour
{
    public void Hide()
    {
        gameObject.SetActive(false);
        Invoke("Active", 4f);
    }
    private void Active()
    {
        gameObject.SetActive(true);
    }
}
