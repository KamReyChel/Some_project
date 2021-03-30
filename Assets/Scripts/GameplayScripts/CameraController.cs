using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private BallComponent followTarget;
    private Vector3 originalPostion;
    private Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        //followTarget = FindObjectOfType<BallComponent>();
        originalPostion = transform.position;
    }

    private void LateUpdate()
    {
        if (!followTarget.IsSimulated())
            return;

        //transform.position = followTarget.transform.position + originalPostion;
        targetPosition = followTarget.transform.position + originalPostion;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, followTarget.ComponentPhisicsRealSpeed());

    }

    public void SetOriginalPosition()
    {
        transform.position = originalPostion;
    }
}
