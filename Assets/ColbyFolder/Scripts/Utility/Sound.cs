using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    [HideInInspector]
    public enum TypeOfSound { Music, SoundEffect };

    public string name;
    public AudioClip clip;
    public TypeOfSound type;
    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(0.1f, 3f)]
    public float pitch = 1f;
    public bool loop = false;
    [Tooltip("Bypass effects (Applied from filter components or global listener filters).")]
    public bool bypassEffects = false;
    [Tooltip("When set doesn't route the signal from an AudioSource into the global reverb associated with reverb zones.")]
    public bool bypassReverbZones = false;
    [Range(0f, 1.1f)]
    [Tooltip("The amount by which the signal from the AudioSource will be mixed into the global reverb associated with the Reverb Zones.")]
    public float revebZoneMix = 1f;
    [Tooltip("(Logarithmic rolloff) MaxDistance is the distance a sound stops attenuating at.")]
    public float maxDistance = float.PositiveInfinity;
    [Tooltip("Within the Min distance the AudioSource will cease to grow louder in volume.")]
    public float minDistance = 0f;
    [Range (0f, 5f)]
    [Tooltip("The higher this number, the more frequency change will happen based on the audio's relative motion to those hearing it")]
    public float dopplerLevel = 1f;

    [HideInInspector]
    public AudioMixerGroup audioMixer;
    [HideInInspector]
    public AudioSource audioSource;
}
