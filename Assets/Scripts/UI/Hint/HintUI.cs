using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintUI : MonoBehaviour
{
    [SerializeField]private Text hintText;
    public void HintEnd()
    {
        hintText.text = string.Empty;
        gameObject.SetActive(false);
    }
}
