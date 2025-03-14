using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class MenuControl : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Volume Settings")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private GameObject confirmationPrompt = null;
    [SerializeField] private float defaultVolume = 5.0f;


    [Header("Gameplay Settings")]
    [SerializeField] private TMP_Text ControllerSenTextValue = null;
    [SerializeField] private Slider ControllerSenSlider = null;
    [SerializeField] private int defaultControllerSen = 5;
    public int mainControllerSen = 5;

    [Header("Graphic Settings")]
    [SerializeField] private TMP_Text brightnessTextValue = null;
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private float defaultBrightness = 1.0f;

    [SerializeField] private TMP_Dropdown qualityDropdown = null;

    private int _qualityLevel;
    private float _brightnessLevel;

    [Header("Resolution Settings")]
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    private int resolutionIndex;

    [Header("Levels to Load")]
    public string _newGameLevel;

    public void Start(){
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++){
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height){
                currentResolutionIndex = i;
            }
            resolutionIndex = currentResolutionIndex;
        }    
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        
    }


    public void SetResolution(int resolutionIndex3){
    
        //Resolution resolution = resolutions[resolutionIndex];
        resolutionIndex = resolutionIndex3;
        //Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    
    }


    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(_newGameLevel);
    }

    public void ExitButton(){
        Application.Quit();
    }
    
    public void SetVolume(float volume){

        volumeTextValue.text = volume.ToString("0.0");
    }

    public void VolumeApply(){
        AudioListener.volume = float.Parse(volumeTextValue.text);
        PlayerPrefs.SetFloat("masterVolume", float.Parse(volumeTextValue.text));
        StartCoroutine(ConfirmationBox());
    }

    public void VolumeSlider(){
        volumeTextValue.text = volumeSlider.value.ToString("0.0");
    }
    
    public IEnumerator ConfirmationBox(){
        confirmationPrompt.SetActive(true);

        yield return new WaitForSeconds(2);

        confirmationPrompt.SetActive(false);
    }
    
    public void ResetButton(string MenuType){
        if(MenuType == "Audio"){
            //AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeTextValue.text = defaultVolume.ToString("0.0");
            //VolumeApply();
        }
        if(MenuType == "Gameplay"){
            ControllerSenSlider.value = defaultControllerSen;
            ControllerSenTextValue.text = defaultControllerSen.ToString("0");
            mainControllerSen = defaultControllerSen;
            //GameplayApply();
        }
        if(MenuType == "Graphics"){
            brightnessSlider.value = defaultBrightness;
            brightnessTextValue.text = defaultBrightness.ToString("0.0");

            qualityDropdown.value = 1;
            QualitySettings.SetQualityLevel(1);
            qualityDropdown.RefreshShownValue();

            Resolution currentResolution = Screen.currentResolution;
            //Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
            resolutionDropdown.value = resolutions.Length;
            //GraphicsApply();
        }
    }

    public void onOpenMenuPress(){
        float volume = PlayerPrefs.GetFloat("masterVolume", 1.0f);
        volumeTextValue.text = volume.ToString("0.0");
        Debug.Log("Vaolume: " + PlayerPrefs.GetFloat("masterVolume"));
        volumeSlider.value = volume;

        float brightness = PlayerPrefs.GetFloat("masterBrightness", 1.0f);
        brightnessTextValue.text = brightness.ToString("0.0");
        brightnessSlider.value = brightness;

        int quality = PlayerPrefs.GetInt("masterQuality", 1);
        qualityDropdown.value = quality;

        int currentResolutionIndex2 = PlayerPrefs.GetInt("resolutionIndex", 1);
        resolutionDropdown.value = currentResolutionIndex2;

    }



    public void SetControllerSen(float sensitivity){
        mainControllerSen = Mathf.RoundToInt(sensitivity);
        ControllerSenTextValue.text = sensitivity.ToString("0");
    }

    public void GameplayApply(){
        PlayerPrefs.SetInt("mainControllerSen", mainControllerSen);
        StartCoroutine(ConfirmationBox());
    }

    public void SetBrightness(float brightness){
        _brightnessLevel = brightness;
        brightnessTextValue.text = brightness.ToString("0.0");
    }

    public void SetQuality(int qualityIndex){
        _qualityLevel = qualityIndex;
        
    }

    public void GraphicsApply(){
        PlayerPrefs.SetFloat("masterBrightness", _brightnessLevel);
        PlayerPrefs.SetInt("masterQuality", _qualityLevel);
        QualitySettings.SetQualityLevel(_qualityLevel);


        PlayerPrefs.SetInt("resolutionIndex", resolutionIndex);
        // 1) Grab the chosen Resolution
        Resolution chosenRes = resolutions[resolutionIndex]; 
        
        // 2) Actually set the resolution
        Screen.SetResolution(chosenRes.width, chosenRes.height, true);

        StartCoroutine(ConfirmationBox());
    }

    public void BrightnessSlider(){
        brightnessTextValue.text = brightnessSlider.value.ToString("0.0");
    }
 

}