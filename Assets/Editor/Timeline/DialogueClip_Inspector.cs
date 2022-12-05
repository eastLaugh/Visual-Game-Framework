using UnityEngine;
using UnityEditor;

using System.Collections.Generic;
using System.IO;

[CustomEditor(typeof(DialogueClip))]
public class DialogueClip_Inspector : Editor {
    string text;

    private List<DialoguePiece> dialoguePieces =>(target as DialogueClip).dialoguePieces;

    private void OnEnable() {
        DialogueToText(dialoguePieces,out text);
    }

    public static void DialogueToText(in List<DialoguePiece> dialoguePieces,out string str2){
        var stream=new MemoryStream();
        var writer=new StreamWriter(stream);

        foreach(var i in dialoguePieces){
            writer.WriteLine(i.Content);
        }
        writer.Flush();

        stream.Position=0;
        var reader=new StreamReader(stream);
        str2=reader.ReadToEnd();

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