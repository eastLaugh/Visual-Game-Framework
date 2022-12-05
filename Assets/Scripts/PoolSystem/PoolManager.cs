
using UnityEngine.Pool;

using UnityEngine;
using System.Collections.Generic;


public class PoolManager : Singleton<PoolManager> {
    
    public List<GameObject> prefabs;
    public List<ObjectPool<GameObject>> pools=new List<ObjectPool<GameObject>>();
    
    public SoundDatabase soundDatabase;

    void CreatPool(){
        foreach(GameObject gameobject in prefabs){
            Transform parent =  new GameObject(gameobject.name).transform;
            parent.SetParent(transform);

            var newPool = new ObjectPool<GameObject>(
                createFunc:
                ()=>{
                    Debug.LogWarning("对象池中一个对象已被创建");
                    return Instantiate(gameobject,parent);
                },
                actionOnGet:
                (e)=>
                   e.SetActive(true),
                actionOnRelease:
                (e)=>
                e.SetActive(false),
                actionOnDestroy:
                (e)=>
                    Destroy(e)
            ); 
            pools.Add(newPool);
        }
        
    }

    

    private void Start() {
        CreatPool();
        //PlayAudio("Fight");
        // PlayAudio("M");
    }

    private void Update() {
        
    }
    
    public void PlayAudio(string soundName){
        var tmp= Pool("Audio").Get();
        tmp.GetComponent<AudioSource>().clip=soundDatabase.FindSoundDetailsByName(soundName);
        tmp.GetComponent<AudioSource>().Play();
    }
    
    ObjectPool<GameObject> Pool(string poolName){
        // foreach(var prefab in prefabs){
        //     if(prefab.name==poolName)
        //         return pools[prefabs.GET];
        // }
        for(int i=0;i<prefabs.Count;i++){
            if(prefabs[i].name==poolName)
                return pools[i];
        }
        return null;
    }
}