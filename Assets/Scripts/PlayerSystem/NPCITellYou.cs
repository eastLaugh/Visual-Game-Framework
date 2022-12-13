using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

using VGF.Dialogue;

namespace VGF.NPC
{
    public class NPCITellYou : MonoBehaviour
    {
        // [HideInInspector]
        // public List<Dialogue_SO> defaultDialogues = new List<Dialogue_SO>();


        [SerializeField]
        private List<Dialogue_SO> dialogues = new List<Dialogue_SO>();

        [SerializeField]
        private Dialogue_SO currentDialogue;

   
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Interact()
        {
            
            if(dialogues.Count==0){
                Debug.Log(gameObject.name+"无话可说");
                return;
            }


            Debug.LogWarningFormat("<color=organe>对话 主体：{0}</color>",gameObject.name);
            //随机选取对话
            currentDialogue=dialogues[UnityEngine.Random.Range(0,dialogues.Count)];

            //→对话系统
            DialogueManager.Instance.Run(currentDialogue);

            //回调
            //DialogueManager.Instance.dialoguesCallBackDict[currentDialogue]();
            //TODO上面这个回调应该放在对话桔束之后，或者干脆放两个回调，一个放这里，一个放对话结束之后hhh

            //阅后即焚
            if(currentDialogue.Snapchat){
                DialogueManager.Instance.dialoguesCallBackDict.Remove(currentDialogue);
                dialogues.Remove(currentDialogue);
            }


        }


        public void OnlyOneDialogue(Dialogue_SO dialogue_SO)
        {
            dialogues = new List<Dialogue_SO>();
            dialogues.Add(dialogue_SO);

        }

        public Dialogue_SO OnlyOneDialogue(params string[] lines)
        {
            var tmp = LinesToSO(lines);
            OnlyOneDialogue(tmp);
            return tmp;
        }

        /// <summary>
        /// 将params标记的多行string参数转换为Dialogue_SO类型，方便DialogueSystem操控
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static Dialogue_SO LinesToSO(params string[] lines)
        {
            var tmp = ScriptableObject.CreateInstance<Dialogue_SO>();

            foreach (var line in lines)
            {
                tmp.dialoguePieces.Add(new DialoguePiece { Content = line });
            }

            return tmp;
        }


        public void PushDialogue(Dialogue_SO dialogue_SO,int times=1){            
            while(times>0){
                times--;
                dialogues.Add(dialogue_SO);
            }
                
        }

        public Dialogue_SO PushDialogue(params string[] lines){
            var tmp=LinesToSO(lines);
            PushDialogue(tmp);
            return tmp;
        }

        public void Clear(int style=0){
            dialogues.Clear();
            if(style==0)
                DialogueManager.Instance.dialoguesCallBackDict.Clear();
        }
    }
}