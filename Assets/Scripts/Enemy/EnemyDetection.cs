using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public abstract class EnemyDetection : MonoBehaviour
{
    private float fovAngle = 196f;
    private float range = 4f;

    [SerializeField]
    private Transform fovPoint;
    [SerializeField]
    private Light2D fovLight;
    private Transform player;

    private GameObject _player;

    protected Vector2 movementDirection = Vector2.up;

    private PlayerAbilities playerAbilities;
    private PlayerDetection playerDetection;

    //Mecanica para detectar al jugador
    [SerializeField]
    private bool playerDetected = false;
    [SerializeField]
    private float timeToGetDetected = 1f;
    [SerializeField]
    private float detectionTimer = 0f;

    private bool playerGotDetected = false;

    void Start()
    {
        fovAngle = fovLight.pointLightOuterAngle;
        range = fovLight.pointLightOuterRadius;
        _player = GameObject.FindGameObjectWithTag("Player");
        player = _player.transform;
        playerAbilities = _player.GetComponent<PlayerAbilities>();
        playerDetection = _player.GetComponent<PlayerDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        if(fovPoint == null || player == null)
        {
            Debug.LogError("El campo de visión o el jugador no están asignados en el inspector.");
            return;
        }

        Vector2 dir = player.position - fovPoint.position;

        if (dir.sqrMagnitude > range * range)
        {
            playerDetected = false;
            detectionTimer = 0f;
            return;
        }

        BeforeGetAngle();

        float angle = GetAngle(dir);

        if(!CompareAngle(angle, fovAngle)){
            playerDetected = false;
            detectionTimer = 0f;
            return;
        }

        RaycastHit2D[] hits = Physics2D.RaycastAll(fovPoint.position, dir.normalized, range);
        GameObject playerGameObject = null;

        DrawDebugRays(dir);

        foreach (RaycastHit2D hit in hits)
        {
            if(hit.collider == null){
                playerDetected = false;
                break;
            }

            if(hit.collider.CompareTag("Enemy")){
                playerDetected = false;
                continue;
            }

            if(!hit.collider.CompareTag("Player"))
            {
                playerDetected = false;
                break;
            }

            if(hit.collider.CompareTag("Player"))
            {
                if(playerAbilities.IsCamouflageActive())
                {
                    playerDetected = false;
                    Debug.Log("Player is camouflaged, cannot be detected.");
                }
                else
                {
                    playerDetected = true;
                    playerGameObject = hit.collider.gameObject;
                    Debug.Log("Player is not camouflaged, can be detected.");
                }
                break;
            }        
        }

        if(playerDetected)
        {
            Debug.Log("Player Detected");
            detectionTimer += Time.deltaTime;
        }
        else
        {
            Debug.Log("Player Not Detected");
            detectionTimer = 0f;
        }

        if(detectionTimer >= timeToGetDetected)
        {
            if(playerGameObject != null && !playerGotDetected)
            {
                playerDetection.GotDetected();
                playerGotDetected = true;
            }
        }
    }

    protected virtual void BeforeGetAngle()
    {
        //Se llama a SetMovementDirection para que el enemigo pueda cambiar su dirección de movimiento antes de calcular el ángulo
    }

    protected abstract void SetMovementDirection(Vector2 direction);
    protected abstract float GetAngle(Vector2 enemyDir);
    protected abstract bool CompareAngle(float angle, float fovAngle);

    private void DrawDebugRays(Vector2 dir)
    {
        Debug.DrawRay(fovPoint.position, movementDirection * range, Color.yellow);
        Debug.DrawRay(fovPoint.position, dir.normalized * range, Color.red);
    }
}
