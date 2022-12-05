using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace VGF.UI
{
    public class CaptionLoader : MonoBehaviour
    {

        private static CaptionLoader _instance;
        public static CaptionLoader instance
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

        }


        public Queue<CaptionPiece> captions=new Queue<CaptionPiece>();

        public CaptionPiece currentCaptionPiece;
        public CaptionPiece[] test;

        public Image CaptionPanel;

        /// <summary>
        /// Caption是否正在放映
        /// </summary>
        public bool isPlaying;

        public void Next(){
            //判断当前是否有Caption
            if(captions.Count==0){
                isPlaying=false;
                return;
            }
            
            //激活面板
            CaptionPanel.gameObject.SetActive(true);
            isPlaying=true;

            //记录
            currentCaptionPiece=captions.Dequeue();
            CaptionPanel.GetComponentInChildren<Text>().text=currentCaptionPiece.content;
            
        }

        public void Push(string content,float seconds=0f,Action callback=null){
            captions.Enqueue(new CaptionPiece{content=content,seconds=seconds,callback=callback});
            if(!isPlaying)
                Next();
        }



        private void Update() {
            test=captions.ToArray();

            if(!isPlaying)
                Next();
        }

        public void Stop(){

            isPlaying=false;
            
            captions.Clear();
            
            CaptionLoader.instance.currentCaptionPiece = new CaptionPiece();
            CaptionAnimationEvent.instance.AnimationEnd();
            
            try
            {
                StopCoroutine(CaptionAnimationEvent.instance.currentCoroutine);
            }
            catch{}
            
            CaptionAnimationEvent.instance.currentCoroutine=null;
            CaptionAnimationEvent.instance.gameObject.SetActive(false);

        }
    }

    [System.Serializable]
    public struct CaptionPiece{
        
        public string content;
        public float seconds;

        public Action callback;
    }

}