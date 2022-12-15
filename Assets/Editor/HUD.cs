using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HUD))]
public class HUDEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        if(GUILayout.Button("Pop(message : string)")){
            HUD.Instance.Pop("unknown");
        }
    }
}