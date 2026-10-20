using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    private bool detected = false;

    public void GotDetected()
    {
        detected = true;
    }

    public bool IsDetected()
    {
        return detected;
    }
}
