using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HintLoader : Singleton<HintLoader>
{
    [SerializeField] private Image hintImage;
    private Text hintText;  //提示信息
    private Animator hintAnimator;

    protected override void Awake()
    {
        base.Awake();
        hintImage.gameObject.SetActive(false);
        hintText=hintImage.GetComponentInChildren<Text>();
        hintAnimator = hintImage.GetComponent<Animator>();
    }
    /// <summary>
    /// 打开Hint面板
    /// </summary>
    /// <param name="message">显示的信息</param>
    public void HintOn(string message)
    {
        hintImage.gameObject.SetActive(true);
        hintText.text = message;
    }

    /// <summary>
    /// 关闭Hint面板
    /// </summary>
    public void HintOff()
    {
        //hintText.text = string.Empty;
        hintAnimator.SetTrigger("Fade");
        //hintImage.gameObject.SetActive(false);
    }

    /// <summary>
    /// 使Hint面板显示一段时间(可打断)
    /// </summary>
    /// <param name="message">显示的信息</param>
    /// <param name="seconds">显示的时间</param>
    public void HintWithSeconds(string message,int seconds)
    {
        hintImage.gameObject.SetActive(true);
        hintText.text = message;
        StartCoroutine(Wait(message,seconds));
    }
    private IEnumerator Wait(string message,int seconds)
    {
        yield return new WaitForSeconds(seconds);
        if(hintText.text==message)
            hintAnimator.SetTrigger("Fade");
        yield break;
    }

}
