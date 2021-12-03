using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource bgSound;
    public AudioMixer Mixer;
    protected override void Awake()
    {
        DontDestroyOnLoad(this);
        
    }
    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject go = new GameObject(sfxName + "Sound");
        AudioSource audioSource = go.AddComponent<AudioSource>();
        
        audioSource.clip = clip;
        audioSource.outputAudioMixerGroup = Mixer.FindMatchingGroups("SFX")[0];
        audioSource.Play();

        Destroy(go, clip.length);
    }
    private void Update()
    {
        Sound();
    }
    public void BgSound(AudioClip clip)
    {
        bgSound.clip = clip;
        bgSound.loop = true;
        bgSound.volume = 0.1f;
        bgSound.Play();
    }
    
    void Sound()
    {
        Mixer.SetFloat("BGSound", PlayerPrefs.GetFloat("BGSound"));
        Mixer.SetFloat("SFX", PlayerPrefs.GetFloat("SFX"));
        
    }
}
