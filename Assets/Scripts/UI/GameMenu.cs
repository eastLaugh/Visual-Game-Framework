using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // if(Application.isEditor)
        //     OnClickPlay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickPlay(){
        SceneManager.LoadScene("Persistent Scene",LoadSceneMode.Single);
    }


    //Save & Load Data
    public static bool isNew;

}
