using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetComponent : InteractiveComponent
{

    private Vector3 sPosition;
    private Quaternion sRotation;

    private ParticleSystem m_particleSystem;
    private AudioSource m_audioSource;
    private Rigidbody2D m_rigidbody;

    protected override Vector3 startPosition { get => sPosition; set => sPosition = value; }
    protected override Quaternion startRotation { get => sRotation; set => sRotation = value; }
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
            m_audioSource.Play();
            Debug.Log("Hit");
        }   
    }
}
