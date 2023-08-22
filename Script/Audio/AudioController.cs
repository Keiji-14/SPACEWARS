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

    // ゲーム起動時に以前に設定していた情報を取得
    void Start()
    {
        SetMasterSlider();
        SetBGMSlider();
        SetSESlider();

        //スライダーを動かした時の処理を登録
        masterSlider.onValueChanged.AddListener(SetAudioMixerMaster);
        bgmSlider.onValueChanged.AddListener(SetAudioMixerBGM);
        seSlider.onValueChanged.AddListener(SetAudioMixerSE);
    }

    // 主音量の設定
    public void SetAudioMixerMaster(float value)
    {
        //5段階補正
        value /= 5;
        //value /= PlayerPrefs.GetFloat("Master", 0);
        //-80~0に変換
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);
        //audioMixerに代入
        audioMixer.SetFloat("Master", volume);
        PlayerPrefs.SetFloat("MasterVolume", volume);
        PlayerPrefs.SetFloat("Master", value);
    }

    public void SetMasterSlider()
    {
        var value = PlayerPrefs.GetFloat("Master", 0);
        masterSlider.value = value * 5;
    }

    // BGMの設定
    public void SetAudioMixerBGM(float value)
    {
        //5段階補正
        value /= 5;
        //value /= PlayerPrefs.GetFloat("BGM", 0);
        //-80~0に変換
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);
        //audioMixerに代入
        audioMixer.SetFloat("BGM", volume);
        PlayerPrefs.SetFloat("BGMVolume", volume);
        PlayerPrefs.SetFloat("BGM", value);
    }

    public void SetBGMSlider()
    {
        var value = PlayerPrefs.GetFloat("BGM", 0);
        bgmSlider.value = value * 5;
    }

    //SEの設定
    public void SetAudioMixerSE(float value)
    {
        //5段階補正
        value /= 5;
        //value /= PlayerPrefs.GetFloat("SE", 0);
        //-80~0に変換
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);
        //audioMixerに代入
        audioMixer.SetFloat("SE", volume);
        PlayerPrefs.SetFloat("SEVolume", volume);
        PlayerPrefs.SetFloat("SE", value);
    }

    public void SetSESlider()
    {
        var value = PlayerPrefs.GetFloat("SE", 0);
        seSlider.value = value * 5;
    }
}