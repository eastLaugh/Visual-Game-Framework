# 施工中.......

---


*你好。*

*欢迎来到Visual Game Framework*

*本文档分为**快速上手**和**API文档**两个部分*

---

# 快速上手：

提前准备：你需要下载Github Desktop软件，点击本页面右上角的Code，用Github Desktop打开，然后将本项目直接Clone到本地。然后就可以在本地修改内容，通过Github Desktop Push上传。每一次Push还可以添加Comment评论。可以通过历史记录回到Push之前的状态。

为了方便管理，如果你要写一个新的功能，请新建一个分支（Branch），这很重要。默认分支是Main分支，这个分支最好先不要更新。先更新你新建的分支，当你的分支完成后，再合并到Main分支中。这相当于开辟了很多个平行空间，我们在不同的平行空间中共同完成任务，最后会和。

在PersistScene这个场景中，System这个空GameObject下，主要存放多个系统的GameObject，并且挂载相应系统的脚本。采用单例模式。

当播放键被打开，系统首先会调用PlotManager.Run()实现开始游戏。PlotManager中有一个类型为ChapterBase的Chapters数组，游戏开始运行后，系统会按顺序播放该数组中继承ChapterBase的脚本（Tip:可以按住Ctrl左键点击属性或类，查看他的定义）。

也就是说，如果你要编写剧本，需要新建一个脚本（eg. Assets/Scripts/PlotSystem/Data/Chapter3.cs），继承ChapterBase，挂载在Persistent/PlotManager上，并拖拽到Chapters数组中。然后覆写该脚本的Run函数。

ChapterBase基类为你的脚本函数提供了很多的方便的接口，你可以直接Ctrl+左键点击ChapterBase查看他的定义，定义中有详细的注释。常用的接口有：

SceneMoveThen 移动到新的场景
Caption 显示黑框提示文字
Say 让某个NPC打开对话系统，并填入对话
AfterSay 实现对话完成后的回调
Align 委派任务并实现完成后的回调
VGF 切换到下一章节
（详细参数，返回值请查阅ChapterBase类中的注释）





# 详细API文档：

# 场景层级

Visual Game Framework的层级（Hierarchy）中拥有***Persistent Scene***和**另外一个场景**。

## Persistent Scene

存放一些必要的Game Object。其中System和UI。

这个场景是被固定的。当加载其他场景时，这个场景并不会被移除，而是始终存在。

* Player

这是玩家。子物体是一个摄像机。挂载Player脚本。

* UI

所有的UI Canvas画布。一般情况下不包含脚本。但是部分Canvas中含有脚本，因为部分Canvas中的UI组件有动画，而动画的AnimationEvent需要挂载脚本。

* System

游戏的控制系统，包含PlotManager，AssignmentManger等等。是神经中枢，含有大量脚本。几乎每个脚本都含有静态成员instance，可以直接在其他代码中通过 xxx.instance调用,即[单例模式](https://blog.csdn.net/qq_40120946/article/details/122026768)（例如CaptionLoader.instance.Clear();）。

> 等等

>
















<!-- 
分层

游戏 Game
-剧情 PlotManager
-- 章节1 ChapterBase
---- 流程 Chapter
------任务控制器 AssignmentManager
--------任务1 Assignment

--------任务2

--章节2
（略）
--章节3
（略）
--……


流程Process :
①创建新的脚本，继承自ChapterBase，作为剧本脚本。
②在PlotManager下创建子GameObject，将ChapterBase附加到子GameObject上
③将拥有ChapterBase的GameObject拖拽到PlotManager中
④点击PlotManager中的Run执行剧本

在剧本脚本中：委派任务（Assignments）


///

AreaSystem 区域系统
提供脚本上方便选区的功能。

在场景的Collections GameObject下创建子物体，命名，附加Area或Point组件。



BUG001 低于-10高度自杀后会多次Run -->
