using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInScene : Singleton<PlayerInScene>
{
    private Vector3 position;
    private Animator animator=>GetComponent<Animator>();
    void Update(){
        var delta = (transform.position-position)/Time.deltaTime;
        //Debug.Log(delta);
        animator.SetFloat("Horizontal",delta.x);
        animator.SetFloat("Vertical",delta.z);
        animator.speed=Mathf.Clamp(Mathf.Max(delta.x,delta.z)*0.2f,1f,Mathf.Infinity);

        position=transform.position;
    }


}
