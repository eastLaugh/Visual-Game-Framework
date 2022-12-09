using UnityEngine;
using VGF.Plot;
namespace VGF.Assignment
{
    //[CreateAssetMenu(fileName = "Assignment_SO", menuName = "Visual Game Framework/Assignment_SO", order = 0)]
    public abstract class Assignment_SO : ScriptableObject
    {

        public string Title;

        [TextArea]
        public string Lore;
        public abstract bool Check();

        public bool Preferential { get; set; }//TODO



        //public bool Finished=false;
        /// <summary>
        /// 任务完成了
        /// </summary>

        public virtual void Finish()
        {
            Debug.Log("<color=green><b>Assignment Finished!  =   </b></color>" + Title);

            AssignmentManager.Instance.assignmentsCallBackDict[this](new AssignmentCallBackMessagee());
            AssignmentManager.Instance.assignmentsCallBackDict.Remove(this);
        }


    }
}