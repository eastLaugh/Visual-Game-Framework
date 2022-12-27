using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlusPoint : MonoBehaviour
{
    [Header("µãµÄÃû×Ö")]
    public string PointName;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position,new Vector3(0.3f,0.3f,0.3f));

        Gizmos.color = Color.red;
    
    }
}
