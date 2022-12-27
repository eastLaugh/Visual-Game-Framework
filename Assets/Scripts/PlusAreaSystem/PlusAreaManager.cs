using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusAreaManager : Singleton<PlusAreaManager>
{
    GameObject[] points => GameObject.FindGameObjectsWithTag("Point");

    private void OnDrawGizmos()
    {
        foreach (var point in points)
        {


        }
    }





}
