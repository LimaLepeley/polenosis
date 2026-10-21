using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerSounds : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] clips; //0 Jump, 1 Second Jump, 2 Camouflage
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudioClip(int clip)
    {
        Debug.Log("Sound played: " + clip);
        audioSource.Stop();
        audioSource.clip = clips[clip];
        audioSource.Play();
    }
}
