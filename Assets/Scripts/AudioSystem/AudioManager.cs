using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Obsolete]
public class AudioManager : MonoBehaviour
{
    public SoundDatabase soundDatabase;

    private AudioSource MusicAudioSource=>GetComponents<AudioSource>()[0];

    private AudioSource AmbientAudioSource=>GetComponents<AudioSource>()[1];


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayMusic(string name){
        MusicAudioSource.clip=soundDatabase.FindSoundDetailsByName(name);

        MusicAudioSource.Play();
    }

    void PlayAmbient(){

    }

    private void OnEnable() {
        EventHandler.PlayMusic+=PlayMusic;
    }
    private void OnDisable() {
        EventHandler.PlayMusic-=PlayMusic;
    }
}
