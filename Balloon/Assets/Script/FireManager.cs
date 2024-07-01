using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class FireManager : MonoBehaviour
{
    //Balloonのスピードによって変わる火の大きさの管理

    public AudioSource audioSource;
    public AudioClip[] audioClips;
    public GameObject fireObject;

    public float[] fireSize;

    private int nowAudioClipNum;

    private void Start()
    {
        SetFireScale();
    }

    public void SetFireScale()
    {
        if (BalloonManager.balloonFireLevel == 0 || BalloonManager.isFalling || BalloonManager.wait)
        {
            fireObject.transform.localScale = new Vector3(fireSize[0], fireSize[0], fireSize[0]);
            audioSource.Stop();
            nowAudioClipNum = 100;
        }
        else if (BalloonManager.balloonFireLevel == 1)
        {
            fireObject.transform.localScale = new Vector3(fireSize[1], fireSize[1], fireSize[1]);
            PlayAudioClip(0);
        }
        else
        {
            fireObject.transform.localScale = new Vector3(fireSize[2], fireSize[2], fireSize[2]);
            PlayAudioClip(1); 
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

        nowAudioClipNum = index;
    }
}
