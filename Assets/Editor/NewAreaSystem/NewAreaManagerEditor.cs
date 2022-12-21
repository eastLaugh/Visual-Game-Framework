using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEditor;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using log4net.Repository.Hierarchy;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(NewAreaManager))]
[System.Obsolete]
public class NewAreaManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
    }


    public override VisualElement CreateInspectorGUI()
    {
        return null;   //只要return了新的visual element就会直接覆盖原来的inspector面板，包括ONInspectorGUI里的内容
    }

    private void OnSceneGUI() {
        //点
        var points=(target as NewAreaManager).points;
        for(int i=0;i<points.Length;i++){
            //path.Points[i]=Handles.FreeMoveHandle(path.Points[i],Quaternion.identity,2f,Vector3.one,capFunction:Handles.ConeHandleCap);
            points[i].position=Handles.PositionHandle(points[i].position,Quaternion.identity);
            
            GUI.color=Color.red;
            Handles.Label(points[i].position+new Vector3(0,0.5f,0),points[i].id);

        }



        //面 rectArea
        var rectAreas = (target as NewAreaManager).RectAreas;
        for(int i=0;i<rectAreas.Length;i++){
            var rect=rectAreas[i].rect;

            //获得地面的y位置
            // RaycastHit hitInfo;
            // Physics.Raycast(new Vector3(rect.x,100,rect.y),Vector3.down,out hitInfo);
            // var y=hitInfo.point.y;
            
            Vector3[] verts=new Vector3[]{
                new Vector3(rect.x,100,rect.y),
                new Vector3(rect.x+rect.width,100,rect.y),
                new Vector3(rect.x+rect.width,100,rect.y+rect.height),
                new Vector3(rect.x,100,rect.y+rect.height),
            }; 
            for(int j=0;j<4;j++){
                RaycastHit hitInfo;
                Physics.Raycast(verts[j],Vector3.down,out hitInfo);
                verts[j].y=hitInfo.point.y;
            }

            Handles.DrawSolidRectangleWithOutline(verts, new Color(0.5f, 0.5f, 0.5f, 0.1f), new Color(0, 0, 0, 1));



            //绘制手柄
            GUI.color=Color.red;
            Handles.Label(verts[0]+new Vector3(0,0.5f,0),rectAreas[i].id);


            var vector3=Handles.PositionHandle(verts[0],Quaternion.identity);
            rectAreas[i].rect.x=vector3.x;
            rectAreas[i].rect.y=vector3.z;


            vector3 = Handles.ScaleHandle(new Vector3(rect.width,0,rect.height),(verts[0]+verts[2])/2,Quaternion.identity);
            rectAreas[i].rect.width=vector3.x;
            rectAreas[i].rect.height=vector3.z;
        }


    }

    /// <summary>
    /// https://docs.unity3d.com/cn/current/ScriptReference/MenuItem.html
    /// </summary>
    [MenuItem("GameObject/[Visual Game Framework]New Point")]
    static public void CreateNewPoint(MenuCommand menuCommand)
    {
        //如果场景中不存在$NEWAREAMANAGER需要自动创建，实现workflow自动化
        var areaManager = GameObject.Find("$NewAreaManager")?.GetComponent<NewAreaManager>();
        if (!areaManager)
            areaManager =new GameObject("$NewAreaManager").AddComponent<NewAreaManager>();




        // Create a custom game object
        GameObject go = new GameObject("#Point");

        // Ensure it gets reparented if this was a context click (otherwise does nothing)
        if(menuCommand.context)
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
        else
            go.transform.position = EditorWindow.GetWindow<SceneView>().camera.transform.position;

        // Register the creation in the undo system
        Undo.RegisterCreatedObjectUndo(go, "创建 " + go.name);
        Selection.activeObject = go;

        go.tag = "Point";

        

        
    }

}
