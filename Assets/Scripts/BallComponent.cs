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

    public List<BallInstruction> Instructions = new List<BallInstruction>();

    public float speed = 1.0f;
    public float rotationSpeed = 10.0f;
    public float sizeChangeSpeed = 1.0f;

    public float instructionLength = 1.0f;
    
    public float SlingStart = 0.5f;
    public float maxSpringDistance = 2.5f;

    public bool restartStatus = false;

    private float physicsSpeed;

    private bool m_hitTheGround = false;
    

    private Vector3 m_startPosition;
    private Quaternion m_startRotation;

    private SpringJoint2D m_connectedJoint;
    private Rigidbody2D m_connectedBody;
    private LineRenderer m_linerenderer;
    private TrailRenderer m_trailRenderer;

    [SerializeField]
    private GameObject leftArmSlingshot;

    [SerializeField]
    private CameraController mainCamera;

    private Rigidbody2D m_rigidbody;

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
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_connectedJoint = GetComponent<SpringJoint2D>();
        m_connectedBody = m_connectedJoint.connectedBody;
        m_linerenderer = GetComponent<LineRenderer>();
        m_trailRenderer = GetComponent<TrailRenderer>();

        m_startPosition = transform.position;
        m_startRotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        physicsSpeed = m_rigidbody.velocity.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > m_connectedBody.transform.position.x + SlingStart)
        {
            m_connectedJoint.enabled = false;
            m_linerenderer.enabled = false;
            m_trailRenderer.enabled = !m_hitTheGround;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }


    }

    /*
    private void OnMouseEnter()
    {
        Debug.Log("Mouse entering over object");
    }

    private void OnMouseExit()
    {
        Debug.Log("Mouse leaving object");
        
    }
    */
    private void OnMouseUp()
    {
        m_rigidbody.simulated = true;

    }
    
    private void OnMouseDrag()
    {
        m_rigidbody.simulated = false;
        m_hitTheGround = false;


        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 newBallPos = new Vector3(worldPos.x, worldPos.y);

        float CurJointDistance = Vector3.Distance(newBallPos, m_connectedBody.transform.position);

        if (CurJointDistance > maxSpringDistance)
        {
            Vector2 direction = (newBallPos - m_connectedBody.position).normalized;
            transform.position = m_connectedBody.position + direction * maxSpringDistance;
        }
        else
        {
            transform.position = newBallPos;
        }

        SetLineRendererPoints();

        //transform.position = new Vector3(worldPos.x, worldPos.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            m_hitTheGround = true;
        }
    }

    public bool IsSimulated()
    {
        return m_rigidbody.simulated;
    }

    public float ComponentPhisicsRealSpeed()
    {
        return physicsSpeed * Time.fixedDeltaTime;
    }

    private void Restart()
    {
        transform.position = m_startPosition;
        transform.rotation = m_startRotation;

        m_rigidbody.velocity = Vector3.zero;
        m_rigidbody.angularVelocity = 0.0f;
        m_rigidbody.simulated = true;

        m_connectedJoint.enabled = true;
        m_linerenderer.enabled = true;
        m_trailRenderer.enabled = false;

        SetLineRendererPoints();

        mainCamera.SetOriginalPosition();
        
    }

    private void SetLineRendererPoints()
    {
        m_linerenderer.positionCount = 3;
        m_linerenderer.SetPositions(new Vector3[]
        {
            m_connectedBody.position,
            transform.position,
            leftArmSlingshot.transform.position
        });
    }
}
    

