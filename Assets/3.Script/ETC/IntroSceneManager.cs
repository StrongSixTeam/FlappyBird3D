using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroSceneManager : MonoBehaviour
{
    public InputField playerName;
    
    public void Gamestart()
    {
        PlayerPrefs.SetString("PlayerName", playerName.text);
        SceneManager.LoadScene("SampleScene");
    }
}
