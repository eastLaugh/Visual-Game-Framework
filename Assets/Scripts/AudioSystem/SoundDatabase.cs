using UnityEngine;

using System.Collections.Generic;

[CreateAssetMenu(fileName = "SoundDatabase", menuName = "Visual Game Framework/SoundDatabase", order = 0)]
public class SoundDatabase : ScriptableObject {
    public List<SoundDetails> soundDetails;

    public SoundDetails FindSoundDetailsByName(string name){
        return soundDetails.Find(i=>i.SoundName==name);
    }


    

}