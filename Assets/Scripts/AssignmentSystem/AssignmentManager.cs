using System;
using System.Collections.Generic;
using UnityEngine;

using VGF.Plot;

namespace VGF.Assignment
{
    public class AssignmentManager : Singleton<AssignmentManager>
    {
        // private static AssignmentManager _instance;
        // public static AssignmentManager instance
        // {
        //     get
        //     {
        //         return _instance;
        //     }
        // }
        // private void Awake()
        // {
        //     if (instance != null)
        //         Destroy(instance);
        //     _instance = this;
        // }


        public List<Assignment_SO> assignments=new List<Assignment_SO>();
        public Dictionary<Assignment_SO, Action<AssignmentCallBackMessagee>> assignmentsCallBackDict=new Dictionary<Assignment_SO, Action<AssignmentCallBackMessagee>>();

        // Start is called before the first frame update
        void Start()
        {

        }

        public void Clear(){
            // assignments=new List<Assignment_SO>();
            // assignmentsCallBackDict=new Dictionary<Assignment_SO, Action<AssignmentCallBackMessagee>>();
            assignments.Clear();
            assignmentsCallBackDict.Clear();

        }

        // Update is called once per frame
        void Update()
        {

            for (int i = 0; i < assignments.Count; i++)
            {
                if (assignments[i].Check())
                {
                    FinishAt(i);
                    return;
                    //这里由于list的特性，用foreach+删除会造成数组上的错误，所以干脆就直接return好了，反正update实时更新    
                }
            }
        }

        public void FinishAt(int i){
            assignments[i].Finish();
            assignments.RemoveAt(i);

        }

    }

    
 

}