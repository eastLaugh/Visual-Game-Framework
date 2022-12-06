
using UnityEngine;
using UnityEngine.Timeline;

using UnityEngine.Playables;
using System.Collections.Generic;

[System.Serializable]
public class DialogueBehaviour : PlayableBehaviour
{
    public DialogueClip clip;
    public PlayableDirector director;

    /// <summary>
    /// 该片段已被播放过
    /// </summary>
    private bool isPlayed=false;

    public override void OnPlayableCreate(Playable playable)
    {
//        Debug.Log("");
        base.OnPlayableCreate(playable);
        director = playable.GetGraph().GetResolver() as PlayableDirector;
        isPlayed=false;
        //Debug.Log(playable.GetDuration());这个不能放在creat里，只能放在Play里，也不知道为啥
        // Debug.Log(clip.duration);得到的一直是Inf不知道为啥
        
    }
    public override void OnPlayableDestroy(Playable playable)
    {
        base.OnPlayableDestroy(playable);
    }

    //在播放时实际启用一次，也就是我们想要的最终效果
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        base.OnBehaviourPlay(playable, info);
        if(isPlayed||!Application.isPlaying)
            return;
        
        var duration =playable.GetDuration();
        
        var so=clip.MakeDialogueForTimeline((float)duration);

        
        bool origin=VGF.Dialogue.DialogueManager.instance.Auto;
        if(clip.Manual){
            //开启手动模式：暂停播放
            director.Pause();
        }else{
            VGF.Dialogue.DialogueManager.instance.Auto=true;
        }

        VGF.Dialogue.DialogueManager.instance.Bind(so,()=>{
            director = playable.GetGraph().GetResolver() as PlayableDirector;
            //播放结束
            Debug.LogFormat("<color=purple>DialogueClip播放完毕</color>");
            // if(clip.Manual){
            director.Play(); //用Resume()不行，原因未知
            isPlayed=true;            
            // }
            if(!clip.Manual){
                VGF.Dialogue.DialogueManager.instance.Auto=origin;
            }
                
        });
        EventHandler.TimelinePlayDialogueInvoke(so);
        //Debug.LogFormat("Play {0}",playable);
    }
}