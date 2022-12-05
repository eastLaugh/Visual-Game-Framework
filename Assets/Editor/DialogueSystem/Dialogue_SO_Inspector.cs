using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

using System.IO;

[CustomEditor(typeof(Dialogue_SO))]
public class Dialogue_SO_Inspector : Editor {
    string text;
    private List<DialoguePiece> dialoguePieces =>(target as Dialogue_SO).dialoguePieces;

    private void OnEnable() {
        var stream=new MemoryStream();
        var writer=new StreamWriter(stream);

        foreach(var i in dialoguePieces){
            writer.WriteLine(i.Content);
        }
        writer.Flush();

        stream.Position=0;
        var reader=new StreamReader(stream);
        text=reader.ReadToEnd();
    }
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        if(GUILayout.Button("Clear")){
            dialoguePieces.Clear();
        }

        GUILayout.Space(20);
        text=GUILayout.TextArea(text);
        if(GUILayout.Button("Convert"))
            Convert();
        
    }

    void Convert(){
        text=text.TrimEnd('\r','\n',' ');

        dialoguePieces.Clear();
        foreach(var i in text.Split("\n")){
            dialoguePieces.Add(new DialoguePiece(){Content=i});
        }
    }
}
