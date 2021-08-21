using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterpolatorFloat : Interpolator<float>
{
    protected override void HandleValue()
    {
        m_Percentage01 += GetSpeed() * Time.unscaledDeltaTime;
        float smoothPercentage = GetSmoothPercentage();
        m_CurrentValue = Mathf.Lerp(m_StartValue, m_TargetValue, smoothPercentage);
    }
}