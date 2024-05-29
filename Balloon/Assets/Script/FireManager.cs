using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScale : MonoBehaviour
{
    //Balloonのスピードによって変わる火の大きさの管理

    public AudioSource audioSource;
    public AudioClip[] audioClips;

    private int nowAudioClipNum;

    private void Start()
    {
        transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
    }

    public void SetFireScale(float value)
    {
        if (value <= 0.0)
        {
            transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
            audioSource.Stop();
            nowAudioClipNum = 100;
        }
        else if (value <= 0.1)
        {
            transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            PlayAudioClip(0);
            nowAudioClipNum = 0;
        }
        else if (value <= 0.2)
        {
            transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            PlayAudioClip(1); 
            nowAudioClipNum = 1;
        }
        else if (value <= 0.3)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            PlayAudioClip(2); 
            nowAudioClipNum = 2;
        }
        else
        {
            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            PlayAudioClip(2);
            nowAudioClipNum = 2;
        }
    }

    void PlayAudioClip(int index)
    {
        if (audioSource != null && audioClips != null && index >= 0 && index < audioClips.Length && nowAudioClipNum != index)
        {
            audioSource.clip = audioClips[index];
            audioSource.Play();
            nowAudioClipNum = index;
        }
    }
}
