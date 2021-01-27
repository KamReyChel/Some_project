using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinLevitate : MonoBehaviour
{

    private Vector3 m_startPosition;
    private Vector3 m_startLocalScale;

    private float m_curYPos = 0.0f;
    private float m_curZRot = 0.0f;
    private float m_curXYScale = 1.0f;

    public float ampliture = 1.0f;
    public float rotationSpeed = 50.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_startPosition = transform.position;
        m_startLocalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        m_curYPos = Mathf.PingPong(Time.time, ampliture) - ampliture * 0.5f;
        m_curXYScale = Mathf.PingPong(Time.time, ampliture) + ampliture;

        transform.position = new Vector3(m_startPosition.x, 
            m_startPosition.y + m_curYPos, 
            m_startPosition.z);

        transform.localScale = new Vector3(m_startLocalScale.x * m_curXYScale, 
            m_startLocalScale.y * m_curXYScale, 
            m_startLocalScale.z);

        m_curZRot += Time.deltaTime * rotationSpeed;
        transform.rotation = Quaternion.Euler(0, 0, m_curZRot);
    }
}
