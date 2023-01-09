using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlusAreaManager : Singleton<PlusAreaManager>
{
    GameObject[] points => GameObject.FindGameObjectsWithTag("Point");

    private void OnDrawGizmos()
    {
        foreach (var point in points)
        {
            GUIStyle gs=new();
            gs.fontStyle = FontStyle.Bold;
            gs.alignment = TextAnchor.MiddleCenter;
            gs.normal.textColor = Color.green;
            Handles.Label(point.transform.position+new Vector3(0,0.3f,0),point.GetComponent<PlusPoint>().PointName,gs);

        }
    }

    



}
