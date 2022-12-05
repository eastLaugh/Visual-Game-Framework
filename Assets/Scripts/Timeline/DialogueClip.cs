using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Playables;

public class DialogueClip :PlayableAsset
{
    [Header("手动运行")]

    public bool Manual;

    
    public List<DialoguePiece> dialoguePieces;


    private ScriptPlayable<DialogueBehaviour> playable;

    private DialogueBehaviour behaviour=>playable.GetBehaviour();
    
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        
        playable=ScriptPlayable<DialogueBehaviour>.Create(graph,new DialogueBehaviour(){clip=this});
        //playable.GetBehaviour();
        return playable; //隐式转换 ScriptPlayable -> Playable
    }



    public Dialogue_SO MakeDialogueForTimeline(float duration){
        var ans = ScriptableObject.CreateInstance<Dialogue_SO>();
        ans.dialoguePieces=dialoguePieces;

        float m=0f;
        if(!Manual){
            foreach(var i in ans.dialoguePieces){
                m+=i.Scaler;
            }
            foreach(var i in ans.dialoguePieces){
                i.Time=duration/m*i.Scaler;
            }
        }

        return ans;
    }
}



