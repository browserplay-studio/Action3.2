using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [SerializeField] private LayerMask m_ObstacleMask = 0;
    [SerializeField] private LineRenderer m_LineRenderer = null;
    [SerializeField] private Transform m_Light = null;

    private int m_MaxDistanse = 100;

    private void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        bool raycast = Physics.Raycast(ray, out var hit, m_MaxDistanse, m_ObstacleMask);

        Vector3 origin = ray.origin;
        Vector3 point = raycast ? hit.point : ray.GetPoint(m_MaxDistanse);

        m_Light.position = Vector3.Lerp(origin, point, 0.95f);

        if (!m_LineRenderer.useWorldSpace)
        {
            origin = transform.InverseTransformPoint(origin);
            point = transform.InverseTransformPoint(point);
        }

        m_LineRenderer.SetPosition(0, origin);
        m_LineRenderer.SetPosition(1, point);
    }
}
