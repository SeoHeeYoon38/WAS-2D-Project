using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio; 
using TMPro;

public class SettingsUIManager : MonoBehaviour
{
    [Header("UI 패널")]
    public GameObject settingsPanel; 

    [Header("오디오 믹서 ")]
    public AudioMixer audioMixer;

    [Header("볼륨 슬라이더")]
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;
    public Slider voiceSlider;

    [Header("볼륨 숫자 텍스트")]
    public TextMeshProUGUI masterText;
    public TextMeshProUGUI bgmText;
    public TextMeshProUGUI sfxText;
    public TextMeshProUGUI voiceText;

    [Header("언어 설정")]
    public TextMeshProUGUI languageText;

    public static bool isKoreanLanguage = true;

    private void Start()
    {
        SetSliderRange(masterSlider);
        SetSliderRange(bgmSlider);
        SetSliderRange(sfxSlider);
        SetSliderRange(voiceSlider);

        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        bgmSlider.onValueChanged.AddListener(SetBgmVolume);
        sfxSlider.onValueChanged.AddListener(SetSfxVolume);
        voiceSlider.onValueChanged.AddListener(SetVoiceVolume);

        if (settingsPanel != null) settingsPanel.SetActive(false);
    }

    public void OpenSettings()
    {
        if (settingsPanel != null) settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        if (settingsPanel != null) settingsPanel.SetActive(false);
    }

    private void SetSliderRange(Slider slider)
    {
        if (slider == null) return;
        slider.minValue = 0;
        slider.maxValue = 100;
        slider.wholeNumbers = true;
    }

    private void SetMasterVolume(float value)
    {
        if (masterText) masterText.text = value.ToString();

        float volume = value <= 0 ? -80f : Mathf.Log10(value / 100f) * 20f;
        if (audioMixer != null) audioMixer.SetFloat("Master", volume);
    }

    private void SetBgmVolume(float value)
    {
        if (bgmText) bgmText.text = value.ToString();
        float volume = value <= 0 ? -80f : Mathf.Log10(value / 100f) * 20f;
        if (audioMixer != null) audioMixer.SetFloat("BGM", volume);
    }

    private void SetSfxVolume(float value)
    {
        if (sfxText) sfxText.text = value.ToString();
        float volume = value <= 0 ? -80f : Mathf.Log10(value / 100f) * 20f;
        if (audioMixer != null) audioMixer.SetFloat("SFX", volume);
    }

    private void SetVoiceVolume(float value)
    {
        if (voiceText) voiceText.text = value.ToString();
        float volume = value <= 0 ? -80f : Mathf.Log10(value / 100f) * 20f;
        if (audioMixer != null) audioMixer.SetFloat("Voice", volume);
    }

    public void ToggleLanguage()
    {
        isKoreanLanguage = !isKoreanLanguage;

        if (isKoreanLanguage)
        {
            languageText.text = "한국어";
        }
        else
        {
            languageText.text = "English";
        }
    }
}