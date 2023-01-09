using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VGF.SceneSystem;
using static Unity.Burst.Intrinsics.X86.Avx;

namespace VGF.SceneSystem
{
   public class Loading : MonoBehaviour
   {
       public Text progress;
       private float progressvalue;
       public Slider Slider;
       private AsyncOperation async = null;
       public bool isLoading;
       
       private void Awake()
        {
            progress = GetComponentInChildren<Text>();
            Slider = GetComponentInChildren<Slider>();
            //Player.instance.characterController.enabled = false;
            isLoading = false; 
            StartCoroutine("LoadPersistentScene");
        }

        IEnumerator LoadPersistentScene()
        {
            //Debug.Log(SceneData.instance.backGroudScene);
            SceneManager.UnloadSceneAsync(0);
            async = SceneManager.LoadSceneAsync("Persistent Scene");
            async.allowSceneActivation = false;
            while(!async.isDone)
            {
                if (async.progress < 0.9f)
                    progressvalue = async.progress;
                else
                    progressvalue = 1.0f;
                Slider.value = progressvalue;
                progress.text = (Slider.value * 100).ToString() + "%";
                if (progressvalue > 0.9f)
                {
                    progress.text = "Press Any Key to Continue...";
                    if (Input.GetKeyDown(KeyCode.H))
                    {
                        async.allowSceneActivation = true;
                        isLoading = true;
                        Debug.Log(isLoading);
                        yield return null;
                    }
                    if (isLoading)
                    {
                        //SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneData.instance.backGroudScene));
                        //Player.instance.characterController.enabled = true;
                        //gameObject.SetActive(false);
                        isLoading = false;
                        //if (SceneData.instance.saveAction != null)
                        //{ 
                        // SceneData.instance.saveAction();
                        SceneManager.UnloadSceneAsync("Loading");
                        yield break;
                        //}


                    }

                }
                yield return null;
            }
            
        }
    }
 }