using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

public class VGFHelper : EditorWindow {

    [MenuItem("Visual Game Framework/VGFHelper")]
    private static void ShowWindow() {
        var window = GetWindow<VGFHelper>();
        window.titleContent = new GUIContent("VGFHelper");
    }

    Vector2 scrollPosition;
    //多次执行
    private void OnGUI() {    
        scrollPosition=GUILayout.BeginScrollView(scrollPosition);
        for(int i=1;i<=10;i++)
            GUILayout.Button("UnityGUI也可以");
        GUILayout.EndScrollView();
    }

    void CreateGUI(){
        // VisualElement tmp=new ScrollView(ScrollViewMode.Vertical);

        // for(int i=1;i<=100;i++)
        //     tmp.Add(new Button(){text="UITOOLKIT"});

        // rootVisualElement.Add(tmp);
        
    }
}