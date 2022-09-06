using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Language
{
    public string language;
    public string langCode;
    public string langShort;
    public Sprite flag;
}

public class SettingsHandler : MonoBehaviour
{
    
    private AudioManager audioManager;
    private PlayerStats playerStats;

    [SerializeField]
    private List<Language> languages;

    [SerializeField]
    private TMP_InputField nicknameInput;

    [SerializeField]
    private Slider SFXslider;

    [SerializeField]
    private Slider MSCslider;

    [SerializeField]
    private TextMeshProUGUI languageText;

    [SerializeField]
    private Image languageFlag;


    int currentLang;

    [SerializeField]
    private VoiceController voiceController;

    [SerializeField]
    private GameObject whole;
    

    // Start is called before the first frame update
    void Start()
    {
        playerStats = Camera.main.gameObject.GetComponent<PlayerStats>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

        MSCslider.value = PlayerPrefs.GetFloat("Musicvolume", 1f);
        SFXslider.value = PlayerPrefs.GetFloat("SFXvolume", 1f);

        nicknameInput.text = playerStats.GetName();
        SetLanguage(PlayerPrefs.GetString("language", "English"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLanguage(string language)
    {
        int i = 0;
        foreach (Language l in languages)
        {
            
            if (l.language == language)
            {
                currentLang = i;
                PlayerPrefs.SetString("language", l.language);
                languageText.text = "Voice: " + l.language;
                languageFlag.sprite = l.flag;

                voiceController.Setup(l.langCode);

                break;
            }
            i++;
        }
    }

    public void ChangeLanguage()
    {
        if (currentLang < languages.Count - 1)
            currentLang++;
        else
            currentLang = 0;

        SetLanguage(languages[currentLang].language);
    }

    public void ChangeSFX()
    {
        PlayerPrefs.SetFloat("SFXvolume", SFXslider.value);
        audioManager.SetVolume();
        
    }

    public void ChangeMusic()
    {
        PlayerPrefs.SetFloat("Musicvolume", MSCslider.value);
        audioManager.SetVolume();
    }

    public void ChangeNickname()
    {
        playerStats.SetName(nicknameInput.text);
    }

    public void OpenPage(string url)
    {
        Application.OpenURL(url);
    }


    public void SetSettings(bool turn)
    {
        whole.SetActive(turn);
    }

}
