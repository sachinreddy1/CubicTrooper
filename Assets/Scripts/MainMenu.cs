using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public SceneFader sceneFader;

    void Start() {

    }

    public void Play() {
        LoadLevel("Level_01");
    }   

    public void LoadLevel(string levelName){
        sceneFader.FadeTo(levelName);
    }


    public void Quit() {
        Debug.Log("Quit.");
        Application.Quit();
    }
}
