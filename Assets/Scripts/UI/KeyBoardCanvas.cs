using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardCanvas : MonoBehaviour
{

    public Canvas canvas;


    void Awake() {
        
        canvas=GetComponent<Canvas>();   
    }

    void Start()
    {
        canvas.worldCamera=Player.instance.PlayerCamera;
    }
}
