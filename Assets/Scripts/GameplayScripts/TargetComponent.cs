using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetComponent : InteractiveComponent
{
    [SerializeField]
    private GameSettingsDatabase gameDatabase;

    private Vector3 m_startPosition;
    private Quaternion m_startRotation;

    private ParticleSystem m_particleSystem;
    private AudioSource m_audioSource;
    private Rigidbody2D m_rigidbody;
    private bool gotHit;

    protected override Vector3 startPosition { get => m_startPosition; set => m_startPosition = value; }
    protected override Quaternion startRotation { get => m_startRotation; set => m_startRotation = value; }
    protected override Rigidbody2D cRigidbody { get => m_rigidbody; set => m_rigidbody = value; }

    private void Start()
    {
        m_particleSystem = GetComponent<ParticleSystem>();
        m_audioSource = GetComponent<AudioSource>();
        cRigidbody = GetComponent<Rigidbody2D>();

        startPosition = transform.position;
        startRotation = transform.rotation;

        GameplayManager.OnGamePlaying += DoPlay;
        GameplayManager.OnGamePaused += DoPause;


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            m_particleSystem.Play();
            m_audioSource.PlayOneShot(gameDatabase.targetHitSound);
            Debug.Log("Hit");
            GameplayManager.Instance.Points += 1;
            SaveManager.Instance.saveData.m_lifetimeHits += 1;

            if (!gotHit && !BallComponent.mistedShoot)
            {
                AnalyticsManager.Instance.SendEvent("HitTarget");
                gotHit = true;
            }
        }
    }
}
