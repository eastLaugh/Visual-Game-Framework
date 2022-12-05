using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UnityEngine.UI;

namespace VGF.Dialogue
{
    public class DialogueManager : MonoBehaviour
    {

        private static DialogueManager _instance;
        public static DialogueManager instance
        {
            get
            {
                return _instance;
            }
        }


        void Awake()
        {

            if (_instance != null)
                Destroy(_instance);
            _instance = this;

            DialogueCanvas.gameObject.SetActive(false);
            
        }

        public Dictionary<Dialogue_SO, Action> dialoguesCallBackDict = new Dictionary<Dialogue_SO, Action>();
        public void Bind(Dialogue_SO d, Action a)
        {
            if (!dialoguesCallBackDict.ContainsKey(d))  //防止报错
                dialoguesCallBackDict.Add(d, a);
        }

        [SerializeField]
        private Dialogue_SO currentDialogue;

        private int index;

        private Coroutine currentCoroutine;

        public bool isChatting{
            get{
                return currentDialogue!=null;
            }
        }

        public void Run(Dialogue_SO dialogue)
        {
            currentDialogue = dialogue;
            index=-1;
            
            DialogueCanvas.gameObject.SetActive(true);
            Player.instance.Mute=true;
            // Player.instance.characterController.enabled=false;
            
            Next();

        }

        

        //UI
        public Canvas DialogueCanvas;
        private Image DialoguePanel=>DialogueCanvas.GetComponentInChildren<Image>();
        private Text DialogueText=>DialogueCanvas.GetComponentInChildren<Text>();

        private string text=>DialogueText.text;
        /// <summary>
        /// 点击PANEL时调用
        /// </summary>
        private void Next(){
            
            if(currentDialogue==null)
                return;

            //加载中跳过 [SKIP]
            if(currentCoroutine!=null){
                StopCoroutine(currentCoroutine);
                currentCoroutine=null;
                if(index<currentDialogue.dialoguePieces.Count)
                    DialogueText.text=currentDialogue.dialoguePieces[index].Content;
                return;
            }


            index++;
            if(index>=currentDialogue.dialoguePieces.Count){
                End();
                return;
            }

            var currentDialoguePiece=currentDialogue.dialoguePieces[index];

            

            //TODO APPEND = TRUE

            DialogueText.text="";
            currentCoroutine=StartCoroutine(Type(currentDialoguePiece.Content,currentDialoguePiece.Time));
        }

        IEnumerator Type(string content,float time){
            
            for(int i=1;i<=content.Length;i++){
                if(_auto)
                    yield return new WaitForSecondsRealtime(time/(float)content.Length);
                else
                    yield return new WaitForSecondsRealtime(0.1f);
                GetComponent<AudioSource>().Play();
                //DialogueText.text=content.Substring(0,i)+new String(' ',(content.Length-i)*2);
                 DialogueText.text=content.Substring(0,i);
            }
            currentCoroutine=null;
            if(_auto){
                Next();
            }
        }

        /// <summary>
        /// 一个Dialogue_SO结束后调用的
        /// </summary>
        void End(){
            Debug.Log("<color=green>对话结束</color>");
            
            IsTimeline=false;//不管是不是Timeline的对话系统，都可以关掉timeline bool

            if(dialoguesCallBackDict.ContainsKey(currentDialogue))
                dialoguesCallBackDict[currentDialogue]?.Invoke();  //可选调用

            
            Player.instance.Mute=false;
            // Player.instance.characterController.enabled=true;
            currentDialogue=null;

            DialoguePanel.GetComponent<Animator>().SetTrigger("Fade");
            //nmaitonEnd();
            //
            //TODO↑目前技术不够无法解决的问题，希望能使得动画播放完毕后回到第一帧，并且执行AnmationEnd 相关解决方案 State  machine behaviour和anmation event均不太满意

            //snapchat已在NPCItellyou中实现


        }
        /// <summary>
        /// BUG003 TODO
        /// </summary>
        public void AnmaitonEnd(){
            DialogueCanvas.gameObject.SetActive(false);
        }
        private void Update() {
            //空格键也可以触发NEXT
            // Debug.Log(Input.GetKeyDown(KeyCode.Space));
            // if(Input.GetKeyDown(KeyCode.Space))
            //     Debug.Log("SPACE DOWN");
            if(Input.GetKeyDown(KeyCode.Space)&&currentDialogue&&!Auto)
                Next();
        }

        private void OnEnable() {
            EventHandler.TimelinePlayDialogue+=TimelinePlayDialogue;
        }

        private void OnDisable() {
            EventHandler.TimelinePlayDialogue-=TimelinePlayDialogue;
        }
        /// <summary>
        /// 从Timeline中播放即使的Dialogue对话
        /// </summary>
        /// <param name="dialogue"></param>
        void TimelinePlayDialogue(Dialogue_SO dialogue){
            IsTimeline=true;
            Run(dialogue);
        }

        public bool IsTimeline=false;

        private bool _auto;
        public bool Auto{
            get=>_auto;
            set{
                _auto=value;
            }
        }
    }
}