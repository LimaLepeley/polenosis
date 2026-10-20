using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerAnimation : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    [SerializeField]
    private Sprite[] sprites; //0 caminando, 1 saltar, 2 caer
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void FlipSprite(bool dir)
    {
        spriteRenderer.flipX = dir;
    }

    public void ChangeAnimator(string name, bool value)
    {
        animator.SetBool(name, value);
        //
    }
}
