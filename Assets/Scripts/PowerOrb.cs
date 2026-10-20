using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PowerOrb : MonoBehaviour
{
    [SerializeField]
    private int powerType;
    private SpriteRenderer spriteRenderer;
    private GameDirector gameDirector;
    [SerializeField]
    private Sprite[] sprites; //0 laguna, 1 opal
    [SerializeField]
    private GameObject lightSource;
    private bool activated;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameDirector = GameObject.FindGameObjectWithTag("Director").GetComponent<GameDirector>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !activated)
        {
            activated = true;
            collision.gameObject.GetComponent<PlayerAbilities>().UnlockAbility(powerType);
            ChangeSprite();
            gameDirector.ChangeSoundtrack();
            lightSource.SetActive(true);
        }
    }

    private void ChangeSprite()
    {
        spriteRenderer.sprite = sprites[powerType - 1];
    }
}
