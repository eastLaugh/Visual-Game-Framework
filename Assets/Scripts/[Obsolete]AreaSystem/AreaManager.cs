using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using VGF.SceneSystem;


namespace VGF.AreaNameSpace
{
    [System.Obsolete]
    public class AreaManager : MonoBehaviour
    {
        private static AreaManager _instance;

        public static AreaManager instance
        {
            get
            {
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance != null)
                Destroy(_instance);
            _instance = this;


        }


        void Tag(){

        }

        public Area GetAreaByName(string name){
            var tmp=GameObject.FindGameObjectsWithTag("Area");
            
            foreach(var i in tmp){
                if(i.name==name)
                    return i.GetComponent<Area>();
            }

            Debug.LogError("<b>区域无法被找到 →</b>"+name);
            return null;  
        }
        
        public Point GetPointByName(string name){
            var tmp=GameObject.FindGameObjectsWithTag("Point");
            
            foreach(var i in tmp){
                if(i.name==name)
                    return i.GetComponent<Point>();
            }

            Debug.LogError("<b>点无法被找到 →"+name+"</b>");
            return null;  
            
        }
    }
    [System.Obsolete]
    /// <summary>
    /// 扩展类。示例：(2f).Between(1f,3f) → True
    /// </summary>
    public static class AreaExpand
    {
        public static bool Between(this float self, float A, float B)
        {
            return self >= Mathf.Min(A, B) && self <= Mathf.Max(A, B);
        }
    }

}