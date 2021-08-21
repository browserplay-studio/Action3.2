using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDilation : MonoBehaviour
{
    [SerializeField] private KeyCode m_Key = KeyCode.Mouse1;
    [SerializeField] private InterpolatorFloat m_TimeScaleInterpolator = null;
    [SerializeField, Range(0, 1)] private float m_Amount = 0;

    private void Update()
    {
        if (Input.GetKeyDown(m_Key))
        {
            m_TimeScaleInterpolator.Activate(Time.timeScale, m_Amount);
        }
        else if (Input.GetKeyUp(m_Key))
        {
            m_TimeScaleInterpolator.Activate(Time.timeScale, 1);
        }

        if (m_TimeScaleInterpolator.IsActive)
        {
            Time.timeScale = m_TimeScaleInterpolator.CurrentValue;
        }
    }

    public void SetAmont(float value)
    {
        m_Amount = Mathf.Clamp01(value);
    }
}
