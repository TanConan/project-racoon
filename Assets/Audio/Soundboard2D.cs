using System;
using System.Collections.Generic;
using UnityEngine;

public class Soundboard2D : MonoBehaviour
{
  public List<SoundClip> clips;
  private AudioSource _audioSource;
  private Dictionary<string, AudioClip> _clipDictionary;

  private void Awake()
  {
    _audioSource = GetComponent<AudioSource>();
    _clipDictionary = new Dictionary<string, AudioClip>();
    foreach (var item in clips) _clipDictionary[item.name] = item.clip;
  }

  public void PlaySound(string soundName)
  {
    if (_clipDictionary.ContainsKey(soundName))
      _audioSource.PlayOneShot(_clipDictionary[soundName]);
    else
      Debug.LogWarning("Sound: " + soundName + " wurde nicht gefunden!");
  }

  [Serializable]
  public struct SoundClip
  {
    public string name;
    public AudioClip clip;
  }
}