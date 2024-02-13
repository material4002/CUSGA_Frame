using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Mat.Kits
{
    [CreateAssetMenu(fileName ="AudioList",menuName ="ScriptableObject/AudioList",order = 1)]
    public class AudioList : SO_SingleTon<AudioList>
    {
        public List<AudioNode> Music;
        public List<AudioNode> Sound;
        public List<AudioNode> BGM;
    }
}

[Serializable]
public class AudioNode
{
    public string name = "newAudio";
    public AudioClip clip;
    [Range(0f,1f)]
    public float volume = 1f;
}
