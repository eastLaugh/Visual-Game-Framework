using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UnityEngine.Playables;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayableDirector))]
public class TimelineManager : Singleton<TimelineManager>
{

    private PlayableDirector director => GetComponent<PlayableDirector>();
    protected override void Awake()
    {
        base.Awake();
        director.stopped += (i) =>
        {
            _isShowing = false;
        };
    }

    void Start()
    {
        // playableAsset=GetPlayableAssetFromResources("test");
        MainCamera.enabled = true;
        SceneCamera.enabled = false;
    }
    void Update()
    {
        if(IsShowing&&Input.GetKeyDown(KeyCode.Space))
            director.Stop();
    }

    public static PlayableAsset GetPlayableAssetFromResources(string fileName)
    {
        return Resources.Load<PlayableAsset>("Timelines/" + fileName);
    }

    public PlayableAsset playableAsset
    {
        set => director.playableAsset = value;
    }

    public PlayableAsset[] playableAssets;

    private bool _isShowing = false;
    /// <summary>
    /// 是否播放
    /// </summary>
    /// <value></value>
    public bool IsShowing
    {
        get
        {
            return director.state == PlayState.Playing;
        }
        set
        {
            _isShowing = value;
            if (value)
            {
                director.Play();
            }
            else
            {
                director.Stop();
            }
        }
    }

    private void OnEnable()
    {
        EventHandler.PlayTimeline += PlayTimeline;
        director.stopped += OnTimelineStop;
    }

    private void OnTimelineStop(PlayableDirector pd)
    {
        Debug.LogFormat("<color=purple>OnTimeLineStop()</color>");
        try
        {
            SceneCamera.enabled = false;
            MainCamera.enabled = true;
        }
        catch
        {
            //throw;
        }

        Player.instance.Mute = false;


        AfterAction?.Invoke();
        AfterAction=()=>{
            Debug.LogError("调用了空的Timeline AfterAction");
        };
    }

    private void OnDisable()
    {
        EventHandler.PlayTimeline -= PlayTimeline;
        director.stopped -= OnTimelineStop;
    }

    private Action AfterAction;
    private Camera MainCamera => GameObject.Find("Main Camera").GetComponent<Camera>();
    
    private Camera SceneCamera{
        get{
                return GameObject.Find("Scene Camera").GetComponent<Camera>();
            
        }
    }

    void PlayTimeline(string name, Action action = null)
    {
        if(GameObject.Find("Scene Camera")==null)
            return;
           

        

        playableAsset = GetPlayableAssetFromResources(name);

        Debug.LogFormat("<color=purple>TimeLine开始播放 name={0}</color>", name);
        IsShowing = true;

        EventHandler.OnTimelinePlayInvoke();   //调用Timeline的广播消息
        try
        {
            MainCamera.enabled = false;
            SceneCamera.enabled = true;
        }
        catch
        {
            OnTimelineStop(director);
            //throw;
        }
        Player.instance.Mute = true;

        AfterAction = action;


    }
}
