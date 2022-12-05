using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAreaManager : MonoBehaviour
{

    #region Singleton
    private static NewAreaManager instance;
    public static NewAreaManager Instance
    {
        get => instance;
    }

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
    }

    #endregion

    [SerializeField]
    private Point[] points;

    public Point[] Points
    {
        get => points;
    }


    [SerializeField]
    private RectArea[] rectAreas;

    public RectArea[] RectAreas{
        get=>rectAreas;
    }


    private void OnDrawGizmos()
    {
        foreach (var item in points)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(item.position, 0.3f);
            

        }
    }


    public Point? FindPointByID(string id){
        foreach(var item in points)
            if(item.id==id)
                return item;
        
        Debug.LogError("<b>点无法被找到 →"+id+"</b>");
        return null;
    }

    public RectArea? FindRectAreaByID(string id){
        foreach(var item in rectAreas)
            if(item.id==id)
                return item;
    
        Debug.LogError("<b>RectArea无法被找到 →"+id+"</b>");
        return null;

    }
}


[System.Serializable]
public struct Point
{

    public string id;
    public Vector3 position;

}


[System.Serializable]

public struct RectArea{
    public string id;
    public Rect rect;

    // public float x1{
    //     get{
    //         return rect.x;
    //     }
    //     set{
    //         rect.x=value;
    //     }
    // }

    // public float 


    public bool Contains2D(Vector3 position){
        if(rect.x <= position.x &&position.x <= rect.x+rect.width)
            if(rect.y <=position.z && position.z <= rect.y+rect.height)
                return true;
        return false;
    }
}
