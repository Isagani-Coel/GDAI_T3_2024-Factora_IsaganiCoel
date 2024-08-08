using System;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance;

    public Sound[] misc, game, bgm;
    public AudioSource source;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void Play(string name, int mode) {
        Sound s = null;

        switch (mode) {
            case 0: s = Array.Find(misc, x => x.name == name); break;
            case 1: s = Array.Find(game, x => x.name == name); break;
            case 2: s = Array.Find(bgm, x => x.name == name); break;
            default: Debug.LogError("Invalid option"); break;
        }

        if (s != null) {
            source.volume = s.volume;
            source.clip = s.clip;
            source.loop = s.loop;
            source.PlayOneShot(s.clip);
        }
        else Debug.LogError("Sound not found!");
    }

    public void ToggleMute() { source.mute = !source.mute; }
    public void ToggleVolume(float v) { source.volume = v; }
}

[Serializable]
public class Sound {
    public string name;
    public AudioClip clip;
    public bool loop;

    [Range(0f, 1f)]
    public float volume = 0.8f;

    [HideInInspector]
    public AudioSource source;
}
