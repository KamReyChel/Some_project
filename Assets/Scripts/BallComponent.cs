using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameState
{
    Start,
    Pause,
    Exit
}

[Flags]
public enum EnemyStatus
{
    Walking = 1,
    Shooting = 2,
    Jumping = 4,
    Alarmed = 8
}

public enum BallInstruction
{
    Idle = 0,
    MoveUp,
    MoveDown,
    MoveLeft,
    MoveRight,
    RotateLeft,
    RotateRight,
    ScaleUp,
    ScaleDown
}


public class BallComponent : MonoBehaviour
{

    public float Speed = 1.0f;
    //public BallInstruction Instruction = BallInstruction.Idle;
    public List<BallInstruction> Instructions = new List<BallInstruction>();

    public float RotationSpeed = 10.0f;
    public float SizeChangeSpeed = 1.0f;
    private Vector3 _vecRotation = Vector3.zero;
    public float InstructionLength = 1.0f;

    //private Vector3 _desirableScaleVector = new Vector3();

    //GameState State = GameState.Start;
    //EnemyStatus EnemyState = EnemyStatus.Walking;

    public void ChangeScaleOfObject(Vector3 targetScaleVector, Transform gameObjectTransform, float speed)
    {

        Vector3 deltaScaleVector = targetScaleVector - gameObjectTransform.localScale;


        if (Mathf.Abs(deltaScaleVector.x) >= 0.01)
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
        //_desirableScaleVector.Set(3.0f, 3.0f, 3.0f);

        //Debug.Log("State: " + State);
        //int StateVal = (int)State;
        //++StateVal;
        //State = (GameState)((int)State + 1);
        //++State;
        //Debug.Log("New State: " + State);

        /*

        EnemyState |= EnemyStatus.Alarmed;
        EnemyState ^= EnemyStatus.Jumping;

        if ((EnemyState & EnemyStatus.Jumping) == EnemyStatus.Jumping)
        Debug.Log("IS JUMPING");

        */

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Czas od ostatniej klatki: " + Time.deltaTime);


        //transform.rotation = Quaternion.Euler(_vecRotation);

        //transform.position += Vector3.up * Time.deltaTime;

        //ChangeScaleOfObject(_desirableScaleVector, transform, Speed);

        /*
        switch (Instruction)
        {
            case BallInstruction.MoveUp:
                transform.position += Vector3.up * Speed * Time.deltaTime;
                break;

            case BallInstruction.MoveDown:
                transform.position += Vector3.down * Speed * Time.deltaTime;
                break;

            case BallInstruction.MoveLeft:
                transform.position += Vector3.left * Speed * Time.deltaTime;
                break;

            case BallInstruction.MoveRight:
                transform.position += Vector3.right * Speed * Time.deltaTime;
                break;

            case BallInstruction.RotateLeft:
                _vecRotation += Vector3.forward * RotationSpeed;
                transform.rotation = Quaternion.Euler(_vecRotation);
                break;

            case BallInstruction.RotateRight:
                _vecRotation += Vector3.back * RotationSpeed;
                transform.rotation = Quaternion.Euler(_vecRotation);
                break;

            case BallInstruction.ScaleUp:
                transform.localScale += Vector3.one * SizeChangeSpeed * Time.deltaTime;
                break;

            case BallInstruction.ScaleDown:
                transform.localScale -= Vector3.one * SizeChangeSpeed * Time.deltaTime;
                break;

            default:
                Debug.Log("Idle");
                break;
        }

        */

        }


    }


    

