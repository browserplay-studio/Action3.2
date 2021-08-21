using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualityWindow : MonoBehaviour
{
    [SerializeField] private Transform m_ButtonsParent = null;
    [SerializeField] private QualityButton m_ButtonPrefab = null;

    private QualityButton[] m_Buttons = null;

    private int m_CurrentIndex = 0;

    private const string SAVE_KEY = "SAVE_QUALITY";

    private void Awake()
    {
        if (PlayerPrefs.HasKey(SAVE_KEY))
        {
            m_CurrentIndex = PlayerPrefs.GetInt(SAVE_KEY);
        }
        else
        {
            int index = QualitySettings.GetQualityLevel();
            if (m_CurrentIndex != index)
            {
                m_CurrentIndex = index;
                UpdateSettings();
                Save();
            }
        }
    }

    private void Start()
    {
        PopulateElements();
        UpdateVisual();
    }

    private void PopulateElements()
    {
        string[] names = QualitySettings.names;

        m_Buttons = new QualityButton[names.Length];

        for (int i = 0; i < m_Buttons.Length; i++)
        {
            var button = Instantiate(m_ButtonPrefab, m_ButtonsParent);
            int k = i;
            button.Init(names[i], () => OnClick(k));
            m_Buttons[i] = button;
        }
    }

    private void UpdateVisual()
    {
        for (int i = 0; i < m_Buttons.Length; i++)
        {
            var button = m_Buttons[i];
            button.UpdateVisual(i == m_CurrentIndex);
        }
    }

    private void OnClick(int index)
    {
        if (index == m_CurrentIndex) return;

        m_CurrentIndex = index;
        UpdateVisual();
        UpdateSettings();
        Save();
    }

    private void UpdateSettings()
    {
        QualitySettings.SetQualityLevel(m_CurrentIndex, true);
    }

    private void Save()
    {
        PlayerPrefs.SetInt(SAVE_KEY, m_CurrentIndex);
    }
}
