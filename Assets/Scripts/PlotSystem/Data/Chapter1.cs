

 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
using UnityEngine.SceneManagement;
using VGF.Assignment;
 using UnityEngine.Events;

 namespace VGF.Plot
 {
     public class Chapter1 : ChapterBase
     {
         public Arrival_SO arrival_SO1;
         public Arrival_SO goHome;


         public Dialogue_SO tm;
         public override void Run()
         {
             PlayMusic("DB");
             SceneMoveThen("SampleScene",action:() =>
             {
                 //PlayTimeline("wow");
                 Caption("去高地");
                 Caption("这是一个2秒的Caption", 2f);
                 Caption("这是一个4秒的Caption", 4f);
                 Caption("这是一个默认秒的Caption");
                 Debug.Log(NPC("GIRAL"));
                 var tmp = Say("GIRAL",
                     "这是第一条对话要长长长长长长长长",
                     "2 ： 长长长长长长长长长长长长长",
                     "我是第三条",
                     "...............(5)",
                     "WOW，我是最后一条4");



                 AfterSay(tmp, () =>
                 {
                     Caption("即将切换到第二章",callback:()=>{
                         Debug.Log("OOOOOOOOOOOOOOOHHHH");
                        
                         VGF(); //切换到下一章
                     });
                 });
                 
                 Assign(arrival_SO1, (i) =>
                     {
                         SayEmpty("GIRAL");
                         Say("GIRAL",overlap:false,"对话一","2","3");
                         Say("GIRAL",overlap:false,"对话2");
                         Caption("我到达了台阶！！现在我要回家了");
                         Assign(goHome, (i) =>
                         {

                             SceneMoveThen("Old", "Default Position");
                             Caption("咦？？我怎么到这里来了！", 3f);

                              Caption("ÖÐÎÄÂÒÂë½â¾ö·½°¸ 1¡¢ÐÞ¸ÄMySqlÊý¾Ý¿âµÄ", 10f);
                         });
                     });
             });



             //return;
              Caption("系统委派给你任务：去台阶上吧",default, () =>
              {
                  Assign(arrival_SO1, (i) =>
                  {

                      Caption("我到达了台阶！！现在我要回家了");
                      Assign(goHome, (i) =>
                      {

                          SceneMoveThen("Old","Default Position");
                          Caption("咦？？我怎么到这里来了！", 3f);

                           Caption("ÖÐÎÄÂÒÂë½â¾ö·½°¸ 1¡¢ÐÞ¸ÄMySqlÊý¾Ý¿âµÄ", 10f);
                      });
                  });

              });

         }

     }
 }