using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    [SerializeField] private AudioSource sfxSource;
    public static AudioManager Instance { get; private set; }

    protected virtual void Awake() {
        if (Instance != null)
            Debug.LogWarning($"Singleton {Instance} already has an instance.");
        else
            Instance = this;
    }

    protected virtual void OnApplicationQuit() {
        Instance = null;
    }

    public void PlayClip(AudioClip clip) {
        sfxSource.PlayOneShot(clip);
    }

}
