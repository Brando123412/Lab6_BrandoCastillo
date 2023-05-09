using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "AudioScript", menuName = "ScriptableObjects/Audio/Soundscriptableobjects", order = 2)]
public class Soundscriptableobjects : ScriptableObject
{
    [SerializeField] AudioClip myAudio;
    [SerializeField] AudioMixerGroup myGroup;
    public void CreateSound(){
        GameObject audioGameObject = new GameObject();
        AudioSource myAudioSource = audioGameObject.AddComponent<AudioSource>();

        myAudioSource.outputAudioMixerGroup = myGroup;
        myAudioSource.PlayOneShot(myAudio);
        Instantiate(audioGameObject,Vector3.zero,Quaternion.identity);
        Destroy(audioGameObject,1.5f);

    }
    
}
