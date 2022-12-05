using UnityEngine;

using System.Collections.Generic;
[System.Serializable]
[CreateAssetMenu(fileName = "Dialogue_SO", menuName = "Visual Game Framework/Dialogue", order = 0)]
public class Dialogue_SO : ScriptableObject {
    public List<DialoguePiece> dialoguePieces=new List<DialoguePiece>();

    public bool Snapchat=false;
    
}



