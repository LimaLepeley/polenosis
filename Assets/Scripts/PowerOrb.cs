using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]

[RequireComponent(typeof(AudioSource))]
public class PowerOrb : MonoBehaviour
{
    [SerializeField]
    private int powerType;
    private SpriteRenderer spriteRenderer;
    private GameDirector gameDirector;
    private AudioSource audioSource;
    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField]
    private CameraFollow cameraFollow;
    [SerializeField]
    private Sprite[] sprites; //0 laguna, 1 opal
    [SerializeField]
    private GameObject lightSource;
    [SerializeField]
    private Animator[] glowAnimator;
    [SerializeField]
    private ParticleSystem[] particles; //0 big pollen, 1 small pollen
    private bool activated;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        gameDirector = GameObject.FindGameObjectWithTag("Director").GetComponent<GameDirector>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !activated)
        {
            activated = true;
            playerMovement.SetCanMove(false);
            gameDirector.StopMusicBeforePowerUp();
            StartCoroutine(PowerUpCinematic(collision.gameObject));
            
        }
    }

    private IEnumerator PowerUpCinematic(GameObject player)
    {
        StartCoroutine(cameraFollow.MoveToObjective(transform));

        while(!cameraFollow.GetCinematicEnd())
        {
            yield return null;
        }

        particles[0].Play();

        yield return new WaitForSeconds(1f);

        audioSource.Play();

        yield return new WaitForSeconds(4.5f);

        glowAnimator[0].Play("Base",0,0f);

        yield return new WaitForSeconds(0.5f);

        glowAnimator[1].Play("Base",0,0f);

        yield return new WaitForSeconds(0.5f);

        glowAnimator[2].Play("Base",0,0f);

        yield return new WaitForSeconds(0.5f);

        player.GetComponent<PlayerAbilities>().UnlockAbility(powerType);

        lightSource.SetActive(true);
        ChangeSprite();

        yield return new WaitForSeconds(1f);

        cameraFollow.SetFollowPlayer(true);

        yield return new WaitForSeconds(0.1f);

        gameDirector.ResumeMusicAfterPowerUp();
        playerMovement.SetCanMove(true);
    }

    private void ChangeSprite()
    {
        spriteRenderer.sprite = sprites[powerType - 1];
    }
}
