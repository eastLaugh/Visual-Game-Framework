
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VGF.NPC
{
    public class NPCManager : MonoBehaviour
    {
        private static NPCManager _instance;
        public static NPCManager instance
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
        }

        
        public NPC GetNPCByName(string name){
            // 
            if(name=="Player"||name=="@"||name=="@Player"||name==string.Empty)
                return Player.instance.GetComponent<NPC>();
            
            foreach(var i in GameObject.FindGameObjectsWithTag("NPC"))
                if(i.name==name)
                    return i.GetComponent<NPC>();

            // var lastAnswer= GameObject.Find(name)?.GetComponent<NPC>();
            // if(lastAnswer)
            //     return lastAnswer;
        

            Debug.LogError("<b>NPCManager中找不到NPC</b>");
            return null;
        }




        //
    }
}