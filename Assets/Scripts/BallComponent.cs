using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallComponent : MonoBehaviour
{

    public float Speed = 1.0f;
    //public float RotationSpeed = 10.0f;
    //public float SizeChangeSpeed = 1.0f;
    //private Vector3 _vecRotation = Vector3.zero;
    private Vector3 _desirableScaleVector = new Vector3();


    public void ChangeScaleOfObject(Vector3 targetScaleVector, Transform gameObjectTransform, float speed)
    {
        
        Vector3 deltaScaleVector = targetScaleVector - gameObjectTransform.localScale;


        if(Mathf.Abs(deltaScaleVector.x) >= 0.01)
        {
            gameObjectTransform.localScale += deltaScaleVector.normalized * Time.deltaTime * speed;
        }
        else
        {
            gameObjectTransform.localScale = targetScaleVector;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Hello World!");
        _desirableScaleVector.Set(3.0f, 3.0f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Czas od ostatniej klatki: " + Time.deltaTime);
        
        //_vecRotation += Vector3.forward * RotationSpeed;
        //transform.rotation = Quaternion.Euler(_vecRotation);

        //transform.position += Vector3.up * Time.deltaTime;

        ChangeScaleOfObject(_desirableScaleVector, transform, Speed);

        


    }
}
