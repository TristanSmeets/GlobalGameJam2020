using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagement : MonoBehaviour
{
    public List<AudioClip> AudioClips;

    void Start()
    {
        GetComponent<AudioSource>().Play();
    }

    public void PlayAudioClip(AudioClip pAudioClip)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = pAudioClip;
        audioSource.Play();
        StartCoroutine(DestroyAudioSource(audioSource));
    }

    private IEnumerator DestroyAudioSource(AudioSource pAudioSource)
    {
        yield return new WaitForSeconds(pAudioSource.clip.length);
        Destroy(pAudioSource);
    }
}
