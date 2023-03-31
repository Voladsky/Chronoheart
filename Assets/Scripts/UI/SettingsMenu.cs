using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] Toggle toggle;
    [SerializeField] Slider SFXSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Button back;

    private void Awake()
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(Screen.resolutions.Select(x => x.width + "x" + x.height).Reverse().ToList());
        Debug.Log(Screen.width + "x" + Screen.height);
        dropdown.value = dropdown.options.FindIndex(x => x.text == Screen.width + "x" + Screen.height);
        toggle.isOn = Screen.fullScreen;
        SetAllVolume();
    }

    public void SetAllVolume()
    {
        if (PlayerPrefs.HasKey("SFXVolume")) SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1);
        if (PlayerPrefs.HasKey("MusicVolume")) musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1);
    }
    public void SetResolution(int option)
    { 
        var wh = dropdown.options[option].text.Split('x').Select(x => int.Parse(x)).ToArray();
        Screen.SetResolution(wh[0], wh[1], Screen.fullScreenMode);        
        PlayerPrefs.Save();
    }
    public void SetSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("SFXVolume", volume);
        SoundManager.Instance.EffectsVolume(volume);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        SoundManager.Instance.MusicVolume(volume);
        PlayerPrefs.Save();
    }
    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Leave();
        }
    }

    void Leave()
    {
        back.onClick.Invoke();
    }
}
