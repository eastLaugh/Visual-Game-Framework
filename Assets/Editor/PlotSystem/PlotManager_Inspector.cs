using UnityEditor;
using UnityEngine;
    namespace VGF.Plot{
    [CustomEditor(typeof(PlotManager))]
    public class PlotManager_Inspector : Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            if(GUILayout.Button(new GUIContent("Run"))){
                if(Application.isPlaying)
                    (target as PlotManager).Run();
                else
                    Debug.LogError("In Editor");
            }
        }
    }
    }