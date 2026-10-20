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

    public void SetYOffset(float newOffset)
    {
        yOffset = newOffset;
    }

    public void ResetYOffset()
    {
        yOffset = normalYOffset;
    }
}
