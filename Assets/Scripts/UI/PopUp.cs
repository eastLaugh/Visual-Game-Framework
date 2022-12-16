using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log(button.GetComponent<UnityEngine.UI.Image>());   // Can be found
        // Debug.Log(button.GetComponent<UnityEngine.UIElements.Image>());
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject window=>transform.Find("Canvas/Window").gameObject;
    private Animator windowsAnimator=>window.GetComponent<Animator>();

    private GameObject button=>transform.Find("Canvas/Window/Image").gameObject;
    private System.Action<int> callback;
    public void Pop(string message,System.Action<int> callback=null){
        button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(Close);
        GetComponentInChildren<UnityEngine.UI.Text>().text=message;
        this.callback=callback;
        windowsAnimator.SetTrigger("pop");
        
    }

    void Init(){

    }

    public enum PopUpType{close,receive}  //弹出窗口用户选择的项目

    public void Close(){
        windowsAnimator.SetTrigger("close");
        StartCoroutine("wait");
    }

    public void Receive(){
        windowsAnimator.SetTrigger("close");
        StartCoroutine("wait");
    }

    IEnumerator wait(int type=-1){
        while(!windowsAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle")){
            yield return null;
        }
        //播放完毕
        callback?.Invoke(type);
    }

}
