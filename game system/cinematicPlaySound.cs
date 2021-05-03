using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class cinematicPlaySound : MonoBehaviour
{
    [SerializeField]
    AudioClip clip;
    [SerializeField]
    bool loop;
    private AudioSource source;

    private void OnEnable()
    {
        source = GetComponent<AudioSource>();
        if (loop)
        {
            source.clip = clip;
            source.Play();
            source.loop = true;
        }
        else
        {
            source.PlayOneShot(clip);
        }
    }
}
