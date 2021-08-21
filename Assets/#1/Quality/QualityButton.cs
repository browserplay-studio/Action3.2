using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Text = UnityEngine.UI.Text;

public class QualityButton : MonoBehaviour
{
    [SerializeField] private Button m_Button = null;
    [SerializeField] private Text m_Label = null;
    [SerializeField] private Image m_Image = null;

    public void Init(string name, UnityEngine.Events.UnityAction clickAction)
    {
        m_Label.text = name;
        m_Button.onClick.AddListener(clickAction);
    }

    public void UpdateVisual(bool isActive)
    {
        m_Image.color = isActive ? Color.green : Color.white;
    }
}
