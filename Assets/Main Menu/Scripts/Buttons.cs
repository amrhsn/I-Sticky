using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
   
    public GameObject[] showHide;
   

    bool soundFlag = false;
    bool showHideMenu = false;


    public void StartGame(string sceneName)
    {
        PlayerPrefs.SetString("SceneName", sceneName);
        PlayerPrefs.Save();

        //SceneManager.LoadScene("Loading Scene1");
       // SceneManager.LoadScene("Loading Scene1");
        //Invoke("LoadScene2", 1f);
    }

    public void LoadScene(string sceneName)
    {
        if(!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
    
    /*public void LoadScene2()
    {
        SceneManager.LoadScene("Loading Scene2");
    }*/

    public void Return ()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void ExitGame ()
    {
        Application.Quit();
    }

    public void ShowHideMenu ()
    {
        if (showHideMenu == false)
        {
            foreach (GameObject i in showHide )
            {
                i.SetActive(true);
            }
        
            showHideMenu = true;
        }
        else
        {
            foreach (GameObject i in showHide)
            {
                i.SetActive(false);
            }

            showHideMenu = false;
        }
    }
    public void DownloadURL()
    {
        Application.OpenURL("https://www.google.com.eg/");
    }
    public void WebURL()
    {
        Application.OpenURL("https://play.google.com/store");
    }
}