using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] Slider mySliderMaster;
    [SerializeField] Slider mySliderEffects;
    [SerializeField] Slider mySliderMusic;

    void Start(){

    }

    public void ChangeValueMaster(){
        float newValue= mySliderMaster.value;
        myMixer.SetFloat("MasterVolumen", newValue);
    }
    public void ChangeValueEffects(){
        float newValue= mySliderEffects.value;
        myMixer.SetFloat("Effects", newValue);
    }
    public void ChangeValueMusic(){
        float newValue= mySliderMusic.value;
        myMixer.SetFloat("Music", newValue);
    }
    public void MuteMaster(){
        myMixer.SetFloat("MasterVolumen", -80);
    }
    public void MuteEffects(){
        myMixer.SetFloat("Effects", -80);
    }
    public void MuteMusic(){
        myMixer.SetFloat("Music", -80);
    }
}
