using System.Collections;
using System;
using UnityEngine;

using UnityEngine.UI;

namespace VGF.UI
{
    public class CaptionAnimationEvent : MonoBehaviour
    {

        private static CaptionAnimationEvent _instance;

        public static CaptionAnimationEvent instance{
            get{
                return _instance;
            }
        }
        //public GameObject RequireClosing;
        private float seconds =>CaptionLoader.instance.currentCaptionPiece.seconds;
        private Action callback =>CaptionLoader.instance.currentCaptionPiece.callback;
        private Animator animator =>GetComponent<Animator>();
        
        public Coroutine currentCoroutine;

        private void Awake() {
            if(_instance!=null)
                Destroy(_instance);
            _instance=this;

            gameObject.SetActive(false);
        }
        public void AnimationEnd()
        {


            gameObject.SetActive(false);
            CaptionLoader.instance.isPlaying=false;
            
            //执行Caption的回调
            if(callback!=null)
                callback?.Invoke();

            //这个NEXT必须在回调之后，不然会先Next，导致currentCaptionPiece被修改，callback执行的是下一个piece的回调
            CaptionLoader.instance.currentCaptionPiece = new CaptionPiece();
            CaptionLoader.instance.Next();


            
        }
        private IEnumerator Wait(float seconds,System.Action action){
            yield return new WaitForSecondsRealtime(seconds);
            action();
        }


        void AnimationStay(){
            currentCoroutine= StartCoroutine(Wait(seconds,()=>{
                animator.SetTrigger("Finish");
            }));
        }
    }
}
