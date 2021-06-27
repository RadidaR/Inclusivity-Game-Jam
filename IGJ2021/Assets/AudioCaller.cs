using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCaller : MonoBehaviour
{
    public void PlayAudio(string audioName)
    {
        FindObjectOfType<AudioManagerScript>().PlaySound(audioName);
    }
}
