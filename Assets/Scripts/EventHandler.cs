using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EventHandler 
{

   public delegate void PlayTimelineDelegate(string name,Action action);

   public static PlayTimelineDelegate PlayTimeline;

   public static void PlayTimelineInvoke(string name,Action action=null){
        PlayTimeline?.Invoke(name,action);
   }

   public static Action<Dialogue_SO> TimelinePlayDialogue;

   public static void TimelinePlayDialogueInvoke(Dialogue_SO dialogue_SO){
      TimelinePlayDialogue?.Invoke(dialogue_SO);
   }
   

   public static Action<string> PlayMusic;

   public static void PlayMusicInvoke(string name){
      PlayMusic?.Invoke(name);
   }

    // public static Action OnTimelinePlay;

    // public static void OnTimelinePlayInvoke(){
    //    OnTimelinePlay?.Invoke();
    // }
    public static event Action<InventoryLocation, List<InventoryItem>> UpdateInventoryUI;
    public static void CallUpdateInventoryUI(InventoryLocation location, List<InventoryItem> list)
    {
        UpdateInventoryUI?.Invoke(location, list);
    }
   
}
