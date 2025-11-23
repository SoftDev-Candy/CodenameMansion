using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
public class OptionsMenu : MonoBehaviour
{

    public GameObject MainMenu;
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider ambienceVolumeSlider;
    public Slider sfxVolumeSlider;

    private void Start()
    {

        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        
    }
    public void BackButton()
    {
        Debug.Log("Options button clicked. Opening options menu...");
        MainMenu.SetActive(true);
        CloseOptions();
    }

    public void CloseOptions()
    {
        gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        //SetVolume(AudioManager.instance.masterVolume);
        AudioManager.instance.masterVolume = masterVolumeSlider.value;
        AudioManager.instance.ambienceVolume = ambienceVolumeSlider.value;
        AudioManager.instance.musicVolume = musicVolumeSlider.value;
        AudioManager.instance.SFXVolume = sfxVolumeSlider.value;
    }

    // Tryng to set volume using FMOD
    public void SetVolume(float volume)
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("MasterVolume", volume);
        PlayerPrefs.SetFloat("MasterVolume", volume); // Save volume setting
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetFloat("MasterVolume"));
    }
}
