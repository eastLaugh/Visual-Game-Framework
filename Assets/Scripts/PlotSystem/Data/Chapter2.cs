using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using VGF.Assignment;
using UnityEngine.Events;

namespace VGF.Plot
{
    public class Chapter2 : ChapterBase
    {
        
        public override void Run()
        {
            SceneMoveThen("SampleScene",action:()=>{
                Caption("第二章");
                Caption("洋溢着青春的junk food");

            });
        
            
        }

    }
}