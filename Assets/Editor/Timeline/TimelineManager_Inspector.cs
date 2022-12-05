using UnityEngine;
using UnityEditor;

using UnityEngine.Playables;
using UnityEditorInternal;
using UnityEditor.Timeline;

[CustomEditor(typeof(TimelineManager))]
public class TimelineManager_Inspector : Editor {
    SerializedProperty playableAssetsProperty;


    ReorderableList reorderableList;
    private void OnEnable() {
        playableAssetsProperty=serializedObject.FindProperty("playableAssets");
        
        
        reorderableList=new ReorderableList(serializedObject,playableAssetsProperty,false,true,false,false);
        reorderableList.drawElementCallback+=(rect,index,isActive,isFocused)=>{
            EditorGUI.PropertyField(rect,playableAssetsProperty.GetArrayElementAtIndex(index));
        };
        reorderableList.onSelectCallback+=(list)=>{
            (target as TimelineManager).playableAsset=(target as TimelineManager).playableAssets[list.index];
            EditorWindow.GetWindow<TimelineEditorWindow>().Repaint();
        };
        
        reorderableList.drawHeaderCallback+=(rect)=>{
            EditorGUI.DrawRect(rect,color:Color.blue);
            EditorGUI.LabelField(rect,new GUIContent("Timeline Asset"),new GUIStyle{
                alignment=TextAnchor.MiddleCenter,
                normal=new GUIStyleState(){
                    textColor=Color.white
                }
            });
        };
        
    }
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        (target as TimelineManager).playableAssets=Resources.LoadAll<PlayableAsset>("Timelines/");




        serializedObject.Update();
        reorderableList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();

        if((target as TimelineManager).IsShowing)
            if(GUILayout.Button("Stop")){
                (target as TimelineManager).IsShowing=false;
                EditorWindow.GetWindow<TimelineEditorWindow>().Repaint();
            }else{}
        else if(GUILayout.Button("Play")){
            (target as TimelineManager).IsShowing=true;
            EditorWindow.GetWindow<TimelineEditorWindow>().Repaint();
        }
    }
}