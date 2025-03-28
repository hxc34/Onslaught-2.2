using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class Brightness : MonoBehaviour
{
    public static Brightness instance;
    [SerializeField] private Slider brightnessSlider = null;
    public PostProcessProfile brightness;
    public PostProcessLayer layer;
    AutoExposure exposure;
    // Start is called before the first frame update


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
         brightness.TryGetSettings(out exposure);
    }

    // Update is called once per frame
    public void SetBrightness(float brightness){
    if (brightness !=0){
        exposure.keyValue.value = brightness;
    }
    else {
        exposure.keyValue.value = 0.05f;
    }
    
    }
}
