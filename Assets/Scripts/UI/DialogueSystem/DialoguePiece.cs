using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialoguePiece
{
    public string Content;

    public float Scaler=1f;

    public float Time;       //只有在Auto模式下才启用

    /// <summary>
    /// 是否直接在上一条对话框中追加内容，而不是更换新的Dialogue Panel
    /// </summary>
    [System.Obsolete]
    public bool Append=false;
}