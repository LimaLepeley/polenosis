using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameDirector : MonoBehaviour
{
    [SerializeField]
    private PlayerCollision playerCollision;
    [SerializeField]
    private WinningScreen winningScreen;
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip[] songs;

    private int phase = 0;

    private bool winningCutscene = true;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        playerCollision = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCollision>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerCollision.GetPlayerScaped())
        {
            if(winningCutscene)
            {
                StartCoroutine(winningScreen.LoadResultScene());
                winningCutscene = false;
            }
        }
    }

    public void ChangeSoundtrack()
    {
        phase++;

        float actualTime = audioSource.time;
        audioSource.Stop();

        if(phase == 1)
        {
            audioSource.clip = songs[phase - 1];
            
        }
        else if(phase == 2)
        {
            audioSource.clip = songs[phase - 1];
        }

        audioSource.time = actualTime;
        audioSource.Play();
    }
}
