using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "ChannelManager", menuName = "ScriptableObjects/Audio/ChannelManager", order = 1)]
public class ChannelManager : ScriptableObject
{
    [SerializeField] string channelVolume;
    [SerializeField] float currentVolume;
    [SerializeField] private AudioMixer myMixer;
    bool isMuted;
    
    public void UpdateVolume(Slider mySlider){
        currentVolume= mySlider.value;
        myMixer.SetFloat(channelVolume, Mathf.Log10(currentVolume)*20f);
    }
    public void UpdateVolumeText(TMP_Text myText){
        float valueVolume= Mathf.Round(currentVolume*100);
        myText.text=valueVolume.ToString();
    }
    public void muteAudio(){
        if(isMuted){
            myMixer.SetFloat(channelVolume, Mathf.Log10(currentVolume)*20f);
            isMuted = false;
        }else{
            myMixer.SetFloat(channelVolume, -80);
            isMuted=true;
        }
    }
    
}
