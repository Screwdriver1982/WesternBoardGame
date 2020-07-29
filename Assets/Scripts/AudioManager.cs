using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton
    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        print("create Audio singleton");
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            music.volume = PlayerPrefs.GetFloat(PREFS_MUSIC_VOLUME, 0.05f);
            effects.volume = PlayerPrefs.GetFloat(PREFS_EFFECTS_VOLUME, 0.3f);
        }

    }

    #endregion

    [SerializeField] AudioSource music;
    [SerializeField] AudioSource effects;

    private const string PREFS_MUSIC_VOLUME = "MusicVolume";
    private const string PREFS_EFFECTS_VOLUME = "EffectsVolume";

    public void PlaySound(AudioClip audio)
    {
        effects.PlayOneShot(audio);
    }

    public void SetMusicVolume(float volume)
    {
        music.volume = volume;
        PlayerPrefs.SetFloat(PREFS_MUSIC_VOLUME, volume);
    }

    public void SetEffectsVolume(float volume)
    {
        effects.volume = volume;
        PlayerPrefs.SetFloat(PREFS_EFFECTS_VOLUME, volume);
    }

    public float GetMusicVolume()
    {
        return music.volume;
    }

    public float GetEffectsVolume()
    {
        return effects.volume;
    }
}
