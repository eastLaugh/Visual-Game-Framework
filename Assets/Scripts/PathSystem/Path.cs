using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class Path:MonoBehaviour{



    [SerializeField]
    private Vector3[] _points;

    public Vector3[] Points{
        get=>_points;
    }

    private void OnDrawGizmosSelected() {
         for(int i=0;i<Points.Length;i++){
            //path.Points[i]=Handles.FreeMoveHandle(path.Points[i],Quaternion.identity,2f,Vector3.one,capFunction:Handles.ConeHandleCap);
            //path.Points[i]=Handles.PositionHandle(path.Points[i],Quaternion.identity);
            Gizmos.color=Color.red;
            Gizmos.DrawSphere(Points[i],-0.2f);

            
        }
    }


}