using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetComponent : MonoBehaviour
{
    private ParticleSystem m_particleSystem;
    private AudioSource m_audioSource;
    private void Start()
    {
        m_particleSystem = GetComponent<ParticleSystem>();
        m_audioSource = GetComponent<AudioSource>();
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
