using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;


[CustomEditor(typeof(Path))][System.Obsolete]
public class PathEditor : Editor
{

    private Path path;


    private void OnEnable()
    {
        path = target as Path;
    }


    private void OnSceneGUI()
    {
        for(int i=0;i<path.Points.Length;i++){
            //path.Points[i]=Handles.FreeMoveHandle(path.Points[i],Quaternion.identity,2f,Vector3.one,capFunction:Handles.ConeHandleCap);
            path.Points[i]=Handles.PositionHandle(path.Points[i],Quaternion.identity);

        }

        


        //不能拖动，只是创建个手柄
        // return;
        // foreach (var i in path.Points)
        // {
             

        //     continue;
        //     Handles.color = Handles.xAxisColor;
        //     Handles.ArrowHandleCap(0, i, path.transform.rotation * Quaternion.LookRotation(Vector3.right), 2f, EventType.Repaint);


        //     Handles.color = Handles.yAxisColor;
        //     Handles.ArrowHandleCap(0, i, path.transform.rotation * Quaternion.LookRotation(Vector3.up), 2f, EventType.Repaint);
            
        //     Handles.color = Handles.zAxisColor;
        //     Handles.ArrowHandleCap(0, i, path.transform.rotation * Quaternion.LookRotation(Vector3.back), 2f, EventType.Repaint);
        // }

    }

}
