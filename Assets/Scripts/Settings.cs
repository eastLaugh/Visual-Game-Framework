using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public static float PlayerSpeed=5f;
}


public class Singleton<T>:MonoBehaviour where T:Singleton<T>{
    private static T _instance;
    public static T Instance{
        get{
            return _instance;
        }
    }
    protected virtual void Awake() {
        // if(_instance!=(T)this)
        //     Destroy(_instance);
        _instance=(T)this;

    }

    protected virtual void OnDestroy() {
        // if(_instance==(T)this)
        //     _instance=null;
    }
}