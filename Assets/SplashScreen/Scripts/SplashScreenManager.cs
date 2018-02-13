using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreenManager : MonoBehaviour {

    [SerializeField]
    private Image fill;
    private float fillAmount = 0.2f;
    AsyncOperation async;
    public string sceneName;
    void Start ()
    {
        Invoke("LoadScene", 5f);
    }
	
	void Update ()
    {
        UpdateBar();
    }
    private void UpdateBar()
    {
        fill.fillAmount = fillAmount*Time.fixedTime;
    }
    void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
