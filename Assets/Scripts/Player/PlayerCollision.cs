using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerCollision : MonoBehaviour
{
    private PlayerMovement playerMovement;

    private bool playerScaped = false;
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("WinGround"))
        {
            playerMovement.SetCanMove(false);
            playerScaped = true;
        }
    }

    //Getter
    public bool GetPlayerScaped()
    {
        return playerScaped;
    }
}
