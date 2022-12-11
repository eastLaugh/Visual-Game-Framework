using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace VGF.Plot
{

    public class PlotManager : MonoBehaviour
    {

        private static PlotManager _instance;
        public static PlotManager instance
        {
            get
            {
                return _instance;
            }
        }

        private void Awake()
        {
            if (instance != null)
                Destroy(instance);
            _instance = this;

        }


        //public List<Plot> plots = new List<Plot>();
        //public PlotOption[] plots;
        
        public int index=0;
        public ChapterBase[] chapters;

        public void Run(){
            Debug.Log("<b>Run</b>");


            //BUG002 增加这行后会连续执行两次   2022.9.21似乎已修复（？）
            VGF.UI.CaptionLoader.instance.Stop();
            

            chapters[index].Run();                  //TODO耦合性太高，可以用EventHandler优化


        }

        private void Start() {
            Run();
        }


        public void NextChapter(){
            index++;
            
            if(index>=chapters.Length){
                Debug.LogError("Game Done!!!  index>=chapters.Length ");
                End();
                return;
            }
            


            
            Run();
        }

        void End(){

            //结束游戏

            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying=false;
            #else
                Application.Quit();
            #endif
            
        }
    }

    

    
}