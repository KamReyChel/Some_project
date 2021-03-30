using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiveComponent : MonoBehaviour, IRestartableObject
{
    protected abstract Vector3 startPosition { get; set; }
    protected abstract Quaternion startRotation { get; set; }
    protected abstract Rigidbody2D cRigidbody { get; set; }

    public void DoRestart()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;

        ///Prosty bug fix
        
        if (cRigidbody == null)
            return;

        cRigidbody.velocity = Vector3.zero;
        cRigidbody.angularVelocity = 0.0f;
        cRigidbody.simulated = true;
    }

    protected void DoPlay()
    {
        cRigidbody.simulated = true;
    }

    protected void DoPause()
    {
        cRigidbody.simulated = false;
    }
}
