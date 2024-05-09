using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixerGroup masterMixer;
    public AudioMixerGroup musicMixer;
    public AudioMixerGroup soundEffectMixer;
    public Sound[] sounds;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }

    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound != null)
        {
            AudioSource audioSource = GetComponent<ObjectPool>().GetPooledObject().GetComponent<AudioSource>();
            audioSource.gameObject.SetActive(true);
            sound.audioSource = audioSource;
            sound.audioSource.clip = sound.clip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
            sound.audioSource.loop = sound.loop;
            sound.audioSource.maxDistance = sound.maxDistance;
            sound.audioSource.minDistance = sound.minDistance;
            sound.audioSource.bypassEffects = sound.bypassEffects;
            sound.audioSource.bypassReverbZones = sound.bypassReverbZones;
            sound.audioSource.reverbZoneMix = sound.revebZoneMix;
            sound.audioSource.maxDistance = sound.maxDistance;
            sound.audioSource.minDistance = sound.minDistance;
            sound.audioSource.dopplerLevel = sound.dopplerLevel;
            //0 spactial blend means the sound is 2D and will play equally everywhere
            sound.audioSource.spatialBlend = 0f;
            if (sound.type == Sound.TypeOfSound.Music)
            {
                audioSource.outputAudioMixerGroup = musicMixer;
            }
            else
            {
                audioSource.outputAudioMixerGroup = soundEffectMixer;
            }
            sound.audioSource.Play();
            StartCoroutine(SoundFinished(audioSource));
        }
        else
        {
            Debug.LogWarning("Sound " + name + " was not found");
        }
    }

    public void PlayAtPosition(string name, Vector3 position)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound != null)
        {
            AudioSource audioSource = GetComponent<ObjectPool>().GetPooledObject().GetComponent<AudioSource>();
            audioSource.gameObject.SetActive(true);
            audioSource.transform.position = position;
            sound.audioSource = audioSource;
            sound.audioSource.clip = sound.clip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
            sound.audioSource.loop = sound.loop;
            sound.audioSource.maxDistance = sound.maxDistance;
            sound.audioSource.minDistance = sound.minDistance;
            sound.audioSource.bypassEffects = sound.bypassEffects;
            sound.audioSource.bypassReverbZones = sound.bypassReverbZones;
            sound.audioSource.reverbZoneMix = sound.revebZoneMix;
            sound.audioSource.maxDistance = sound.maxDistance;
            sound.audioSource.minDistance = sound.minDistance;
            sound.audioSource.dopplerLevel = sound.dopplerLevel;
            //1 spactial blend means the sound is 3D and will play louder when closer
            sound.audioSource.spatialBlend = 1f;
            sound.audioSource.rolloffMode = AudioRolloffMode.Custom;
            if (sound.type == Sound.TypeOfSound.Music)
            {
                audioSource.outputAudioMixerGroup = musicMixer;
            }
            else
            {
                audioSource.outputAudioMixerGroup = soundEffectMixer;
            }
            sound.audioSource.Play();
            StartCoroutine(SoundFinished(audioSource));
        }
        else
        {
            Debug.LogWarning("Sound " + name + " was not found");
        }
    }

    public void PlayOnObject(string name, GameObject audioObject)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        AudioSource audioSource;
        if (sound != null)
        {
            GameObject pooledObject = GetComponent<ObjectPool>().GetPooledObject();
            if (pooledObject != null)
            {
                audioSource = pooledObject.GetComponent<AudioSource>();
            }
            else
            {
                Debug.Log("There were no pooled AudioSources to play the sound with, don't forget to set them back inactive when they are done playing");
                return;
            }
            audioSource.gameObject.SetActive(true);
            audioSource.transform.SetParent(audioObject.transform);
            sound.audioSource = audioSource;
            sound.audioSource.clip = sound.clip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
            sound.audioSource.loop = sound.loop;
            sound.audioSource.maxDistance = sound.maxDistance;
            sound.audioSource.minDistance = sound.minDistance;
            sound.audioSource.bypassEffects = sound.bypassEffects;
            sound.audioSource.bypassReverbZones = sound.bypassReverbZones;
            sound.audioSource.reverbZoneMix = sound.revebZoneMix;
            sound.audioSource.maxDistance = sound.maxDistance;
            sound.audioSource.minDistance = sound.minDistance;
            sound.audioSource.dopplerLevel = sound.dopplerLevel;
            //1 spactial blend means the sound is 3D and will play louder when closer
            sound.audioSource.spatialBlend = 1f;
            sound.audioSource.rolloffMode = AudioRolloffMode.Custom;
            if (sound.type == Sound.TypeOfSound.Music)
            {
                audioSource.outputAudioMixerGroup = musicMixer;
            }
            else
            {
                audioSource.outputAudioMixerGroup = soundEffectMixer;
            }
            sound.audioSource.transform.position = sound.audioSource.transform.parent.position;
            sound.audioSource.Play();
            StartCoroutine(SoundFinished(audioSource));
        }
        else
        {
            Debug.LogWarning("Sound " + name + " was not found");
        }
    }

    private IEnumerator SoundFinished(AudioSource audioSource)
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        //If the audioSource is parented to something else, set it back to the AudioManager
        audioSource.transform.SetParent(transform);
        audioSource.gameObject.SetActive(false);
    }

    //This is called just before loading into a new scene to gather all outstanding sounds back to the Singleton AudioManager so it doesn't lose them
    public void GatherAllSounds()
    {
        foreach (GameObject audioSourceObject in GetComponent<ObjectPool>().pooledObjects)
        {
            if (audioSourceObject.transform.parent != gameObject)
            {
                audioSourceObject.transform.SetParent(transform);
                audioSourceObject.gameObject.SetActive(false);
            }
        }
    }

    public void Stop(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound != null)
        {
            if (sound.audioSource != null)
            {
                sound.audioSource.Stop();
                sound.audioSource.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Sound not playing");
            }
        }
        else
        {
            Debug.LogWarning("Sound " + name + " was not found");
        }
    }

    public void StopAll()
    {
        foreach (GameObject audioSourceObject in GetComponent<ObjectPool>().pooledObjects)
        {
            if (audioSourceObject.activeSelf)
            {
                audioSourceObject.SetActive(false);
            }
        }
    }
}
