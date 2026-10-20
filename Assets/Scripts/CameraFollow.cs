using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private float followSpeed = 10f;
    [SerializeField]
    private float yOffset = 2.5f;
    [SerializeField]
    private float normalYOffset = 2.5f;
    [SerializeField]
    private Transform playerTransform;

    private bool followPlayer = true;

    private bool cinematicEnd = false;

    void Awake()
    {
        yOffset = normalYOffset ;
    }

    void OnEnable()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(followPlayer)
        {
            Vector3 newPosition = new Vector3(playerTransform.position.x, playerTransform.position.y + yOffset, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, followSpeed * Time.deltaTime);
        }
    }

    public IEnumerator MoveToObjective(Transform objective)
    {
        followPlayer = false;

        yield return new WaitForSeconds(0.3f);

        while(Vector3.Distance(transform.position, new Vector3(objective.position.x, objective.position.y, transform.position.z)) > 0.01f)
        {
            Vector3 newPosition = new Vector3(objective.position.x, objective.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, 2f * Time.deltaTime);
            yield return null;
        }

        transform.position = new Vector3(
            objective.position.x,
            objective.position.y,
            transform.position.z
        );

        yield return new WaitForSeconds(0.2f);

        cinematicEnd = true;
    }

    public void SetFollowPlayer(bool isFollowingPlayer)
    {
        followPlayer = isFollowingPlayer;
    }

    public void SetYOffset(float newOffset)
    {
        yOffset = newOffset;
    }

    public void ResetYOffset()
    {
        yOffset = normalYOffset;
    }

    public bool GetCinematicEnd()
    {
        return cinematicEnd;
    }

    public void SetCinematicEnd(bool cinematicEnd)
    {
        this.cinematicEnd = cinematicEnd;
    }
}
