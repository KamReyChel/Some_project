using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongAnim : BallComponent
{
    [SerializeField]
    private Vector3 _targetScale = new Vector3(4.0f, 4.0f, 4.0f);
    [SerializeField]
    private Vector3 _initialScale;
    [SerializeField]
    protected float _timeOfChange = 1;

    private float deltaForTime;

    // Start is called before the first frame update
    void Start()
    {
        _initialScale = transform.localScale; 
    }

    // Update is called once per frame
    void Update()
    {
        deltaForTime = Mathf.Abs(_initialScale.magnitude - _targetScale.magnitude);

        Time.timeScale = deltaForTime / _timeOfChange;

        ChangeScaleOfObject(_targetScale, transform, Speed);

        if(transform.localScale == _targetScale)
        {
            Debug.Log("Done!");
            _targetScale = _initialScale;
            _initialScale = transform.localScale;
        }
    }
}
