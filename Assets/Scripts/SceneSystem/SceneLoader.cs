// using UnityEngine.SceneManagement;
                                    //代码在下面代码在下面代码在下面代码在下面代码在下面代码在下面代码在下面代码在下面代码在下面代码在下面代码在下面代码在下面代码在下面

// using System.Collections.Generic;
// using UnityEngine;

// using System;

// namespace VGF.SceneSystem
// {
//     /// <summary>
//     /// 防止与UnityEngine.SceneManagement.SceneManager重名
//     /// </summary>
//     public class SceneLoader : MonoBehaviour
//     {
//         [Header("过渡画布")]
//         public Canvas TransitionCanvas;


//         private static SceneLoader _instance;
//         public static SceneLoader instance
//         {
//             get
//             {
//                 return _instance;
//             }
//         }

//         private void Awake()
//         {
//             if (_instance != null)
//                 Destroy(_instance);
//             _instance = this;

//             //然后加载初始设定的场景
//             //SwitchSceneByName(CurrentScene);
//             //↑已废弃，加载场景的操作应交由Chapter来执行

//         }
//         private void Start()
//         {





//         }

//         //public string CurrentScene = string.Empty;
//         public void SwitchSceneByName(string name, Action action = null)
//         {
            
//             Player.instance.Mute=true;
//             //Transition
//             TransitionCanvas.enabled=true;


//             StartCoroutine(LoadSceneSetActive(name, () =>
//             {

//                 //场景异步加载成功后

                


//                 //卸载当前场景，并换上新的场景名称
//                 // if (name != CurrentScene)
//                 // {
//                 //     SceneManager.UnloadSceneAsync(CurrentScene);
//                 //     CurrentScene = name;
//                 // }

               
//  //游戏AWAKE阶段，先卸载除了PersistentScene以外的所有场景  -- 2022.9.12无法实现
//                 for (int i = 1; i < SceneManager.sceneCount; i++)   //从1开始是为了避开PersistentScene
//                 {
//                     var tmp = SceneManager.GetSceneAt(i);
//                     if (tmp != SceneManager.GetActiveScene())
//                         SceneManager.UnloadSceneAsync(tmp);
//                 }


                

                

//                 //执行回调的回调
//                 if (action != null)
//                     action();


//                 //Player.instance.Mute=false;
                
                

//             }));


//         }



//         private IEnumerator LoadSceneSetActive(string name, Action action = null)
//         {
//             yield return SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);

//             TransitionCanvas.enabled=false;

//             SceneManager.SetActiveScene(SceneManager.GetSceneByName(name));
//             if (action != null)
//                 action();

            
//         }

//         private void OnEnable()
//         {
//             SceneManager.sceneLoaded += OnSceneLoaded;
//         }

//         private void OnDisable()
//         {
//             SceneManager.sceneLoaded -= OnSceneLoaded;
//         }
//         void OnSceneLoaded(Scene scene, LoadSceneMode mode)
//         {

//         }
//     }
// }
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace VGF.SceneSystem
{
    /// <summary>
    /// 防止与UnityEngine.SceneManagement.SceneManager重名
    /// </summary>
    public class SceneLoader : MonoBehaviour
    {
        [Header("过渡画布")]
        public Canvas TransitionCanvas;
        public Text progress;
        private float progressvalue;
        public Slider Slider;
        private AsyncOperation async = null;

        private List<AsyncOperation> UnloadAsyncList=new List<AsyncOperation>();
        private Action UnloadCallback;

        private static SceneLoader _instance;
        public static SceneLoader instance
        {
            get
            {
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance != null)
                Destroy(_instance);
            _instance = this;

            //然后加载初始设定的场景
            //SwitchSceneByName(CurrentScene);
            //↑已废弃，加载场景的操作应交由Chapter来执行

        }
        private void Start()
        {





        }

        private void Update() {
            CheckUnloaded();
        }


        void CheckUnloaded(){
            // if(!(UnloadAsyncList.Count>0))
            //     return;
            bool unloaded=true;
            foreach(var unloadAsync in UnloadAsyncList){
                if(!unloadAsync.isDone)
                    unloaded=false;
            }
            if(unloaded){
                UnloadAsyncList.Clear();
                UnloadCallback?.Invoke();
                UnloadCallback=null;
            }else{
                Debug.LogWarning("场景卸载中...");
            }
        }
        //public string CurrentScene = string.Empty;
        public void SwitchSceneByName(string name, Action action = null)
        {
            
            Player.instance.Mute=true;
            //Transition
            TransitionCanvas.enabled=true;

            Debug.LogFormat("<color=green>场景{0}开始加载</color>",name);
            StartCoroutine(LoadSceneSetActive(name, () =>
            {

                //场景异步加载成功后

                


                //卸载当前场景，并换上新的场景名称
                // if (name != CurrentScene)
                // {
                //     SceneManager.UnloadSceneAsync(CurrentScene);
                //     CurrentScene = name;
                // }

               
 //游戏AWAKE阶段，先卸载除了PersistentScene以外的所有场景  -- 2022.9.12无法实现
                for (int i = 1; i < SceneManager.sceneCount; i++)   //从1开始是为了避开PersistentScene
                {
                    var tmp = SceneManager.GetSceneAt(i);
                    if (tmp != SceneManager.GetActiveScene())
                        UnloadAsyncList.Add(SceneManager.UnloadSceneAsync(tmp));
                }


                UnloadCallback = ()=>{
                    action?.Invoke();

                    TransitionCanvas.enabled = false;
                    Player.instance.Mute=false;
                };
                //执行回调的回调
                //if (action != null)
                     //action();
                    
                

                

            }));


        }



        private IEnumerator LoadSceneSetActive(string name, Action action = null)
        {
            bool isLoadingEnd = false;
            //yield return SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
            async = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
            async.allowSceneActivation = false;
            while (!async.isDone)
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
                    
                    if (Input.anyKeyDown)
                    {
                        async.allowSceneActivation = true;
                        isLoadingEnd = true;
                        yield return null;  //等待一帧执行后面的语句。
                    }
                    if (isLoadingEnd)
                    {
                        SceneManager.SetActiveScene(SceneManager.GetSceneByName(name));
                        isLoadingEnd = false;
                        
                    }
                }
                yield return null;
            }
            if (action != null)
            { action(); yield break; }

            
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {

        }
    }
}