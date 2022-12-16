using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PopUp))]
public class PopUpEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        if(GUILayout.Button("Pop(message : string)")){
            (target as PopUp).Pop("unknown");
        }
        if(GUILayout.Button("Close()")){
            (target as PopUp).Close();
        }
    }
}