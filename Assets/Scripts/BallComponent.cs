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

    public float speed = 1.0f;
    //private float realSpeed;

    //public BallInstruction Instruction = BallInstruction.Idle;
    public List<BallInstruction> Instructions = new List<BallInstruction>();

    public float rotationSpeed = 10.0f;
    public float sizeChangeSpeed = 1.0f;
    //private Vector3 _vecRotation = Vector3.zero;
    public float instructionLength = 1.0f;

    //private Vector3 leftLook = new Vector3(-1, 1, 1);
    //private Vector3 rightLook = Vector3.one;

    //private bool isBig = false;

    //private Vector3 _desirableScaleVector = new Vector3();

    //GameState State = GameState.Start;
    //EnemyStatus EnemyState = EnemyStatus.Walking;

    Rigidbody2D m_rigidbody;

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
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log("Mouse position: " + Input.mousePosition);
        //Debug.Log("Mouse in world position: " + worldPos);


        if (Input.GetMouseButtonDown(0))
            Debug.Log("Left mouse button has been pressed");

        m_rigidbody.simulated = !GameplayManager.Instance.pause;
  
    }

    private void OnMouseEnter()
    {
        Debug.Log("Mouse entering over object");
    }

    private void OnMouseExit()
    {
        Debug.Log("Mouse leaving object");
        
    }

    private void OnMouseUp()
    {
        m_rigidbody.simulated = true;

    }

    private void OnMouseDrag()
    {
        /*
        if (GameplayManager.Instance.pause)
            return;
        */
        m_rigidbody.simulated = false;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(worldPos.x, worldPos.y);
    }
}
    

