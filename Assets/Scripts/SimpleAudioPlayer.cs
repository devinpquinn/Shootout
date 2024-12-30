using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAudioPlayer : MonoBehaviour
{
    private AudioSource src;

    public List<AudioClip> clips;

    private void Awake()
    {
        src = GetComponent<AudioSource>();
    }

    public void PlaySound(int index)
    {
        src.PlayOneShot(clips[index]);
    }
}
