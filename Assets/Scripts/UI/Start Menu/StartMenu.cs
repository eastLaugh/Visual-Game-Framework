using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace VGF.SceneSystem
{
    public class StartMenu : MonoBehaviour
    {
        public void Start()
        {
            //Time.timeScale = 0f;
            //SceneManager.LoadScene("DontDestroyOnLoadData",LoadSceneMode.Additive);
        }
        public void onClickStartStory()
        {
            //SceneManager.UnloadSceneAsync(0);
            SceneManager.LoadSceneAsync("Persistent Scene");
            //Time.timeScale = 1f;
        }
        public void onClickExit()
        {
            Application.Quit();
        }

    }
}
