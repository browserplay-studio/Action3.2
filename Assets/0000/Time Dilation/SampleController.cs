using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleController : MonoBehaviour
{
    [SerializeField] private Transform m_Head = null;
    [SerializeField] private Vector2 m_MouseSensitivity = Vector2.one;

    private Vector2 m_Angles = Vector2.zero;

    private void Update()
    {
        // dont multiply with Time.deltaTime, otherwise it would be timeScale dependent
        m_Angles.y += Input.GetAxis("Mouse X") * m_MouseSensitivity.x;
        m_Angles.x -= Input.GetAxis("Mouse Y") * m_MouseSensitivity.y;

        m_Head.rotation = Quaternion.Euler(m_Angles);
    }
}
