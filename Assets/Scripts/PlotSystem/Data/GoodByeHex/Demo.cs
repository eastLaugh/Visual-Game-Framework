using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VGF.Assignment;
namespace VGF.Plot
{
    public class Demo:ChapterBase
    {
        public override void Run()
        {
            SceneMoveThen("castle attic", action: () =>
            {
                Caption("城堡阁楼");

            });

        }
    }
}

