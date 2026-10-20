using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform pointA;
    [SerializeField]
    private Transform pointB;
    private Transform target;

    private Vector2 startPosition;
    private float elapsedTime;
    [SerializeField]
    private float duration = 5f;

    private bool targetASelected = true; 
    private bool changingTarget;
    private bool canMove;

    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        target = pointA;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(!canMove){return;}

        if(changingTarget) {return;}

        elapsedTime += Time.deltaTime;

        float t = Mathf.Clamp01(elapsedTime / duration);

        Vector2 newPosition = Vector2.Lerp(startPosition, target.position,t);

        rb.MovePosition(newPosition);

        if(Vector2.Distance(transform.position, target.transform.position) <= 0.05f)
        {
            changingTarget = true;
            ChangeTarget(!targetASelected);
        }
    }

    private void ChangeTarget(bool targetSelected)
    {
        elapsedTime  = 0f;

        targetASelected = targetSelected;

        if(targetASelected)
        {
            target = pointA;
        }
        else
        {
            target = pointB;
        }

        startPosition = transform.position;

        changingTarget = false;
    }

    //Getters y Setters
    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }
}
