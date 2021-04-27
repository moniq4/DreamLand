using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    public AudioSource[] audioSounds;
    public string[] audioNames;

    private IDictionary<string, AudioSource> audioSoundsDictionary;

    private void Start()
    {
        audioSoundsDictionary = new Dictionary<string, AudioSource>();

        int n = 0;
        foreach (string name in audioNames)
        {
            audioSoundsDictionary.Add(name, audioSounds[n++]);
        }
    }

    public void PlaySound(string soundName)
    {
        AudioSource sound;
        audioSoundsDictionary.TryGetValue(soundName, out sound);
        if (sound) sound.Play();
    }

}
