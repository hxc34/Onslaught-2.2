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
    [SerializeField] private float defaultVolume = 1.0f;

    [Header("Levels to Load")]
    public string _newGameLevel;

    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(_newGameLevel);
    }

    public void ExitButton(){
        Application.Quit();
    }
    
    public void SetVolume(float volume){

        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");
    }

    public void VolumeApply(){
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
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
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeTextValue.text = defaultVolume.ToString("0.0");
            VolumeApply();
        }
    }



}
