using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using VGF.Plot;
using VGF.NPC;
public class Session1 : ChapterBase
{
    public override void Run()
    {
        SceneMoveThen("Giral Mental World", action: () =>
        {

            Debug.Log("Test");
            //Say("@","ddddddd");
            Timeline("D", () =>
            {
                Caption("........");
                Say("蜡烛","我是蜡烛");
                
                NPC("GIRAL").Mute = false;
                // Debug.Log(NPC("GIRAL"));
                var s1 = Say("@Player",
                    @"…………
                    ……………………
                    这是哪……
                    当我恢复意识之后，就发现自己位于一个白色的空间当中
                    我……
                    是在做梦吧……"
                );


                //                 Say("@Player",
                // @"ABCDEFG
                // www
                // ww12
                // was
                // dsadasd
                //                 "
                // );
                AfterSay(s1, () =>
                {
                    Caption("开端", callback: () =>
                    {
                        NPC("GIRAL").Mute = false;
                    });
                    #region  s

                    #endregion
                    PlayMusic("M");

                    AfterSay(Say(NPC("GIRAL"), overlap: true, "嗨！", "你好呀……"), () =>
                    {
                        Caption("你为什么这么小……");

                        var tip = WaitThen(7f, () =>
                        {
                            Caption("不……不知道做什么吗……");
                            Caption("那就奔赴太阳升起的地方吧！");
                        });

                        Then();
                    });
                });

            });



        });
    }

    void Then()
    {
        NPC[] girals = { NPC("GIRAL BIG") };
        AfterSay(Say(girals[0], overlap: true, "「我……」",
        "「我终于变得好大好大……」",
        "「这样就没有人欺负我了……是吗」",
        "「是这样的吧……」",
        "…………"
        ), () =>
        {
            girals[0].Mute = true;
            PlayMusic("AI");
            Caption("快跑！！！往西边跑！！");
            WaitThen(5F, () =>
            {
                SceneMoveThen("Darkness", action: Darkness);
            });
        });
    }

    void Darkness()
    {

        // DEMO演示语句
        Player.instance.transform.Find("Particle System").gameObject.SetActive(false);

        //
        Say("GIRAL", "不要……",
        "不要再往前去了……",
        "求求你！");
        Say("GIRAL", "为什么……");
        Say("GIRAL", "为什么……不愿意相信我……");
        Arrival("House", (msg) =>
        {
            SceneMoveThen("Giral Mental World", action: () =>
            {
                Caption("¸ 1¡¢ÐÞ¸ÄMySqlÊý¾Ý¿");
                SayEmpty("GIRAL");
                Say("GIRAL", "为什么……");
                Say("GIRAL", "要让我去死……");
                Say("GIRAL", "为什么！！！……");
                WaitThen(10f, () =>
                {
                    Caption("本DEMO结束，仅供测试使用 - HEX STUDIO");
                });
            });
        });
    }
}




