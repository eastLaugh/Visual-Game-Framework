using UnityEngine;
using UnityEditor;
using VGF.SceneSystem;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(SceneLoader))]
public class SceneLoader_Inpsector : Editor
{

    //增加一个注释
    //Scene[] scenes;
    string originSceneName;
    private void OnEnable()
    {
        // scenes= new Scene[SceneManager.sceneCountInBuildSettings];
        // for(int i=0;i<SceneManager.sceneCountInBuildSettings;i++){
        //     scenes[i] = SceneManager.GetSceneByBuildIndex(i);
        // }

    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        // foreach(var scene in scenes){
        //     if(GUILayout.Button(scene.name)){
        //         if(Application.isPlaying){
        //             originSceneName = SceneManager.GetSceneAt(1).name;
        //             VGF.Plot.ChapterBase.Instance.SceneMoveThen(scene.name,action:()=>{
        //                 Debug.LogFormat("<color=green>手动切换地图成功 场景为:{0}→{1} 坐标是默认点",originSceneName,scene.name);
        //             });
        //         }else{
        //             // SceneLoader.instance.SwitchSceneByName(scene.name);
        //         }

        //     }
        // }
        // for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        // {
        //     var scene = SceneManager.GetSceneByBuildIndex(i);
        //     if (GUILayout.Button(scene.path))
        //     {
        //         if (Application.isPlaying)
        //         {
        //             originSceneName = SceneManager.GetSceneAt(1).name;
        //             VGF.Plot.ChapterBase.Instance.SceneMoveThen(scene.name, action: () =>
        //             {
        //                 Debug.LogFormat("<color=green>手动切换地图成功 场景为:{0}→{1} 坐标是默认点", originSceneName, scene.name);
        //             });
        //         }
        //         else
        //         {
        //             // SceneLoader.instance.SwitchSceneByName(scene.name);
        //         }
        //     }
        // }
        
    }
}