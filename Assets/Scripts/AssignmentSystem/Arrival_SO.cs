using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VGF.AreaNameSpace;



namespace VGF.Assignment
{
    

    [CreateAssetMenu(menuName = "Visual Game Framework/Assignment/Arrival", order = 0)]
    public class Arrival_SO : Assignment_SO
    {
        public string Area;

        private RectArea? area => NewAreaManager.Instance.FindRectAreaByID(Area);
        //override public bool Preferential{get{return true;}}//TODO
        private Vector3 position => Player.instance.transform.position;



        public override bool Check()
        {

            if(area?.Contains2D(position)==true)
                return true;
            else
                return false;

        }

    }
}