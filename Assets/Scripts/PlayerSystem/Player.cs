using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region  Singleton
        private static Player _instance;
        public static Player instance{
            get{
                return _instance;
            }
        }

    #endregion

    public Camera PlayerCamera;
    public CharacterController characterController;
    private Animator animator;
    [SerializeField]private float speedOffset = 1f;
    private void Awake() {
        if(_instance!=null)
            Destroy(_instance);
        _instance=this;

        characterController= GetComponent<CharacterController>();
        animator=GetComponent<Animator>();
    }
    void Start()
    {
        
    }
    void Update()
    {
        // if(Mute)
        //     characterController.enabled=false;

        if(Mute)
            return;

        var motion =new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"))*Settings.PlayerSpeed*speedOffset;
        motion.y=-1f;

        if(characterController.enabled)
            characterController.Move(motion*Time.deltaTime);
        if(Input.GetAxisRaw("Horizontal")!=0)
            transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"),1,1);

        animator.SetFloat("Horizontal",motion.x);
        animator.SetFloat("Vertical",motion.z);
        animator.speed=Mathf.Clamp(Mathf.Max(motion.x,motion.z),1f,2f);

        // Debug.Log(animator.speed);
        //shift疾跑
        if(Input.GetKey(KeyCode.LeftShift)){
            speedOffset=2f;
            //animator.speed=2;
        }
        else{
            speedOffset=1f;
            //animator.speed=1;
        }
        

        //死亡
        if(transform.position.y<-10)
             Die();
    }



    /// <summary>
    /// 这是为了解决BUG1而出此下策
    /// BUG1已解决，此FUNCTION可以删除。纪念作用留存于此 :）
    /// 
    /// </summary>
    public void ForceMoveToPosition(Vector3 point){
        Debug.Log("<color=green>wow</color>");
        
        StartCoroutine(ForceMoveToPositionEnumerator(point));
    }

    IEnumerator ForceMoveToPositionEnumerator(Vector3 point){

        transform.position=point;
        yield return new WaitForSecondsRealtime(5f);
    }
    void Die(){
        //清空委派
        VGF.Assignment.AssignmentManager.Instance.Clear();
        VGF.UI.CaptionLoader.instance.Stop();


        
        //Player.instance.characterController.minMoveDistance = Mathf.Infinity;
        transform.position=new Vector3(transform.position.x,10,transform.position.z);//在切换到初始场景前，玩家的y值必须先设置成0，防止自杀误判，回导出现2次以上的Run

        VGF.Plot.PlotManager.instance.Run();

        
       

    }


    private bool _mute=false;
    public bool Mute{
        get{
            if(VGF.Dialogue.DialogueManager.Instance.isChatting)
                return true;
            return _mute;
        }
        set{
            _mute=value;
            if(_mute)
                characterController.enabled=false;
            else
                characterController.enabled=true;
        }
    }

}
