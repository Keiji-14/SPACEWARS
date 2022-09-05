using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider seSlider;

    void Start()
    {
        //ƒXƒ‰ƒCƒ_[‚ğ“®‚©‚µ‚½‚Ìˆ—‚ğ“o˜^
        masterSlider.onValueChanged.AddListener(SetAudioMixerMaster);
        bgmSlider.onValueChanged.AddListener(SetAudioMixerBGM);
        seSlider.onValueChanged.AddListener(SetAudioMixerSE);
    }

    // å‰¹—Ê‚Ìİ’è
    public void SetAudioMixerMaster(float value)
    {
        //5’iŠK•â³
        value /= 5;
        //value /= PlayerPrefs.GetFloat("Master", 0);
        //-80~0‚É•ÏŠ·
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);
        //audioMixer‚É‘ã“ü
        audioMixer.SetFloat("Master", volume);
        PlayerPrefs.SetFloat("Master", value);
        
    }

    // BGM‚Ìİ’è
    public void SetAudioMixerBGM(float value)
    {
        //5’iŠK•â³
        value /= 5;
        //value /= PlayerPrefs.GetFloat("BGM", 0);
        //-80~0‚É•ÏŠ·
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);
        //audioMixer‚É‘ã“ü
        audioMixer.SetFloat("BGM", volume);
        PlayerPrefs.SetFloat("BGM", value);
        
    }

    //SE‚Ìİ’è
    public void SetAudioMixerSE(float value)
    {
        //5’iŠK•â³
        value /= 5;
        //value /= PlayerPrefs.GetFloat("SE", 0);
        //-80~0‚É•ÏŠ·
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);
        //audioMixer‚É‘ã“ü
        audioMixer.SetFloat("SE", volume);
        PlayerPrefs.SetFloat("SE", value);
    }
}