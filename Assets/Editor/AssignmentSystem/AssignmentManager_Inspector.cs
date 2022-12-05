  using UnityEditor;
using UnityEngine;

  using VGF.Assignment;

   [CustomEditor(typeof(AssignmentManager))]
    public class AssignmentManager_Inspector : Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            if(GUILayout.Button(new GUIContent("<color=red>Finish One</color>"))){
                if(Application.isPlaying)
                    (target as AssignmentManager).FinishAt(0);
                else
                    Debug.LogError("In Editor");
            }
        }
    }