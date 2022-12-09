using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using VGF.Assignment;
using VGF.UI;
using VGF.SceneSystem;
using VGF.AreaNameSpace;
using VGF.NPC;
using VGF.Dialogue;

namespace VGF.Plot
{
    
    public abstract class ChapterBase:Singleton<ChapterBase>{


        private bool isPlaying;
        public bool IsPlaying{
            get{
                return isPlaying;
            }
        }
        /// <summary>
        /// 游戏剧情入口
        /// </summary>
        public abstract void Run();

        /// <summary>
        /// 委派任务
        /// </summary>
        /// <param name="assignment"></param>
        public void Assign(Assignment_SO assignment){
            if(assignment==null){
                Debug.LogError("<b>委派了空任务</b>");
                return;
            }

            AssignmentManager.Instance.assignments.Add(assignment);
        }

        /// <summary>
        /// 委派任务，但回调
        /// </summary>
        /// <param name="assignment"></param>
        /// <param name="action"></param>
        public void Assign(Assignment_SO assignment,Action<AssignmentCallBackMessagee> action){
            Assign(assignment);
            AssignmentManager.Instance.assignmentsCallBackDict.Add(assignment,action);
    
        }




        /// <summary>
        /// 显示黑色文本
        /// </summary>
        /// <param name="content"></param>
        public void Caption(string content,float seconds=1f,Action callback=null){
            CaptionLoader.instance.Push(content,seconds,callback);
        }

        [Obsolete]
        public void InstantCaption(string content,float seconds=1f,Action callback=null){
            CaptionLoader.instance.Stop();
            
            Caption(content,seconds,callback);
        }

        public void CapitionEmpty(){
            CaptionLoader.instance.Stop();
        }
        /// <summary>
        /// 切换场景
        /// </summary>
        /// <param name="name"></param>
        private void Scene(string name,Action callback=null){
            SceneLoader.instance.SwitchSceneByName(name,callback);
        }
        
        /// <summary>
        /// 切换场景并把角色转移到某点
        /// </summary>
        /// <param name="name"></param>
        /// <param name="point">//传的点不能用DEFAULT不是参数默认值而是https://wenku.baidu.com/view/e9d49b55f142336c1eb91a37f111f18582d00c72.html</param>
        /// <param name="action"></param>
        public void SceneMoveThen(string name,string point="Default Point",Action action=null){
            //Player.instance.transform.position=AreaManager.instance.GetPointByName(point).transform.position;  //在加载前移动位置

            Scene(name,()=>{

                Debug.Log("<color=green>场景切换完成,玩家将前往点"+point+"</color>");
                //Player.instance.ForceMoveToPosition(AreaManager.instance.GetPointByName(point).transform.position);
                Player.instance.transform.localPosition=NewAreaManager.Instance.FindPointByID(point).Value.position;  //在加载后移动位置

                Player.instance.Mute=false;

                if(action!=null)
                    action();

            });
            //
        }
        
        /// <summary>
        /// 通过GameObject的名字获取NPC，NPC需要挂在NPC Componment和NPC tag（tag会自动挂载，如果有NPC Componment）
        /// </summary>
        /// <param name="name"></param>
        /// <returns>NPC:MonoBehaviour</returns>
        public NPC.NPC NPC(string name){
            var tmp=NPCManager.instance.GetNPCByName(name);
            // if(!tmp)
            //     Debug.LogError("<b>Chapter中找不到NPC</b>");
            return tmp;
                 
        }


        /// <summary>
        /// 只允许说一段对话
        /// </summary>
        /// <param name="npc"></param>
        /// <param name="lines">params标记实现不定量形参</param>
        /// <returns>创建对话的SO实例</returns>
        [Obsolete]
        public Dialogue_SO OnlySay(NPC.NPC npc,params string[] lines){
            return npc.npcITellYou.OnlyOneDialogue(lines);
        }
        [Obsolete]
        public Dialogue_SO OnlySay(string npc,params string[] lines){
            return OnlySay(NPC(npc),lines);
        }


