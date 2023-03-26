using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] Toggle toggle;
    [SerializeField] Slider slider;
    [SerializeField] Button back;

    private void Awake()
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(Screen.resolutions.Select(x => x.width + "x" + x.height).Reverse().ToList());
        Debug.Log(Screen.width + "x" + Screen.height);
        dropdown.value = dropdown.options.FindIndex(x => x.text == Screen.width + "x" + Screen.height);
        toggle.isOn = Screen.fullScreen;
        if (PlayerPrefs.HasKey("volume")) slider.value = PlayerPrefs.GetFloat("volume", 1);
    }
    public void SetResolution(int option)
    { 
        var wh = dropdown.options[option].text.Split('x').Select(x => int.Parse(x)).ToArray();
        Screen.SetResolution(wh[0], wh[1], Screen.fullScreenMode);        
        PlayerPrefs.Save();
    }
    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);
        SoundManager.Instance.MusicVolume(volume);
        SoundManager.Instance.EffectsVolume(volume);
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
