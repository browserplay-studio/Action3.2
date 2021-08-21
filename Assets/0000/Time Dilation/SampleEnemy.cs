using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleEnemy : MonoBehaviour
{
    [SerializeField] private float m_Amplitude = 1;
    [SerializeField] private float m_Frequency = 1;

    private float m_ElapsedTime = 0;

    private void Update()
    {
        m_ElapsedTime += m_Frequency * Time.deltaTime;
        transform.position = Vector3.up * Mathf.Sin(m_ElapsedTime) * m_Amplitude;
    }
}