        public void Say(string npc,string lineStr,Action action){  //最最最最常用;希望用户使用的
            List<string> list=new List<string>();
            foreach(var item in lineStr.Split("\n")){
                if(item.Trim()!="")
                    list.Add(item.Trim());
            }
            var so=Say(NPC(npc),overlap:true,list.ToArray());
            AfterSay(so,action);
        }
        public Dialogue_SO Say(string npc,string lineStr){  //最常用
            List<string> list=new List<string>();
            foreach(var item in lineStr.Split("\n")){
                if(item.Trim()!="")
                    list.Add(item.Trim());
            }
            return Say(NPC(npc),overlap:true,list.ToArray());
        }
        public Dialogue_SO Say(string npc,params string[] lines){   //最常用
            return Say(NPC(npc),overlap:true,lines);
        }
        public Dialogue_SO Say(string npc,bool overlap=true,params string[] lines){   
            return Say(NPC(npc),overlap:overlap,lines);
        }
        public Dialogue_SO Say(NPC.NPC npc,bool overlap=true,params string[] lines){
            return Say(npc,overlap:overlap,NPCITellYou.LinesToSO(lines));
           // return npc.npcITellYou.PushDialogue(lines);
        }
    
        
        public Dialogue_SO Say(NPC.NPC npc,bool overlap=true,Dialogue_SO dialogue=null,int times=1){
            if(overlap)
                npc.npcITellYou.Clear(1);
            npc.npcITellYou.PushDialogue(dialogue,times);
            if(npc.isPlayer)
                npc.npcITellYou.Interact();
            
            return dialogue;
        }
        public Dialogue_SO Say(string npc,bool overlap=true,Dialogue_SO dialogue=null,int times=1){
            return Say(NPC(npc),overlap:overlap,dialogue,times);
        }

        

        /// <summary>
        /// 删除NPC的所有对话内容以及委托字典
        /// </summary>
        /// <param name="npc"></param>
        public void SayEmpty(NPC.NPC npc){
            npc.npcITellYou.Clear();
        }
        public void SayEmpty(string npc){
            SayEmpty(NPC(npc));
        }

        /// <summary>
        /// 绑定NPC说话后效果（委托）
        /// </summary>
        public void AfterSay(Dialogue_SO d,Action action){
            DialogueManager.instance.Bind(d,action);
        }


        /// <summary>
        /// 切换下一章节
        /// </summary>
        public void VGF(){
            Debug.LogWarning("<color=orange>切换到下一章</color>");
            PlotManager.instance.NextChapter();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        protected void Timeline(string name){
            EventHandler.PlayTimelineInvoke(name);
            
        }
        protected void PlayTimeline(string name){
            EventHandler.PlayTimelineInvoke(name);
        }
        ///
        protected void Timeline(string name,Action action){
            if(name==string.Empty)
                action?.Invoke();
            else
                EventHandler.PlayTimelineInvoke(name,action);
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        protected void PlayMusic(string name){
            EventHandler.PlayMusicInvoke(name);
        }


        // void SelfSay(){
        //     var playeritellyou= Player.instance.GetComponent<NPCITellYou>();
        //     playeritellyou.OnlyOneDialogue();
        //     playeritellyou.Interact();
        // }

        /// <summary>
        /// 等待一段时间并执行
        /// </summary>
        /// <param name="seconds"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        protected Coroutine WaitThen(float seconds,Action action){
            return StartCoroutine(IEnumeratorWaitThen(seconds,action));
        }

        IEnumerator IEnumeratorWaitThen(float seconds,Action action){
            yield return new WaitForSecondsRealtime(seconds);
            action?.Invoke();
        }


        protected void Arrival(string name,Action<AssignmentCallBackMessagee> action){
            var tmp = ScriptableObject.CreateInstance<Arrival_SO>();
            tmp.Area=name;
            Assign(tmp,action);
        }
    }

  
}