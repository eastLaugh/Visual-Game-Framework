using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

[CustomEditor(typeof(PlusAreaManager))]
public class PlusAreaEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.Label("接管");
    }
    /// <summary>
    /// https://docs.unity3d.com/cn/current/ScriptReference/MenuItem.html
    /// </summary>
    [MenuItem("GameObject/添加新的点")]
    static public void CreateNewPoint(MenuCommand menuCommand)
    {
        // Create a custom game object
        GameObject go = new GameObject("#Point");

        // Ensure it gets reparented if this was a context click (otherwise does nothing)
        if (menuCommand.context)
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
        else
            go.transform.position = EditorWindow.GetWindow<SceneView>().camera.transform.position;

        // Register the creation in the undo system
        Undo.RegisterCreatedObjectUndo(go, "创建 " + go.name);
        Selection.activeObject = go;

        go.tag = "Point";
        go.AddComponent<PlusPoint>();




    }
}
