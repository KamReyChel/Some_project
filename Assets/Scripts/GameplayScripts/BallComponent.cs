using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BallComponent : InteractiveComponent
{

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
    private Rigidbody2D m_rigidbody;

    protected override Vector3 startPosition { get => m_startPosition; set => m_startPosition = value; }
    protected override Quaternion startRotation { get => m_startRotation; set => m_startRotation = value; }
    protected override Rigidbody2D cRigidbody { get => m_rigidbody; set => m_rigidbody = value; }

    private Rigidbody2D m_connectedBody;
    private SpringJoint2D m_connectedJoint;
    private LineRenderer m_linerenderer;
    private TrailRenderer m_trailRenderer;
    private Animator m_animator;
    private AudioSource m_audioSource;
    private ParticleSystem m_particles;

    [SerializeField]
    private GameObject leftArmSlingshot;

    [SerializeField]
    private CameraController mainCamera;

    [SerializeField]
    private GameSettingsDatabase gameDatabase;

    [SerializeField] private Camera mainSceneCamera;

    public void ChangeScaleOfObject(Vector3 targetScaleVector, Transform gameObjectTransform, float speed)
    {

        Vector3 deltaScaleVector = targetScaleVector - gameObjectTransform.localScale;


        if (Mathf.Abs(deltaScaleVector.x) >= 0.01)
        {
            gameObjectTransform.localScale += deltaScaleVector.normalized * (Time.deltaTime * speed);
        }
        else
        {
            gameObjectTransform.localScale = targetScaleVector;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        cRigidbody = GetComponent<Rigidbody2D>();
        m_connectedJoint = GetComponent<SpringJoint2D>();
        m_connectedBody = m_connectedJoint.connectedBody;
        m_linerenderer = GetComponent<LineRenderer>();
        m_trailRenderer = GetComponent<TrailRenderer>();
        m_audioSource = GetComponent<AudioSource>();
        m_animator = GetComponentInChildren<Animator>();
        m_particles = GetComponentInChildren<ParticleSystem>();

        startPosition = transform.position;
        startRotation = transform.rotation;

        GameplayManager.OnGamePaused += DoPause;
        GameplayManager.OnGamePlaying += DoPlay;
        GameplayManager.GameReset += DoAddicionalReset;

        StartCoroutine(LoseConnection());
    }

    private void FixedUpdate()
    {
        physicsSpeed = cRigidbody.velocity.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (transform.position.x > m_connectedBody.transform.position.x + SlingStart)
        {
            m_connectedJoint.enabled = false;
            m_linerenderer.enabled = false;
            m_trailRenderer.enabled = !m_hitTheGround;
        }
        */

    }


    private void OnMouseDown()
    {
        m_audioSource.PlayOneShot(gameDatabase.pullSound);
    }
    private void OnMouseUp()
    {
        cRigidbody.simulated = true;
        m_audioSource.PlayOneShot(gameDatabase.shootSound);
        m_particles.Play();
    }
    
    private void OnMouseDrag()
    {
        if (GameplayManager.Instance.GameState == EGameState.Playing)
        {
            cRigidbody.simulated = false;
            m_hitTheGround = false;

            //Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 worldPos = mainSceneCamera.ScreenToWorldPoint(Input.mousePosition);
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
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            m_hitTheGround = true;
            m_audioSource.PlayOneShot(gameDatabase.impactSound);

            m_animator.enabled = true;
            m_animator.Play(0);
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

    private void DoAddicionalReset()
    {
        m_connectedJoint.enabled = true;
        m_linerenderer.enabled = true;
        m_trailRenderer.enabled = false;

        SetLineRendererPoints();

        mainCamera.SetOriginalPosition();
        m_audioSource.PlayOneShot(gameDatabase.restartSound);
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


    IEnumerator LoseConnection()
    {
        while (true)
        {
            if (Time.frameCount % 2 == 0)
            {
                if (transform.position.x > m_connectedBody.transform.position.x + SlingStart)
                {
                    m_connectedJoint.enabled = false;
                    m_linerenderer.enabled = false;
                    m_trailRenderer.enabled = !m_hitTheGround;

                    yield return new WaitForEndOfFrame();
                }
            }

            yield return null;
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
    

