using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuWindow : MonoBehaviour
{
    [SerializeField] CanvasGroup window;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject soundSettings;
    [SerializeField] GameObject newGameQuestion;
    [SerializeField] GameObject exitGameQuestion;
    [SerializeField] Slider musicSlider;
    [SerializeField] Text musicVolumeText;
    [SerializeField] Slider effectsSlider;
    [SerializeField] Text effectsVolumeText;
    [Header("Звуки в меню")]
    [SerializeField] AudioClip menuButtonSound;


    public void ShowSoundSettings()
    {
        soundSettings.SetActive(true);
        mainMenu.SetActive(false);
        float musicVolume = AudioManager.Instance.GetMusicVolume();
        float effectsVolume = AudioManager.Instance.GetEffectsVolume();
        musicSlider.value = Mathf.FloorToInt(musicVolume * 100);
        effectsSlider.value = Mathf.FloorToInt(effectsVolume * 100);
        musicVolumeText.text = musicSlider.value + "%";
        effectsVolumeText.text = effectsSlider.value + "%";

    }

    public void MusicSliderChanged()
    {
        musicVolumeText.text = musicSlider.value + "%";
        AudioManager.Instance.SetMusicVolume(musicSlider.value / 100f);
    }

    public void EffectsSliderChanged()
    {
        effectsVolumeText.text = effectsSlider.value + "%";
        AudioManager.Instance.SetEffectsVolume(effectsSlider.value / 100f);
        AudioManager.Instance.PlaySound(menuButtonSound);
    }

    public void BackButton()
    {
        mainMenu.SetActive(true);
        soundSettings.SetActive(false);
        newGameQuestion.SetActive(false);
        exitGameQuestion.SetActive(false);
    }

    public void NewGameButton()
    {
        newGameQuestion.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void ExitGameButton()
    {
        exitGameQuestion.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void ResumeGameButton()
    {
        UIManager.Instance.HideWindow(window);
    }

    
}
