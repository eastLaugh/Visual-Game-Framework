using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{

    [Header("")]
    public Canvas PauseCanvas;


    public 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause(){
        PauseCanvas.enabled=true;

    }


    public void Resume(){
        
        PauseCanvas.enabled=false;
    }

    public TextAsset textAsset;
    public void Save(){
        
    }

    public void SaveAsNew(){

    }

    public void BackToLobby(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameMenu");
    }

}
