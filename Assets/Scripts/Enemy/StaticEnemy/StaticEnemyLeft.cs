using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemyLeft : EnemyDetection
{
    protected override bool CompareAngle(float angle, float fovAngle)
    {
        if(angle > fovAngle /2f)
        {
            return false;
        }
        return true;
    }

    protected override float GetAngle(Vector2 enemyDir)
    {
        return Vector2.Angle(movementDirection, enemyDir);
    }

    protected override void SetMovementDirection(Vector2 direction)
    {
        movementDirection = direction;
    }

    protected override void BeforeGetAngle()
    {
        SetMovementDirection(Vector2.left);
    }
}
