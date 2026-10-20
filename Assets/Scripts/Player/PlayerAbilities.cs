using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerSounds))]
public class PlayerAbilities : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerControls playerControls;
    private SpriteRenderer playerSpriteRenderer;
    private PlayerSounds playerSounds;

    [SerializeField]
    private bool camouflageUnlocked = false;
    [SerializeField]
    private bool canCamouflage = false;
    [SerializeField]
    private float camouflageDuration = 5f;
    [SerializeField]
    private float camouflageTimer = 0f;
    private bool isCamouflageActive = false;
    [SerializeField]
    private float camouflageCooldown = 10f;
    [SerializeField]
    private float camouflageCooldownTimer = 0f;
    // Start is called before the first frame update
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerControls = new();
        playerSounds = GetComponent<PlayerSounds>();
    }

    // Update is called once per frame
    void Update()
    {
        if(camouflageUnlocked)
        {
            if(!canCamouflage && !isCamouflageActive)
            {
                if(camouflageCooldownTimer < camouflageCooldown)
                {
                    camouflageCooldownTimer += Time.deltaTime;
                }
                else
                {
                    canCamouflage = true;
                    playerSounds.PlayAudioClip(2);
                    camouflageCooldownTimer = 0f;
                }
            }
            else if(isCamouflageActive)
            {
                if(camouflageTimer < camouflageDuration)
                {
                    camouflageTimer += Time.deltaTime;
                }
                else
                {
                    isCamouflageActive = false;
                    playerSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
                }
            }
        }
    }

    void OnEnable()
    {
        playerControls.Enable();
        playerControls.Base.Camouflage.started += Camouflage;
    }

    void OnDisable()
    {
        playerControls.Disable();
        playerControls.Base.Camouflage.started -= Camouflage;
    }

    public void UnlockAbility(int abilityType)
    {
        switch (abilityType)
        {
            case 1:
                playerMovement.SetDoubleJumpUnlocked(true);
                break;
            case 2:
                camouflageUnlocked = true;
                camouflageCooldownTimer = camouflageCooldown;
                canCamouflage = true;
                break;
            default:
                break;
        }
    }

    private void Camouflage(InputAction.CallbackContext context)
    {
        if(canCamouflage && camouflageUnlocked)
        {
            canCamouflage = false;
            isCamouflageActive = true;
            camouflageTimer = 0f;
            playerSpriteRenderer.color = new Color(1f, 1f, 1f, 0.15f);
        }
    }

    public bool IsCamouflageActive()
    {
        return isCamouflageActive;
    }
}
